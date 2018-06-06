using System.Collections.Generic;
using Lovewing.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Graphics.Shapes;
using Lovewing.Beatmaps;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Transforms;
using osu.Framework.MathUtils;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Containers;
using Lovewing.Gameplay;
using Lovewing.Graphics.Gameplay;

/*
 * Dimensions:
 * Target circle radius is 65px (130px d)
 * Source circle radius is 67.5px (135px d)
 */

namespace Lovewing.Screens.Game
{
    class TargetCircle : Container
    {
        /// <summary>
        /// Construct a target circle
        /// </summary>
        public TargetCircle()
        {
            Add(new CircularContainer
            {
                Masking = true,
                Origin = Anchor.Centre,
                Height = GameScreen.TargetCircleRadius * 2,
                Width = GameScreen.TargetCircleRadius * 2,
                BorderColour = Color4.White,
                BorderThickness = 5.0f,
                Child = new Box
                {
                    Colour = Color4.Transparent,
                    RelativeSizeAxes = Axes.Both
                }
            });
        }
    }

    class SourceCircle : Container
    {
        public const float SourceCircleRadius = 67.5f;

        public SourceCircle()
        {
            // Source circle
            Add(new CircularContainer
            {
                Height = SourceCircleRadius * 2.2f,
                Width = SourceCircleRadius * 2.2f,
                BorderColour = new Color4(1.0f, 1.0f, 1.0f, 0.4f),
                Origin = Anchor.Centre,
                BorderThickness = 24,
                CornerRadius = 0,
                Masking = true,
                Child = new Box
                {
                    Colour = Color4.Transparent,
                    RelativeSizeAxes = Axes.Both
                }
            }.WithEffect(new BlurEffect
            {
                PadExtent = true,
                Strength = 1.0f,
                Sigma = new Vector2(6f, 6f),
                Blending = new BlendingParameters
                {
                    Mode = BlendingMode.Additive,
                    RGBEquation = BlendingEquation.Add,
                    AlphaEquation = BlendingEquation.Add
                }
            }));

            Add(new CircularContainer
            {
                Height = SourceCircleRadius * 2,
                Width = SourceCircleRadius * 2,
                BorderColour = Color4.White,
                Origin = Anchor.Centre,
                BorderThickness = 8,
                CornerRadius = 0,
                Masking = true,
                Child = new Box
                {
                    Colour = Color4.Transparent,
                    RelativeSizeAxes = Axes.Both
                }
            });
        }
    }

    public class GameScreen : Screen
    {
        public const float TargetCircleRadius = 65.0f;
        public Vector2 SourcePosition = new Vector2(0.5f, 0.25f);

        public GameScreen()
        {
            AlwaysPresent = true;
        }

        /// <summary>
        /// The current beatmap that the game screen is running.
        /// </summary>
        public Beatmap Beatmap
        {
            get => beatmap;
            set => loadGameState(value);
        }
        private Beatmap beatmap;

        private GameState gameState;

        private Vector2[] lanePositions = new Vector2[9];

        private double timeBase = 0.0;

        private uint noteSpawnCount = 0;

        private void loadGameState(Beatmap bm)
        {
            beatmap = bm;
            gameState = new GameState(bm, 1.0);
            timeBase = Time.Current / 1000.0;
        }

        protected override void LoadComplete()
        {
            AddRange(new Drawable[]
            {
                new Background(@"Backgrounds/game_default")
                {
                    BlurSigma = new Vector2(8.0f)
                }
            });
        }

        protected override void OnEntering(Screen last)
        {
            
            SourceCircle source = new SourceCircle()
            {
                RelativePositionAxes = Axes.Both,
                Position = new Vector2(0.5f, -0.25f)
            };
            Add(source);
            source.MoveTo(SourcePosition, 500).OnComplete(circle =>
            {
                const double startRotation = 0.0f;
                const double endRotation = MathHelper.Pi;
                for (uint i = 0; i < 9; i++)
                {
                    // Take the source position, add a unit vector of (1, 0) by rotation angle, multiply it by offset and add it to source position.
                    float angle = (float)Interpolation.Lerp(startRotation, endRotation, i / 8.0);
                    Vector2 temp = Vector2Extensions.Transform(new Vector2(1.0f, 0.0f), Matrix3.CreateRotationZ(angle));
                    temp.Y *= 1.4f;
                    temp = Vector2.Multiply(temp, 0.4f);
                    temp = Vector2.Add(temp, SourcePosition);
                    lanePositions[i] = temp;
                    TargetCircle target = new TargetCircle()
                    {
                        RelativePositionAxes = Axes.Both,
                        Position = SourcePosition
                    };
                    Add(target);
                    target.MoveTo(temp, 1000, Easing.OutElasticHalf);
                }
            });
        }

        protected override void Update()
        {
            if(gameState != null)
            {
                double scaledTime = Time.Current - timeBase;
                gameState.Update(scaledTime, noteState =>
                {
                    ActiveNote newNote = new ActiveNote
                    {
                        RelativePositionAxes = Axes.Both,
                        Position = SourcePosition
                    };

                    Add(newNote);
                    newNote.MoveTo(lanePositions[noteState.LaneIndex], noteState.HitTime - scaledTime)
                           .OnComplete(note => Remove(note));
                });
            }
        }
    }
}

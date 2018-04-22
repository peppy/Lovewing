using Lovewing.Beatmaps;
using Lovewing.Graphics;
using Lovewing.Graphics.Containers;
using Lovewing.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Screens;
using System;
using System.Collections.Generic;

namespace Lovewing.Screens.Game
{
    public class SongSelectorScreen : Screen
    {
        private Bindable<Beatmap> selected = new Bindable<Beatmap>();
        private Bindable<List<Beatmap>> beatmaps = new Bindable<List<Beatmap>>(new List<Beatmap>());

        private SpriteText titleText;
        private SpriteText artistText;
        private ScrollContainer beatmapContainer;

        public SongSelectorScreen()
        {
            beatmaps.Value.AddRange(new Beatmap[]
            {
                new Beatmap
                {
                    Title = @"Questions",
                    Artists = { @"Tristam" }
                },
                new Beatmap
                {
                    Title = @"Snow Halation",
                    Artists = { @"Muse" }
                },
                new Beatmap
                {
                    Title = @"Moving Hectic",
                    Artists = { @"Slippy", @"Harry Shota" }
                },
                new Beatmap
                {
                    Title = @"Broken Bones",
                    Artists = { @"Pixl", @"Cassandra Kay" }
                },
                new Beatmap
                {
                    Title = @"Searching for You",
                    Artists = { @"Unlike Pluto", @"Karra", @"Eric Zayne" }
                }
            });

            selected.Value = beatmaps.Value[0];
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            var maps = beatmaps.Value;

            int i;

            switch (args.Key)
            {
                case Key.Left:
                    i = maps.IndexOf(selected) - 1;

                    if (i > -1)
                        selected.Value = maps[i];

                    return true;

                case Key.Right:
                    i = maps.IndexOf(selected) + 1;

                    if (i < maps.Count)
                        selected.Value = maps[i];

                    return true;

                default:
                    return base.OnKeyDown(state, args);
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            selected.ValueChanged += val =>
            {
                titleText.Text = val.Title;
                artistText.Text = string.Join(", ", val.Artists);
            };

            AddRange(new Drawable[]
            {
                new Background(@"Backgrounds/liveshow"),
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Height = 500,
                    Children = new Drawable[]
                    {
                        new FocusedTextBox
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            PlaceholderText = @"Search",
                            Width = 250,
                            Height = 50,
                            BorderColour = Color4.White,
                            BorderThickness = 5,
                            CornerRadius = 0,
                            CommitColour = Color4.LimeGreen,
                            FocusedColour = LovewingColours.LightBlue,
                            UnfocusedColour = LovewingColours.Transparent,
                            PlaceholderColour = LovewingColours.White,
                            TextColour = LovewingColours.White
                        },
                        beatmapContainer = new ScrollContainer(Direction.Horizontal)
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.X,
                            Height = 400,
                            Margin = new MarginPadding { Bottom = 100 }
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 200,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.5f),
                            RelativeSizeAxes = Axes.Both
                        },
                        titleText = new SpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding { Top = 20 },
                            Text = selected.Value.Title,
                            TextSize = 40,
                            AllowMultiline = false
                        },
                        artistText = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Margin = new MarginPadding { Bottom = 20 },
                            Text = string.Join(", ", selected.Value.Artists),
                            AllowMultiline = false
                        },
                        new LovewingButton
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Margin = new MarginPadding { Bottom = 20 },
                            CornerRadius = 0,
                            Text = @"M.U.S.I.C. START!",
                            TextSize = 20,
                            BackgroundColour = LovewingColours.Blue,
                            Height = 50,
                            Width = 200
                        }
                    }
                }
            });

            beatmapContainer.ScrollContent.Anchor = Anchor.Centre;
            beatmapContainer.ScrollContent.Origin = Anchor.Centre;

            foreach (var beatmap in beatmaps.Value)
            {
                int index = beatmaps.Value.IndexOf(beatmap);

                beatmapContainer?.Add(new BeatmapItem(beatmap)
                {
                    Margin = new MarginPadding { Left = 450 * index },
                    Action = () => selected.Value = beatmap
                });
            }

            base.LoadComplete();
        }

        private class BeatmapItem : ClickableContainer
        {
            private readonly Box hover;
            private Circle curRipple;

            public BeatmapItem(Beatmap beatmap)
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                Size = new Vector2(200);
                Masking = true;
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Radius = 10,
                    Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.05f)
                };
                CornerRadius = 5;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = LovewingColours.LightBlue
                    },
                    new SpriteText
                    {
                        Rotation = 45,
                        Text = beatmap.Title,
                        Colour = Color4.White,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre
                    },
                    hover = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = ColourInfo.SingleColour(Color4.White).MultiplyAlpha(0.2f),
                        Alpha = 0
                    }
                };
            }

            protected override bool OnHover(InputState state)
            {
                hover.FadeIn(200);

                return base.OnHover(state);
            }

            protected override void OnHoverLost(InputState state)
            {
                hover.FadeOut(200);

                base.OnHoverLost(state);
            }

            protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
            {
                // var x = state.Mouse.Position.X - BoundingBox.X - BoundingBox.Width / 2;
                // var y = state.Mouse.Position.Y - BoundingBox.Y - BoundingBox.Height / 2;

                Circle ripple;

                AddInternal(ripple = new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    // X = x,
                    // Y = y,
                    Width = 10,
                    Height = 10,
                    Colour = ColourInfo.SingleColour(Color4.Gray).MultiplyAlpha(0.3f),
                    Blending = BlendingMode.Additive
                });

                ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 650, Easing.OutCirc);

                curRipple = ripple;

                return base.OnMouseDown(state, args);
            }

            protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
            {
                curRipple?.FadeOut(450)
                    .Expire();

                curRipple = null;

                return base.OnMouseUp(state, args);
            }
        }
    }
}

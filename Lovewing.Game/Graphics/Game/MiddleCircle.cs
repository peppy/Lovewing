using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Game
{
    public class MiddleCircle : CircularContainer
    {
        private readonly CircularProgress healthCircle;
        private readonly SpriteIcon musicIcon;

        public Color4 StateColour
        {
            get => healthCircle.Colour;
            set
            {
                healthCircle.FadeColour(value);
                musicIcon.FadeColour(value);
            }
        }

        public double Health
        {
            get => healthCircle.Current.Value;
            set => healthCircle.Current.Value = value;
        }

        public MiddleCircle()
        {
            Height = 128;
            Width = 128;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Colour = Color4.White.Opacity(0.5f),
                Radius = 10
            };

            AddRangeInternal(new Drawable[]
            {
                healthCircle = new CircularProgress
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Current = { Value = -0.75 },
                    Colour = new Color4(58, 244, 102, 255)
                },
                new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Height = 120,
                    Width = 120,
                    Colour = Color4.White
                },
                musicIcon = new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Height = 64,
                    Width = 64,
                    Colour = new Color4(58, 244, 102, 255),
                    Icon = FontAwesome.fa_music
                }
            });
        }
    }
}

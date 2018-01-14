using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class LovewingSmallButton : Button
    {
        private readonly Box hover;
        private readonly Box shadow;
        private readonly Box buttonBox;
        private readonly SpriteIcon icon;

        public FontAwesome Icon
        {
            get => icon.Icon;
            set => icon.Icon = value;
        }

        public float TextSize
        {
            get => SpriteText.TextSize;
            set => SpriteText.TextSize = value;
        }

        public string Font
        {
            get => SpriteText.Font;
            set => SpriteText.Font = value;
        }

        public Color4 TextColour
        {
            get => SpriteText.Colour;
            set => SpriteText.FadeColour(value);
        }

        public Color4 IconColour
        {
            get => icon.Colour;
            set => icon.FadeColour(value);
        }

        public Color4 ShadowColour
        {
            get => shadow.Colour;
            set => shadow.FadeColour(value);
        }

        public Color4 ButtonColour
        {
            get => buttonBox.Colour;
            set => buttonBox.FadeColour(value);
        }

        public LovewingSmallButton()
        {
            Height = 40;
            Masking = true;
            CornerRadius = 5;
            SpriteText.Font = @"Noto Sans CJK JP Regular";
            BackgroundColour = new Color4(0, 0, 0, 0);

            AddRangeInternal(new Drawable[]
            {
                shadow = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Margin = new MarginPadding
                    {
                        Top = 10
                    },
                    RelativeSizeAxes = Axes.Both
                },
                buttonBox = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding
                    {
                        Bottom = 10
                    }
                },
                icon = new SpriteIcon
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding
                    {
                        Left = 10,
                        Bottom = 5
                    },
                    Size = new Vector2(Height - 10)
                },
                SpriteText = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Margin = new MarginPadding
                    {
                        Bottom = 5
                    }
                },
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White.Opacity(0.5f),
                    Alpha = 0
                }
            });
        }

        protected override bool OnHover(InputState state)
        {
            hover.FadeIn(250);
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            hover.FadeOut(250);
            base.OnHoverLost(state);
        }
    }
}

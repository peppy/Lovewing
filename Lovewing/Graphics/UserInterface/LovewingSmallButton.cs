using Lovewing.Graphics.Sprites;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace Lovewing.Graphics.UserInterface
{
    public class LovewingSmallButton : LovewingButton
    {
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
            BackgroundColour = new Color4(0, 0, 0, 0);

            AddRangeInternal(new Drawable[]
            {
                shadow = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Margin = new MarginPadding { Top = 10 },
                    RelativeSizeAxes = Axes.Both
                },
                buttonBox = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding { Bottom = 10 }
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
                    Margin = new MarginPadding { Bottom = 5 },
                    Font = @"Noto Sans CJK JP Regular"
                },
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColourInfo.SingleColour(Color4.White).MultiplyAlpha(0.5f),
                    Alpha = 0
                }
            });
        }
    }
}

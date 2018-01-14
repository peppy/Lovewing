// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class LovewingHollowButton : Button
    {
        private readonly Box hover;
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

        public LovewingHollowButton()
        {
            Height = 40;
            Masking = true;
            BorderThickness = 5;
            CornerRadius = 5;
            SpriteText.Font = @"Noto Sans CJK JP Regular";

            AddRangeInternal(new Drawable[]
            {
                
                icon = new SpriteIcon
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding
                    {
                        Left = 10
                    },
                    Size = new Vector2(Height - 10)
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

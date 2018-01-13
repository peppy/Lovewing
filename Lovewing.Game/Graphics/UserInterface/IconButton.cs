// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Extensions.Color4Extensions;
using OpenTK.Graphics;
using System;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class IconButton : ClickableContainer
    {
        private readonly SpriteIcon hover;
        private readonly SpriteIcon spriteIcon;

        public FontAwesome Icon
        {
            get => spriteIcon.Icon;
            set { spriteIcon.Icon = value; hover.Icon = value; }
        }

        public Color4 HoverColour { get; set; } = Color4.Gray;

        public IconButton()
        {
            AddRangeInternal(new Drawable[]
            {
                spriteIcon = new SpriteIcon
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                hover = new SpriteIcon
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = HoverColour.Opacity(0.5f),
                    Alpha = 0
                }
            });
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

        protected override bool OnClick(InputState state)
        {
            Circle ripple;

            Add(ripple = new Circle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = 10,
                Width = 10,
                Colour = Color4.Gray,
                Blending = BlendingMode.Additive
            });

            ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 450, Easing.OutCirc)
                .FadeOut(450)
                .Expire();

            return base.OnClick(state);
        }
    }
}

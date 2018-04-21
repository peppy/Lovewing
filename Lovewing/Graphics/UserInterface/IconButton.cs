using Lovewing.Graphics.Sprites;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using System;

namespace Lovewing.Graphics.UserInterface
{
    public class IconButton : ClickableContainer
    {
        private readonly SpriteIcon hover;
        private readonly SpriteIcon spriteIcon;

        private Circle curRipple;

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
                    Colour = ColourInfo.SingleColour(HoverColour).MultiplyAlpha(0.5f),
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

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            Circle ripple;

            Add(ripple = new Circle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = 10,
                Width = 10,
                Colour = ColourInfo.SingleColour(Color4.Gray).MultiplyAlpha(0.5f),
                Blending = BlendingMode.Additive
            });

            ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 450, Easing.OutCirc);

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

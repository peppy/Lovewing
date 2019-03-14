using Lovewing.Graphics.Sprites;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using System;
using osu.Framework.Input.Events;

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

        protected override bool OnHover(HoverEvent args)
        {
            hover.FadeIn(200);
            return base.OnHover(args);
        }

        protected override void OnHoverLost(HoverLostEvent args)
        {
            hover.FadeOut(200);
            base.OnHoverLost(args);
        }

        protected override bool OnMouseDown(MouseDownEvent args)
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

            return base.OnMouseDown(args);
        }

        protected override bool OnMouseUp(MouseUpEvent args)
        {
            curRipple?.FadeOut(450)
                .Expire();

            curRipple = null;

            return base.OnMouseUp(args);
        }
    }
}

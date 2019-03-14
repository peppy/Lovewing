using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using System;
using osu.Framework.Input.Events;

namespace Lovewing.Graphics.UserInterface
{
    public class LovewingButton : Button
    {
        private Circle curRipple;

        protected Box Hover;

        public float TextSize
        {
            get => SpriteText.TextSize;
            set => SpriteText.TextSize = value;
        }

        public LovewingButton()
        {
            SpriteText.Font = @"Noto Sans CJK JP Regular";

            Height = 40;
            CornerRadius = 5;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Radius = 10,
                Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.05f)
            };

            AddRangeInternal(new Drawable[]
            {
                Hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColourInfo.SingleColour(Color4.White).MultiplyAlpha(0.2f),
                    Alpha = 0
                }
            });
        }

        protected override bool OnHover(HoverEvent args)
        {
            Hover.FadeIn(200);
            return base.OnHover(args);
        }

        protected override void OnHoverLost(HoverLostEvent args)
        {
            Hover.FadeOut(200);
            base.OnHoverLost(args);
        }

        protected override bool OnMouseDown(MouseDownEvent args)
        {
            var x = args.CurrentState.Mouse.Position.X - BoundingBox.X - BoundingBox.Width / 2;
            var y = args.CurrentState.Mouse.Position.Y - BoundingBox.Y - BoundingBox.Height / 2;

            Circle ripple;

            AddInternal(ripple = new Circle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                X = x,
                Y = y,
                Width = 10,
                Height = 10,
                Colour = ColourInfo.SingleColour(Color4.Gray).MultiplyAlpha(0.5f),
                Blending = BlendingMode.Additive
            });

            ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 650, Easing.OutCirc);

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

using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using System;

namespace Lovewing.Graphics.UserInterface
{
    public class LovewingButton : Button
    {
        private Circle curRipple;

        protected Box hover;

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
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColourInfo.SingleColour(Color4.White).MultiplyAlpha(0.2f),
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
            var x = state.Mouse.Position.X - BoundingBox.X - BoundingBox.Width / 2;
            var y = state.Mouse.Position.Y - BoundingBox.Y - BoundingBox.Height / 2;

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

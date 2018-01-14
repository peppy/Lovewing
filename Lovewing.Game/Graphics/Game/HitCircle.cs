using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Game
{
    public class HitCircle : ClickableContainer
    {
        public Action HitAction
        {
            set => Action = value;
        }

        public HitCircle()
        {
            Height = 128;
            Width = 128;

            AddInternal(new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                BorderThickness = 5,
                BorderColour = Color4.White,
                Masking = true,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(0, 0, 0, 0)
                }
            });
        }

        protected override bool OnClick(InputState state)
        {
            CircularContainer ripple;

            AddInternal(ripple = new CircularContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Masking = true,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                BorderThickness = 3,
                BorderColour = Color4.Gray,
                Alpha = 0.5f,
                Blending = BlendingMode.Additive,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(0, 0, 0, 0)
                }
            });

            ripple.ScaleTo(2, 450, Easing.OutCirc)
                .FadeOut(450)
                .Expire();

            return base.OnClick(state);
        }
    }
}

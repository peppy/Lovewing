using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input;
using OpenTK;
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
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
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
            Circle ripple;

            Add(ripple = new Circle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = 10,
                Width = 10,
                Colour = Color4.Gray,
                Alpha = 0.5f,
                Blending = BlendingMode.Additive
            });

            ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 450, Easing.OutCirc)
                .FadeOut(450)
                .Expire();

            return base.OnClick(state);
        }
    }
}

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
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
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore, LovewingColours lovewingColours)
        {
            AddInternal(new Circle
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                Colour = lovewingColours.LightMagenta,
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
                Blending = BlendingMode.Additive
            });

            ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 450, Easing.OutCirc)
                .FadeOut(450)
                .Expire();

            return base.OnClick(state);
        }
    }
}

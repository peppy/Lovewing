// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using System;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Effects;

namespace Lovewing.Game.Screens.Main
{
    public abstract class Wedge : OverlayContainer
    {
        protected abstract Color4 WedgeColor { get; }

        private readonly Container<Box> wedgeBackground;

        public Wedge()
        {
            Children = new Drawable[]{
                wedgeBackground = new Container<Box>
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    Shear = new Vector2(-0.1f, 0),
                    Colour = WedgeColor,
                    Children = new[]
                    {
                        new Box()
                        {
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.29f,
                            EdgeSmoothness = Vector2.One,
                        },
                        new Box()
                        {
                            RelativeSizeAxes = Axes.Y,
                            Colour = WedgeColor,
                            Alpha = 0.29f,
                            Width = 50,
                            EdgeSmoothness = Vector2.One,
                        }
                    }
                },

            };
        }

        protected override void PopIn()
        {
            wedgeBackground.ScaleTo(Vector2.One, 250, EasingTypes.OutQuad);
        }

        protected override void PopOut()
        {
            wedgeBackground.ScaleTo(new Vector2(0, 1), 250, EasingTypes.InQuad);
        }
    }
}

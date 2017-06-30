// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using System;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Lovewing.Game.Screens.Main
{
    public abstract class Wedge : OverlayContainer
    {
        protected abstract Color4 WedgeColor { get; }

        private readonly Container<Box> wedgeBackground;

        public Wedge()
        {
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomCentre;
            Children = new[]{
                wedgeBackground = new Container<Box>
                {
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
            ResizeWidthTo(1, 250, EasingTypes.OutQuad);
        }

        protected override void PopOut()
        {
            ResizeWidthTo(0, 250, EasingTypes.InQuad);
        }
    }
}

// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

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

        private Container<Box> wedgeBackground;
        private Container button;
        private WedgeButton buttonBox;
        private Container content;

        public override bool HandleInput => true;

        protected override bool BlockPassThroughMouse => false;

        protected override Container<Drawable> Content => content;

        private const float wedge_width = 50;

        protected override void LoadComplete()
        {
            InternalChildren = new Drawable[]{
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
                            Width = wedge_width,
                            EdgeSmoothness = Vector2.One,
                        }
                    }
                },
                button = new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Y,
                    Width = 100,
                    Shear = new Vector2(-0.1f, 0),
                    Masking = true,
                    Children = new Drawable[]
                    {
                        buttonBox = new WedgeButton(WedgeColor)
                        {
                            Shear = new Vector2(0.1f, 0),
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            Size = new Vector2(wedge_width * 1.1f),
                            Action = Show,
                        }
                    },
                },
                content = new Container
                {

                }
            };

            State = Visibility.Visible;
        }

        protected override void PopIn()
        {
            wedgeBackground.ScaleTo(Vector2.One, 250, EasingTypes.OutQuad);
            buttonBox.FadeColour(WedgeColor, 250);
            buttonBox.ResizeHeightTo(75, 250, EasingTypes.OutQuad);
        }

        protected override void PopOut()
        {
            wedgeBackground.ScaleTo(new Vector2(0, 1), 250, EasingTypes.InQuad);
            buttonBox.FadeColour(Color4.Gray, 250);
            buttonBox.ResizeHeightTo(50, 250, EasingTypes.InQuad);
        }

        private class WedgeButton : ClickableContainer
        {
            private readonly Color4 activeColor;

            public WedgeButton(Color4 activeColor)
            {
                this.activeColor = activeColor;

                Children = new Drawable[]
                {
                    new Box()
                    {
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        RelativeSizeAxes = Axes.Both,
                        Colour = activeColor,
                        Shear = new Vector2(-0.05f, 0.05f),
                        EdgeSmoothness = Vector2.One,
                        Width = 1.05f,
                    }
                };
            }
        }
    }
}

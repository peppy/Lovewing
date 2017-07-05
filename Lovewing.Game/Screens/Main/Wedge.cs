// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using System;

namespace Lovewing.Game.Screens.Main
{
    public abstract class Wedge : OverlayContainer
    {
        protected abstract Color4 WedgeColor { get; }

        private Container<Box> wedgeBackground;
        private Container content;

        //public override bool HandleInput => true;

        protected override Container<Drawable> Content => content;

        private const float wedge_width = 50;

        public Wedge()
        {
            AddInternal(content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                AlwaysPresent = true,
                Depth = -1,
            });
        }

        protected override void LoadComplete()
        {
            AddInternal(new Drawable[] {
                wedgeBackground = new Container<Box>
                {
                    AlwaysPresent = true,
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Width = 2,
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
            });

            State = Visibility.Visible;
        }

        public override bool Invalidate(Invalidation invalidation = Invalidation.All, Drawable source = null, bool shallPropagate = true)
        {
            if((invalidation & (Invalidation.DrawSize | Invalidation.MiscGeometry)) > 0)
            {
                content.Size = Vector2.Divide(LayoutSize, DrawSize);
            }

            return base.Invalidate(invalidation, source, shallPropagate);
        }

        protected override void PopIn()
        {
            wedgeBackground.MoveTo(new Vector2(0, 0), 250, EasingTypes.OutQuad);
            content.MoveTo(new Vector2(0, 0), 250, EasingTypes.OutQuad);
        }

        protected override void PopOut()
        {
            wedgeBackground.MoveTo(new Vector2(2, 0), 250, EasingTypes.InQuad);
            content.MoveTo(new Vector2(content.DrawSize.X, 0), 250, EasingTypes.InQuad);
        }

        public Drawable CreateButton()
        {
            WedgeButton button = new WedgeButton(WedgeColor)
            {
                Action = Show,
                RelativeSizeAxes = Axes.Both,
                Width = Width,
                Anchor = Anchor,
                Origin = Origin,
                Margin = Margin,
            };
            StateChanged += (con, vis) => button.Active = vis == Visibility.Visible;
            if (!IsLoaded)
                OnLoadComplete += drawable =>
                {
                    button.ActiveColor = WedgeColor;
                    button.Active = State == Visibility.Visible;
                };
            return button;
        }

        private class WedgeButton : Container
        {
            public Color4 ActiveColor;
            private readonly Box background;
            private readonly ClickableContainer clickCon;

            public bool Active
            {
                set
                {
                    background.FadeColour(value ? ActiveColor : Color4.Gray, 250);
                    clickCon.ResizeHeightTo(value ? 75 : 50, 250, value ? EasingTypes.OutQuad : EasingTypes.InQuad);
                }
            }

            public Action Action;

            public WedgeButton(Color4 activeColor)
            {
                ActiveColor = activeColor;

                Child = new Container
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Y,
                    Width = 100,
                    Shear = new Vector2(-0.1f, 0),
                    Masking = true,
                    Child = clickCon = new ClickableContainer
                    {
                        Shear = new Vector2(0.1f, 0),
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        Size = new Vector2(wedge_width * 1.1f),
                        Action = () => Action?.Invoke(),
                        Children = new Drawable[]
                        {
                            background = new Box
                            {
                                Anchor = Anchor.BottomRight,
                                Origin = Anchor.BottomRight,
                                RelativeSizeAxes = Axes.Both,
                                Shear = new Vector2(-0.05f, 0.05f),
                                Width = 0.95f,
                                Colour = activeColor,
                                EdgeSmoothness = Vector2.One,
                            },
                        }
                    },
                };
            }
        }
    }
}

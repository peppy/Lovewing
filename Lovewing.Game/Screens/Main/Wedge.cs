// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Extensions.Color4Extensions;
using System;
using Lovewing.Game.Graphics;
using osu.Framework.Input;

namespace Lovewing.Game.Screens.Main
{
    public abstract class Wedge : OverlayContainer
    {
        protected abstract Color4 WedgeColour { get; }
        protected abstract Color4 ButtonColour { get; }
        protected abstract FontAwesome ButtonIcon { get; }
        protected abstract string ButtonText { get; }

        private Container<Box> wedgeBackground;
        private readonly Container content;

        public Action WedgeClick { get; set; }

        //public override bool HandleInput => true;

        protected override Container<Drawable> Content => content;

        private const float wedge_width = 50;

        protected Wedge()
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
            AddRangeInternal(new Drawable[]
            {
                wedgeBackground = new Container<Box>
                {
                    AlwaysPresent = true,
                    Alpha = 0,
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Width = 2,
                    Shear = new Vector2(-0.1f, 0),
                    Colour = WedgeColour,
                    Children = new[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.29f,
                            EdgeSmoothness = Vector2.One
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Y,
                            Colour = WedgeColour,
                            Alpha = 0.29f,
                            Width = wedge_width,
                            EdgeSmoothness = Vector2.One
                        }
                    }
                },
            });

            State = Visibility.Visible;
        }

        public override bool Invalidate(Invalidation invalidation = Invalidation.All, Drawable source = null, bool shallPropagate = true)
        {
            if ((invalidation & (Invalidation.DrawSize | Invalidation.MiscGeometry)) > 0)
                content.Size = Vector2.Divide(LayoutSize, DrawSize);

            return base.Invalidate(invalidation, source, shallPropagate);
        }

        protected override void PopIn()
        {
            wedgeBackground.MoveTo(new Vector2(0), 250, Easing.OutQuad);
            content.MoveTo(new Vector2(0), 250, Easing.OutQuad);
        }

        protected override void PopOut()
        {
            wedgeBackground
                .MoveTo(new Vector2(2, 0), 250, Easing.InQuad)
                .FadeOut(250);

            content.MoveTo(new Vector2(content.DrawSize.X, 0), 250, Easing.InQuad);
        }

        protected override bool OnClick(InputState state)
        {
            WedgeClick?.Invoke();
            return base.OnClick(state);
        }

        public Drawable CreateButton()
        {
            var button = new WedgeButton
            {
                ActiveColour = WedgeColour,
                Text = ButtonText,
                Action = Show,
                RelativeSizeAxes = Axes.Both,
                Width = Width,
                Anchor = Anchor,
                Origin = Origin,
                Margin = Margin,
                
            };

            StateChanged += vis => button.Active = vis == Visibility.Visible;

            if (!IsLoaded)
                OnLoadComplete += drawable =>
                {
                    button.ActiveColour = ButtonColour;
                    button.Active = State == Visibility.Visible;
                    button.ButtonIcon.Icon = ButtonIcon;
                };
            return button;
        }

        public void Expand()
        {
            wedgeBackground?
                .MoveTo(new Vector2(-0.75f, 0), 250, Easing.InQuad)
                .FadeInFromZero(250);

            content
                .MoveTo(new Vector2(content.DrawSize.X, 0), 250, Easing.InQuad)
                .FadeInFromZero(250);
        }

        private class WedgeButton : Container
        {
            private readonly SpriteText buttonText;
            public readonly SpriteIcon ButtonIcon;
            private readonly Box background;
            private readonly ClickableContainer clickCon;

            public Color4 ActiveColour { get; set; }
            public string Text
            {
                get { return buttonText.Text; }
                set { buttonText.Text = value; }
            }

            public bool Active
            {
                set
                {
                    background.FadeColour(value ? ActiveColour : Color4.Gray, 250);
                    clickCon.ResizeHeightTo(value ? 75 : 60, 250, value ? Easing.OutQuad : Easing.InQuad);
                    ButtonIcon.MoveToY(value ? -10 : 0, 250, value ? Easing.OutQuad : Easing.InQuad);
                    ButtonIcon.ScaleTo(value ? 0.6f : 0.7f, 250, value ? Easing.OutQuad : Easing.InQuad);
                    if (value)
                        buttonText.FadeIn(250, Easing.OutQuad);
                    else
                        buttonText.FadeOut(250, Easing.InQuad);
                }
            }

            public Action Action;

            public WedgeButton()
            {

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
                        Masking = true,
                        EdgeEffect = new EdgeEffectParameters
                        {
                            Colour = Color4.Black.Opacity(0.05f),
                            Type = EdgeEffectType.Shadow,
                            Radius = 10
                        },
                        Children = new Drawable[]
                        {
                            background = new Box
                            {
                                Anchor = Anchor.BottomRight,
                                Origin = Anchor.BottomRight,
                                RelativeSizeAxes = Axes.Both,
                                Shear = new Vector2(-0.05f, 0.05f),
                                Width = 0.95f,
                                Colour = ActiveColour,
                                EdgeSmoothness = Vector2.One
                            },
                            new Box
                            {
                                Anchor = Anchor.BottomRight,
                                Origin = Anchor.BottomRight,
                                RelativeSizeAxes = Axes.Both,
                                Shear = new Vector2(-0.05f, 0.05f),
                                Width = 0.95f,
                                Colour = Color4.White.Opacity(0.1f),
                                Alpha = 0,
                            },
                            ButtonIcon = new SpriteIcon
                            {
                                RelativeSizeAxes = Axes.Both,
                                Y = -10,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                FillMode = FillMode.Fit,
                                Colour = Color4.White
                            },
                            buttonText = new SpriteText
                            {
                                Y = 25,
                                Shadow = true,
                                AllowMultiline = false,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                TextSize = 20,
                            }
                        }
                    }
                };

                buttonText.Hide();
            }
        }
    }
}

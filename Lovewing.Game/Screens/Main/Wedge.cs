// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.IO.Stores;
using System;

namespace Lovewing.Game.Screens.Main
{
    public abstract class Wedge : OverlayContainer
    {
        protected abstract Color4 WedgeColor { get; }
        protected abstract Color4 ButtonColor { get; }
        protected abstract Texture ButtonIcon { get; }

        private Container<Box> wedgeBackground;
        private Container content;
        private FontStore fstore;

        //public override bool HandleInput => true;

        protected override Container<Drawable> Content => content;

        private const float wedgeWidth = 50;

        [BackgroundDependencyLoader]
        private void load(FontStore store)
        {
            fstore = store;
        }

        public void Expand()
        {
            wedgeBackground?.MoveTo(new Vector2(-0.75f, 0), 250, EasingTypes.InQuad);
        }

        public Wedge()
        {
            AddInternal(content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                AlwaysPresent = true,
                Depth = -1
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
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.29f,
                            EdgeSmoothness = Vector2.One
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Y,
                            Colour = WedgeColor,
                            Alpha = 0.29f,
                            Width = wedgeWidth,
                            EdgeSmoothness = Vector2.One
                        }
                    }
                }
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
            wedgeBackground.MoveTo(new Vector2(0, 0), 250, EasingTypes.OutQuad);
            content.MoveTo(new Vector2(0, 0), 250, EasingTypes.OutQuad);
        }

        protected override void PopOut()
        {
            wedgeBackground.MoveTo(new Vector2(2, 0), 250, EasingTypes.InQuad);
            content.MoveTo(new Vector2(content.DrawSize.X, 0), 250, EasingTypes.InQuad);
        }

        public Drawable CreateButton(string text = "")
        {
            var button = new WedgeButton(WedgeColor, text)
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
                    button.ActiveColor = ButtonColor;
                    button.Active = State == Visibility.Visible;
                    button.buttonIcon.Texture = ButtonIcon;
                };
            return button;
        }

        private class WedgeButton : Container
        {
            public Color4 ActiveColor;
            private readonly SpriteText buttonText;
            public readonly Sprite buttonIcon;
            private readonly Box hover, background;
            private readonly ClickableContainer clickCon;

            public bool Active
            {
                set
                {
                    background.FadeColour(value ? ActiveColor : Color4.Gray, 250);
                    clickCon.ResizeHeightTo(value ? 75 : 60, 250, value ? EasingTypes.OutQuad : EasingTypes.InQuad);
                    buttonIcon.MoveToY(value ? -10 : 0, 250, value ? EasingTypes.OutQuad : EasingTypes.InQuad);
                    buttonIcon.ScaleTo(value ? 0.6f : 0.7f, 250, value ? EasingTypes.OutQuad : EasingTypes.InQuad);
                    if (value)
                        buttonText.FadeIn(250, EasingTypes.OutQuad);
                    else
                        buttonText.FadeOut(250, EasingTypes.InQuad);
                }
            }

            public Action Action;

            public WedgeButton(Color4 activeColor, string text = "")
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
                        Size = new Vector2(wedgeWidth * 1.1f),
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
                                Colour = activeColor,
                                EdgeSmoothness = Vector2.One
                            },
                            hover = new Box
                            {
                                Anchor = Anchor.BottomRight,
                                Origin = Anchor.BottomRight,
                                RelativeSizeAxes = Axes.Both,
                                Shear = new Vector2(-0.05f, 0.05f),
                                Width = 0.95f,
                                Colour = Color4.White.Opacity(0.1f),
                                Alpha = 0,
                                BlendingMode = BlendingMode.Additive
                            },
                            buttonIcon = new Sprite
                            {
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
                                Text = text
                            }
                        }
                    }
                };

                buttonText.Hide();
            }
        }
    }
}

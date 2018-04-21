using Lovewing.Graphics.Sprites;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using System;

namespace Lovewing.Graphics.Containers
{
    public class TabContainer : OverlayContainer
    {
        public ColourInfo TabColour { get; set; }
        public ColourInfo ButtonColour { get; set; }
        public Action ButtonAction { get; set; }
        public FontAwesome ButtonIcon { get; set; }
        public string ButtonText { get; set; }

        private Container<Box> tabBackground;
        private readonly Container content;

        protected override Container<Drawable> Content => content;

        private const float tab_width = 50;

        public TabContainer()
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
            AddRangeInternal(new Drawable[]
            {
                tabBackground = new BufferedContainer<Box>
                {
                    AlwaysPresent = true,
                    Alpha = 0,
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    RelativePositionAxes = Axes.Both,
                    Width = 2,
                    Shear = new Vector2(-0.1f, 0),
                    Colour = TabColour,
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
                            Colour = TabColour,
                            Alpha = 0.29f,
                            Width = tab_width,
                            EdgeSmoothness = Vector2.One
                        }
                    }
                }
            });

            State = Visibility.Visible;
        }

        protected override void PopIn()
        {
            tabBackground.MoveTo(Vector2.Zero, 250, Easing.OutQuad)
                .FadeOutFromOne(500);

            content.MoveTo(Vector2.Zero, 250, Easing.OutQuad);
        }

        protected override void PopOut()
        {
            tabBackground.MoveTo(new Vector2(2, 0), 250, Easing.InQuad)
                .FadeOut(500);

            content.MoveTo(new Vector2(content.DrawSize.X, 0), 250, Easing.InQuad);
        }

        public TabButton CreateButton()
        {
            var button = new TabButton
            {
                ActiveColour = TabColour,
                Text = ButtonText,
                Action = () =>
                {
                    if (ButtonAction != null)
                        ButtonAction.Invoke();
                    else
                        Show();
                },
                RelativeSizeAxes = Axes.Both,
                Width = Width,
                Anchor = Anchor,
                Origin = Origin,
                Margin = Margin
            };

            StateChanged += vis => button.Active = vis == Visibility.Visible;

            if (!IsLoaded)
            {
                OnLoadComplete += drawable =>
                {
                    button.ActiveColour = ButtonColour;
                    button.Active = State == Visibility.Visible;
                    button.ButtonIcon.Icon = ButtonIcon;
                };
            }

            return button;
        }

        public class TabButton : Container
        {
            private readonly SpriteText buttonText;
            private readonly Box background;
            private readonly ClickableContainer clickableContainer;
            public readonly SpriteIcon ButtonIcon;

            public ColourInfo ActiveColour { get; set; }
            public string Text
            {
                get => buttonText.Text;
                set => buttonText.Text = value;
            }

            public bool Active
            {
                set
                {
                    background.FadeColour(value ? ActiveColour : (ColourInfo)Color4.Gray, 250);
                    clickableContainer.ResizeHeightTo(value ? 75 : 60, 250, value ? Easing.OutQuad : Easing.InQuad);
                    ButtonIcon.MoveToY(value ? -10 : 0, 250, value ? Easing.OutQuad : Easing.InQuad);
                    ButtonIcon.ScaleTo(value ? 0.6f : 0.7f, 250, value ? Easing.OutQuad : Easing.InQuad);

                    if (value)
                        buttonText.FadeIn(250, Easing.OutQuad);
                    else
                        buttonText.FadeOut(250, Easing.InQuad);
                }
            }

            public Action Action;

            public TabButton()
            {
                Child = new Container
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Y,
                    Width = 100,
                    Shear = new Vector2(-0.1f, 0),
                    Masking = true,
                    Child = clickableContainer = new ClickableContainer
                    {
                        Shear = new Vector2(0.1f, 0),
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        Size = new Vector2(tab_width * 1.2f),
                        Action = () => Action?.Invoke(),
                        Masking = true,
                        EdgeEffect = new EdgeEffectParameters
                        {
                            Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.05f),
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
                                Colour = ColourInfo.SingleColour(Color4.White).MultiplyAlpha(0.1f),
                                Alpha = 0
                            },
                            ButtonIcon = new SpriteIcon
                            {
                                RelativeSizeAxes = Axes.Both,
                                Y = -10,
                                // Margin = new MarginPadding { Bottom = 10 },
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                FillMode = FillMode.Fit,
                                Colour = Color4.White,
                            },
                            buttonText = new SpriteText
                            {
                                Y = 25,
                                // Margin = new MarginPadding { Top = 25 },
                                Shadow = true,
                                AllowMultiline = false,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                TextSize = 20
                            }
                        }
                    }
                };

                buttonText.Hide();
            }
        }
    }
}

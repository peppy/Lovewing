using Lovewing.Graphics;
using Lovewing.Graphics.Containers;
using Lovewing.Graphics.Sprites;
using Lovewing.Graphics.UserInterface;
using Lovewing.Screens.Game;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using System;
using osu.Framework.Input.Events;

namespace Lovewing.Screens
{
    public class MainScreen : Screen
    {
        private Container<TabContainer> tabs;
        private TabContainer homeTab;
        private TabContainer idolTab;
        private TabContainer liveshowTab;
        private GameScreen gameScreen;
        private SongSelectorScreen songSelectorScreen;

        public override void OnEntering(IScreen last)
        {
            LoadComponentAsync(gameScreen = new GameScreen());
            LoadComponentAsync(songSelectorScreen = new SongSelectorScreen());
        }

        protected override bool OnClick(ClickEvent state)
        {
            if (((LovewingGame) Game).Sidebar.IsOpen)
                ((LovewingGame) Game).Sidebar.Toggle();

            return base.OnClick(state);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddRangeInternal(new Drawable[]
            {
                new Background(@"Backgrounds/mainmenu"),
                tabs = new Container<TabContainer>
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        liveshowTab = new TabContainer
                        {
                            Margin = new MarginPadding { Right = 100 },
                            ButtonText = @"Live",
                            ButtonIcon = FontAwesome.fa_music,
                            TabColour = LovewingColours.Blue,
                            ButtonColour = LovewingColours.LightBlue,
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            ButtonAction = () => this.Push(songSelectorScreen)
                        },
                        idolTab = new TabContainer
                        {
                            Margin = new MarginPadding { Right = 50 },
                            ButtonText = @"Idols",
                            ButtonIcon = FontAwesome.fa_users,
                            TabColour = LovewingColours.Yellow,
                            ButtonColour = LovewingColours.LightYellow,
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f
                        },
                        homeTab = new TabContainer
                        {
                            ButtonText = @"Home",
                            ButtonIcon = FontAwesome.fa_home,
                            TabColour = LovewingColours.Magenta,
                            ButtonColour = LovewingColours.LightMagenta,
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    Margin = new MarginPadding
                                    {
                                        Bottom = 235,
                                        Left = 170
                                    },
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children = new Drawable[]
                                    {
                                        new InboxButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Margin = new MarginPadding { Right = 620 }
                                        },
                                        new StoryButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre
                                        }
                                    }
                                },
                                new Container
                                {
                                    Margin = new MarginPadding
                                    {
                                        Top = 235,
                                        Right = 50
                                    },
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children = new Drawable[]
                                    {
                                        new EventsButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            liveshowTab.StateChanged += vis => selectTab(liveshowTab, vis);
            idolTab.StateChanged += vis => selectTab(idolTab, vis);
            homeTab.StateChanged += vis => selectTab(homeTab, vis);

            AddRangeInternal(new[]
            {
                liveshowTab.CreateButton(),
                idolTab.CreateButton(),
                homeTab.CreateButton()
            });
        }

        private void selectTab(VisibilityContainer container, Visibility visibility)
        {
            if (visibility == Visibility.Visible)
            {
                if (container == homeTab)
                {
                    idolTab.Hide();
                    liveshowTab.Hide();
                }
                else if (container == liveshowTab)
                {
                    idolTab.Hide();
                    homeTab.Hide();
                }
                else if (container == idolTab)
                {
                    homeTab.Hide();
                    liveshowTab.Hide();
                }
            }
        }

        private class InboxButton : ClickableContainer
        {
            private readonly Circle circ;
            private readonly Circle hover;
            private Circle curRipple;

            public InboxButton()
            {
                Height = 200;
                Width = 200;

                AddRangeInternal(new Drawable[]
                {
                    circ = new Circle
                    {
                        Colour = LovewingColours.White,
                        RelativeSizeAxes = Axes.Both
                    },
                    new SpriteIcon
                    {
                        Icon = FontAwesome.fa_envelope_o,
                        Colour = LovewingColours.Magenta,
                        Height = 100,
                        Width = 100,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Margin = new MarginPadding { Bottom = 20 }
                    },
                    new SpriteText
                    {
                        Text = @"Inbox",
                        Colour = LovewingColours.Magenta,
                        TextSize = 30,
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        Margin = new MarginPadding { Bottom = 20 }
                    },
                    hover = new Circle
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = ColourInfo.SingleColour(Color4.White).MultiplyAlpha(0.2f),
                        Alpha = 0
                    }
                });
            }

            protected override bool OnHover(HoverEvent args)
            {
                hover.FadeIn(200);
                return base.OnHover(args);
            }

            protected override void OnHoverLost(HoverLostEvent args)
            {
                hover.FadeOut(200);
                base.OnHoverLost(args);
            }

            protected override bool OnMouseDown(MouseDownEvent args)
            {
                Circle ripple;

                AddInternal(ripple = new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Width = 10,
                    Height = 10,
                    Colour = ColourInfo.SingleColour(Color4.Gray).MultiplyAlpha(0.5f),
                    Blending = BlendingMode.Additive
                });

                ripple.ScaleTo(Math.Max(Size.X, Size.Y) / 5, 650, Easing.OutCirc);

                curRipple = ripple;

                return base.OnMouseDown(args);
            }

            protected override bool OnMouseUp(MouseUpEvent args)
            {
                curRipple?.FadeOut(450)
                    .Expire();

                curRipple = null;

                return base.OnMouseUp(args);
            }
        }

        private class StoryButton : LovewingButton
        {
            public StoryButton()
            {
                Height = 200;
                Width = 380;
                BackgroundColour = LovewingColours.Magenta;

                AddRange(new Drawable[]
                {
                    new SpriteIcon
                    {
                        Icon = FontAwesome.fa_film,
                        Height = 80,
                        Width = 80,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding
                        {
                            Left = 15,
                            Bottom = 25
                        },
                        Colour = Color4.White
                    },
                    new SpriteText
                    {
                        Text = @"Story",
                        Anchor = Anchor.BottomLeft,
                        Origin = Anchor.BottomLeft,
                        Colour = Color4.White,
                        Margin = new MarginPadding
                        {
                            Left = 25,
                            Bottom = 15
                        },
                        TextSize = 30
                    },
                    new Box
                    {
                        Shear = new Vector2(-0.4f, 0),
                        Colour = LovewingColours.LightMagenta,
                        RelativeSizeAxes = Axes.Both,
                        Margin = new MarginPadding { Left = 85 }
                    }
                });
            }
        }

        private class EventsButton : LovewingButton
        {
            public EventsButton()
            {
                Height = 200;
                Width = 600;
                BackgroundColour = LovewingColours.Magenta;

                AddRange(new Drawable[]
                {
                    new SpriteIcon
                    {
                        Icon = FontAwesome.fa_angle_right,
                        Height = 100,
                        Width = 100,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Bottom = 50 },
                        Colour = Color4.White
                    },
                    new SpriteText
                    {
                        Text = @"Events",
                        Anchor = Anchor.BottomLeft,
                        Origin = Anchor.BottomLeft,
                        Colour = Color4.White,
                        Margin = new MarginPadding(15),
                        TextSize = 40
                    },
                    new Box
                    {
                        Shear = new Vector2(-0.7f, 0),
                        Colour = LovewingColours.LightMagenta,
                        RelativeSizeAxes = Axes.Both,
                        Margin = new MarginPadding { Left = 75 }
                    }
                });
            }
        }
    }
}

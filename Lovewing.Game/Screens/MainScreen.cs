// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.UserInterface;
using Lovewing.Game.Graphics.Overlay;
using Lovewing.Game.Screens.Main;
using Lovewing.Game.Screens.Liveshow;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK;
using System.Linq;
using osu.Framework.Input;
using osu.Framework.Screens;

namespace Lovewing.Game.Screens
{
    public class MainScreen : Screen
    {
        private readonly LovewingToolbar toolbar;
        private readonly LovewingSidebar sidebar;
        private readonly Sprite idol;
        private readonly Container<Wedge> wedgeContainer;
        private readonly LovewingColours colours = new LovewingColours();
        private readonly Inbox inboxOverlay;

        private void toggleSideBar()
        {
            if (sidebar.State == Visibility.Visible)
                sidebar.Hide();
            else
                sidebar.Show();
        }

        private void hideOverlays()
        {
            if (sidebar.State == Visibility.Visible)
                sidebar.Hide();

            if (inboxOverlay.State == Visibility.Visible)
                inboxOverlay.Hide();
        }

        private void toggleInbox()
        {
            if (inboxOverlay.State == Visibility.Visible)
                inboxOverlay.Hide();
            else
                inboxOverlay.Show();
        }

        private void toLiveshow()
        {
            var screen = new LiveshowScreen();

            Push(screen);
        }

        public MainScreen()
        {
            Wedge liveshow, management, home;

            AddRange(new Drawable[]
            {
                new Background(@"Backgrounds/mainmenu")
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                idol = new Sprite
                {
                    Margin = new MarginPadding { Left = 100 },
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    FillMode = FillMode.Fit,
                    Scale = new Vector2(0.9f),
                },
                sidebar = new LovewingSidebar
                {
                    Origin = Anchor.CentreRight,
                    Anchor = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Y,
                    Depth = -2,
                },
                toolbar = new LovewingToolbar
                {
                    Depth = -1,
                    ButtonAction = toggleSideBar,
                },
                inboxOverlay = new Inbox(),
                wedgeContainer = new Container<Wedge>
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        liveshow = new LiveShowWedge
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Margin = new MarginPadding { Right = 100 },
                            Depth = 2,
                            ButtonAction = toLiveshow,
                            /*Children = new[]
                            {
                                new Container
                                {
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children = new Drawable[]
                                    {
                                        new LovewingButton
                                        {
                                            TextSize = 75,
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            Text = "Solo",
                                            Size = new Vector2(500, 200),
                                            BackgroundColour = colours.Blue,
                                            HoverColour = colours.Blue,
                                        },
                                        new LovewingButton
                                        {
                                            TextSize = 75,
                                            Action = matchmaking,
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre,
                                            Text = "Multiplayer",
                                            Size = new Vector2(500, 200),
                                            BackgroundColour = colours.Blue,
                                            HoverColour = colours.Blue,
                                        }
                                    }
                                }
                            }*/
                        },
                        management = new IdolManagementWedge
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Margin = new MarginPadding { Right = 50 },
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children = new Drawable[]
                                    {
                                        new LovewingButton
                                        {
                                            TextSize = 60,
                                            Size = new Vector2(630, 200),
                                            Text = "Button",
                                            BackgroundColour = colours.Yellow,
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                        },
                                        new LovewingButton
                                        {
                                            TextSize = 60,
                                            Size = new Vector2(630, 200),
                                            Text = "Another Button",
                                            BackgroundColour = colours.Yellow,
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre,
                                        }
                                    }
                                }
                            }
                        },
                        home = new HomeWedge
                        {
                            WedgeClick = hideOverlays,
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Children = new[]
                            {
                                new Container
                                {
                                    X = -100,
                                    Y = -220,
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children =  new Drawable[]
                                    {
                                        new LovewingDoubleButton
                                        {
                                            Angle = -0.4f,
                                            ShearPosition = new Vector2(140, 0),
                                            TextPosition = new Vector2(-125, 60),
                                            TextSize = 30,
                                            CornerRadius = 5,
                                            Size = new Vector2(400, 200),
                                            Text = "Story",
                                            BackgroundColour = colours.Magenta,
                                            ShearColour = colours.LightMagenta,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                        },
                                        new LovewingButton
                                        {
                                            Action = toggleInbox,
                                            TextPosition = new Vector2(0, 60),
                                            TextSize = 30,
                                            CornerRadius = 100,
                                            Size = new Vector2(200, 200),
                                            Text = "Inbox",
                                            BackgroundColour = colours.White,
                                            HoverColour = colours.Magenta,
                                            TextColour = colours.Magenta,
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            Icon = FontAwesome.fa_envelope_o,
                                            IconColour = colours.Magenta,
                                            IconSize = new Vector2(80),
                                            IconPosition = new Vector2(0, -10),
                                        }
                                    }
                                },
                                new Container
                                {
                                    Y = 235f,
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Child = new LovewingDoubleButton
                                    {
                                        Angle = -0.7f,
                                        ShearPosition = new Vector2(200, 0),
                                        TextPosition = new Vector2(-200, 60),
                                        TextSize = 40,
                                        CornerRadius = 5,
                                        Size = new Vector2(630, 200),
                                        Text = "Events",
                                        BackgroundColour = colours.Magenta,
                                        ShearColour = colours.LightMagenta,
                                        Anchor = Anchor.BottomCentre,
                                        Origin = Anchor.BottomCentre,
                                    }
                                }
                            }
                        }
                    }
                }
            });

            /*inbox.Add(new Circle
            {
                Y = -40,
                X = 40,
                Size = new Vector2(25),
                Colour = Color4.Red,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
            });*/

            home.StateChanged += vis => selectWedge(home, vis);
            management.StateChanged += vis => selectWedge(management, vis);
            liveshow.StateChanged += vis => selectWedge(liveshow, vis);

            AddRange(new[]
            {
                liveshow.CreateButton(),
                management.CreateButton(),
                home.CreateButton(),
            });
        }

        protected override void OnEntering(Screen last)
        {
            Content.FadeIn(400);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOut(400);
            return base.OnExiting(next);
        }

        protected override bool OnClick(InputState state)
        {
            hideOverlays();

            return base.OnClick(state);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore, UserData user) => idol.Texture = texStore.Get(user.Idol);

        private void selectWedge(VisibilityContainer con, Visibility vis)
        {
            if (vis == Visibility.Visible)
                wedgeContainer.Children.Where(child => child != con).ToList().ForEach(wedge => wedge.Hide());
        }
    }
}

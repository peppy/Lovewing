// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.UserInterface;
using Lovewing.Game.Graphics.Overlay;
using Lovewing.Game.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK;
using System.Linq;
using osu.Framework.Input;

namespace Lovewing.Game.Screens
{
    public class MainScreen : LovewingScreen
    {
        private readonly Sprite idol;
        private readonly LovewingButton inbox;   
        private readonly Container<Wedge> wedgeContainer;
        private readonly LovewingColors colors = new LovewingColors();
        private readonly LiveShowWedge liveshow;

        private Inbox inboxOverlay;

        private void hideOverlays()
        {
            if (Sidebar.State == Visibility.Visible)
                Sidebar.Hide();

            if (inboxOverlay.State == Visibility.Visible)
                inboxOverlay.Hide();
        }

        private void showOverlays()
        {
            if (Sidebar.State == Visibility.Hidden)
                Sidebar.Show();

            if (inboxOverlay.State == Visibility.Hidden)
                inboxOverlay.Show();
        }

        private void toggleInbox()
        {
            if (inboxOverlay.State == Visibility.Visible)
                inboxOverlay.Hide();
            else
                inboxOverlay.Show();
        }

        private void solo()
        {
            liveshow.Expand();
        }

        private void matchmaking()
        {
            Push(new MatchmakingScreen());
        }

        public MainScreen()
        {
            Wedge home, management; // switch back to field if needed. just for appveyor rn

            AddRange(new Drawable[]
            {
                new Background
                {
                    TextureName = @"Backgrounds/mainmenu",
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                idol = new Sprite
                {
                    Margin = new MarginPadding { Left = 100 },
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    FillMode = FillMode.Fit,
                    Scale = new Vector2(0.9f)
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
                            Children = new[]
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
                                            Action = solo,
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            Text = "Solo",
                                            Size = new Vector2(500, 200),
                                            BackgroundColour = colors.Blue,
                                            HoverColour = colors.Blue
                                        },
                                        new LovewingButton
                                        {
                                            TextSize = 75,
                                            Action = matchmaking,
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre,
                                            Text = "Multiplayer",
                                            Size = new Vector2(500, 200),
                                            BackgroundColour = colors.Blue,
                                            HoverColour = colors.Blue
                                        }
                                    }
                                }
                            }
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
                                            BackgroundColour = colors.Yellow,
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre
                                        },
                                        new LovewingButton
                                        {
                                            TextSize = 60,
                                            Size = new Vector2(630, 200),
                                            Text = "Another Button",
                                            BackgroundColour = colors.Yellow,
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre
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
                                            ShearRotation = -0.4f,
                                            ShearX = 140,
                                            TextX = -125,
                                            TextSize = 30,
                                            CornerRadius = 5,
                                            Size = new Vector2(400, 200),
                                            Text = "Story",
                                            BackgroundColour = colors.Magenta,
                                            ShearColour = colors.LightMagenta,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                        },
                                        inbox = new LovewingButton
                                        {
                                            Action = toggleInbox,
                                            TextY = 60,
                                            TextSize = 30,
                                            CornerRadius = 100,
                                            Size = new Vector2(200, 200),
                                            Text = "Inbox",
                                            BackgroundColour = colors.White,
                                            HoverColour = colors.Magenta,
                                            TextColour = colors.Magenta,
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            Icon = FontAwesome.fa_envelope_o,
                                            IconColour = colors.Magenta,
                                            IconSize = new Vector2(80),
                                            IconY = -10,
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
                                        ShearRotation = -0.7f,
                                        ShearX = 200,
                                        TextX = -200,
                                        TextSize = 40,
                                        CornerRadius = 5,
                                        Size = new Vector2(630, 200),
                                        Text = "Events",
                                        BackgroundColour = colors.Magenta,
                                        ShearColour = colors.LightMagenta,
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

        protected override bool OnClick(InputState state)
        {
            if (inboxOverlay.State == Visibility.Visible)
                inboxOverlay.Hide();

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

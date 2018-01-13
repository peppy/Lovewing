// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.Overlay;
using Lovewing.Game.Graphics.UserInterface;
using Lovewing.Game.Online;
using Lovewing.Game.Screens.Game;
using Lovewing.Game.Screens.Liveshow.Matchmaking;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Screens;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Screens.Liveshow
{
    public class LiveshowScreen : Screen
    {
        private readonly LovewingToolbar toolbar;
        private readonly LovewingSidebar sidebar;
        private readonly Box bar;
        private readonly Box mmBtnBox;
        private readonly Box soloBtnBox;
        private readonly Container matchmaking;
        private readonly Container solo;
        private string currentTab = "matchmaking";

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
        }

        private void toSolo()
        {
            if (currentTab == "solo") return;

            currentTab = "solo";

            bar.FadeColour(Color4.Blue, 250, Easing.InQuad);
            solo.MoveToX(0, 250, Easing.InQuad);
            soloBtnBox.FadeIn(250);
            mmBtnBox.FadeOut(250);
            matchmaking.MoveToX(DrawWidth, 250, Easing.InQuad);
        }

        private void toMatchmaking()
        {
            if (currentTab == "matchmaking") return;

            bar.FadeColour(Color4.Orange, 250, Easing.OutQuad);
            currentTab = "matchmaking";
            matchmaking.MoveToX(0, 250, Easing.OutQuad);
            mmBtnBox.FadeIn(250);
            soloBtnBox.FadeOut(250);
            solo.MoveToX(-DrawWidth, 250, Easing.OutQuad);
        }


        public LiveshowScreen()
        {

            LobbyTabControl<LobbyTabs> tabs;
            ScrollContainer tabContent;

            AddRangeInternal(new Drawable[]
            {
                new Background(@"https://i.imgur.com/0ywSXS7.png")
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BlurSigma = new Vector2(5)
                },
                sidebar = new LovewingSidebar
                {
                    Origin = Anchor.CentreRight,
                    Anchor = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.Y,
                    Depth = -2
                },
                toolbar = new LovewingToolbar
                {
                    Margin = new MarginPadding
                    {
                        Top = 10
                    },
                    Depth = -1,
                    ButtonAction = toggleSideBar
                },
                bar = new Box
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 10,
                    Colour = Color4.Orange
                },
                new FillFlowContainer
                {
                    Height = 60,
                    RelativeSizeAxes = Axes.X,
                    Margin = new MarginPadding { Top = 10 },
                    Spacing = Vector2.One,
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Children = new Drawable[]
                    {
                        new IconButton
                        {
                            Icon = FontAwesome.fa_chevron_left,
                            Action = Exit,
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(200, 40)
                        },
                        new ClickableContainer
                        {
                            Action = toSolo,
                            Size = new Vector2(250, 60),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Children = new Drawable[]
                            {
                                new SpriteIcon
                                {
                                    Margin = new MarginPadding { Left = 10 },
                                    Icon = FontAwesome.fa_user,
                                    Size = new Vector2(30),
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Depth = 0
                                },
                                new SpriteText
                                {
                                    TextSize = 30,
                                    Text = "Solo",
                                    Origin = Anchor.Centre,
                                    Anchor = Anchor.Centre,
                                    Depth = 0
                                },
                                soloBtnBox = new Box
                                {
                                    Alpha = 0,
                                    Size = new Vector2(250, 60),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Colour = Color4.Blue,
                                    Depth = 1
                                }
                            }
                        },
                        new ClickableContainer
                        {
                            Action = toMatchmaking,
                            Size = new Vector2(250, 60),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Children = new Drawable[]
                            {
                                new SpriteIcon
                                {
                                    Margin = new MarginPadding { Left = 10 },
                                    Icon = FontAwesome.fa_users,
                                    Size = new Vector2(30),
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Depth = 0
                                },
                                new SpriteText
                                {
                                    TextSize = 30,
                                    Text = "Group Stage",
                                    Origin = Anchor.Centre,
                                    Anchor = Anchor.Centre,
                                    Depth = 0
                                },
                                mmBtnBox = new Box
                                {
                                    Size = new Vector2(250, 60),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Colour = Color4.Orange,
                                    Depth = 1
                                }
                            }
                        }
                    }
                },
                solo = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding
                    {
                        Left = 300,
                        Top = 300,
                        Right = 300
                    },
                    Children = new Drawable[]
                    {
                        new ClickableContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(128, 128),
                            Child = new Box
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both
                            },
                            Action = () =>
                            {
                                var screen = new GameScreen();

                                Push(screen);
                            }
                        }
                    }
                },
                matchmaking = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding
                    {
                        Left = 300,
                        Top = 300,
                        Right = 300
                    },
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Margin = new MarginPadding
                            {
                                Left = 200,
                                Right = 200
                            },
                            RelativeSizeAxes = Axes.Y,
                            Width = 825,
                            Anchor = Anchor.TopLeft,
                            Origin = Anchor.TopLeft,
                            Children = new Drawable[]
                            {
                                new FillFlowContainer
                                {
                                    Direction = FillDirection.Vertical,
                                    RelativeSizeAxes = Axes.Both,
                                    Children = new Drawable[]
                                    {
                                        tabs = new LobbyTabControl<LobbyTabs>
                                        {
                                            RelativeSizeAxes = Axes.X,
                                            Height = 50
                                        },
                                        tabContent = new ScrollContainer
                                        {
                                            ScrollbarAnchor = Anchor.TopRight,
                                            ScrollbarVisible = true,
                                            RelativeSizeAxes = Axes.Both,
                                            Children = new Drawable[]
                                            {
                                                new LobbyCard
                                                {
                                                    Anchor = Anchor.TopLeft,
                                                    Origin = Anchor.TopLeft
                                                },
                                                new LobbyCard
                                                {
                                                    Anchor = Anchor.TopRight,
                                                    Origin = Anchor.TopRight
                                                },
                                                new LobbyCard
                                                {
                                                    Anchor = Anchor.CentreLeft,
                                                    Origin = Anchor.CentreLeft
                                                },
                                                new LobbyCard
                                                {
                                                    Anchor = Anchor.CentreRight,
                                                    Origin = Anchor.CentreRight
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new Container
                        {
                            Margin = new MarginPadding
                            {
                                Left = 200,
                                Right = 200
                            },
                            RelativeSizeAxes = Axes.Y,
                            Width = 200,
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Height = 20,
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    Children = new Drawable[]
                                    {
                                        new SpriteIcon
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Icon = FontAwesome.fa_users,
                                            RelativeSizeAxes = Axes.Y,
                                            Width = 20
                                        },
                                        new SpriteText
                                        {
                                            Margin = new MarginPadding { Left = 25 },
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Text = "Party",
                                            TextSize = 30
                                        },
                                        new SpriteText
                                        {
                                            Colour = Color4.WhiteSmoke,
                                            TextSize = 20,
                                            Text = "1/9",
                                            Anchor = Anchor.CentreRight,
                                            Origin = Anchor.CentreRight
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            tabs.Current.Default = LobbyTabs.All;

            tabs.Current.ValueChanged += filterLobbies;

        }

        private void filterLobbies(LobbyTabs tab)
        {

        }

        protected override bool OnClick(InputState state)
        {
            hideOverlays();
            return base.OnClick(state);
        }

        protected override void LoadComplete()
        {
            solo.MoveToX(-DrawWidth);
        }

        protected override void OnEntering(Screen last)
        {
            var presence = new DiscordRpc.RichPresence
            {
                details = "Liveshow Selection",
                state = "Idle",
                largeImageKey = "logo"
            };

            DiscordRpc.UpdatePresence(ref presence);

            Content.FadeInFromZero(400);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOut(400);
            return base.OnExiting(next);
        }
    }
}

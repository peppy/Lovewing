// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using Lovewing.Game.Graphics.UserInterface;
using Lovewing.Game.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;
using System.Linq;
using OpenTK.Graphics.OpenGL;

namespace Lovewing.Game.Screens
{
    public class MainScreen : LovewingScreen
    {
        private readonly Background background;
        private readonly Sprite idol, notifIcon;
        private readonly LovewingButton soloBtn, mpBtn, notifBtn;
        private readonly Container toolbar;
        private readonly Container<Wedge> wedgeContainer;
        private readonly LovewingColors colors = new LovewingColors();
        private readonly Wedge home, management, liveshow;
        
        private void Solo()
        {
            liveshow.Expand();
        }

        private void Matchmaking()
        {
            Push(new MatchmakingScreen());
        }

        public MainScreen()
        {
            // Wedge home, management, liveshow;
            Children = new Drawable[]
            {
                background = new Background(@"Backgrounds/mainmenu")
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                idol = new Sprite
                {
                    X = 50,
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    FillMode = FillMode.Fit,
                    Scale = new Vector2(0.75f)
                },
                toolbar = new Toolbar(),
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
                                        soloBtn = new LovewingButton(0, 0, 75)
                                        {
                                            Action = Solo,
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre,
                                            Text = "Solo",
                                            Size = new Vector2(500, 200),
                                            BackgroundColour = colors.Blue,
                                            HoverColour = colors.Blue
                                        },
                                        mpBtn = new LovewingButton(0, 0, 75)
                                        {
                                            Action = Matchmaking,
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
                                        new LovewingButton(0, 0, 60)
                                        {
                                            Size = new Vector2(630, 200),
                                            Text = "Button",
                                            BackgroundColour = colors.Yellow,
                                            HoverColour = colors.Yellow,
                                            Anchor = Anchor.BottomCentre,
                                            Origin = Anchor.BottomCentre
                                        },
                                        new LovewingButton(0, 0, 60)
                                        {
                                            Size = new Vector2(630, 200),
                                            Text = "Another Button",
                                            BackgroundColour = colors.Yellow,
                                            HoverColour = colors.Yellow,
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre
                                        }
                                    }
                                }
                            }
                        },
                        home = new HomeWedge
                        {
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
                                        new LovewingDoubleButton(-0.4f, 140, -125, 30)
                                        {
                                            CornerRadius = 5,
                                            Size = new Vector2(400, 200),
                                            Text = "Story",
                                            BackgroundColour = colors.Magenta,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft
                                        },
                                        notifBtn = new LovewingButton
                                        {
                                            CornerRadius = 100,
                                            Size = new Vector2(200, 200),
                                            Text = "Inbox",
                                            BackgroundColour = Color4.White,
                                            HoverColour = colors.Magenta,
                                            TextColour = colors.Magenta,
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            Children = new Drawable[]
                                            {
                                                notifIcon = new Sprite
                                                {
                                                    Y = -10,
                                                    Scale = new Vector2(70),
                                                    Anchor = Anchor.Centre,
                                                    Origin = Anchor.Centre,
                                                    Colour = colors.Magenta
                                                },
                                                new Circle
                                                {
                                                    Y = -40,
                                                    X = 40,
                                                    Size = new Vector2(25, 25),
                                                    Colour = Color4.Red,
                                                    Origin = Anchor.Centre,
                                                    Anchor = Anchor.Centre
                                                }
                                            }
                                        }
                                    }
                                },
                                new Container
                                {
                                    Y = 235f,
                                    Padding = new MarginPadding(15f),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Child = new LovewingDoubleButton
                                    {
                                        CornerRadius = 5,
                                        Size = new Vector2(630f, 200f),
                                        Text = "Events",
                                        BackgroundColour = colors.Magenta,
                                        Anchor = Anchor.BottomCentre,
                                        Origin = Anchor.BottomCentre
                                    }
                                }
                            }
                        }
                    }
                }
            };

            home.StateChanged += SelectWedge;
            management.StateChanged += SelectWedge;
            liveshow.StateChanged += SelectWedge;

            Add(new[]
            {
                liveshow.CreateButton("Shows"),
                management.CreateButton("Idols"),
                home.CreateButton("Home")
            });
        }

        protected override void OnEntering(Screen last)
        {
            Content.FadeOut();

            Content.FadeIn(2000);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOut(2000);
            return base.OnExiting(next);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore, FontStore fontStore)
        {
            idol.Texture = texStore.Get(@"Idols/kotori");
            
            notifIcon.Texture = fontStore.Get(((char)FontAwesome.fa_envelope_o).ToString());
        }

        private void SelectWedge(VisibilityContainer con, Visibility vis)
        {
            if (vis == Visibility.Visible)
                wedgeContainer.Children.Where(child => child != con).ToList().ForEach(wedge => wedge.Hide());
        }
    }
}

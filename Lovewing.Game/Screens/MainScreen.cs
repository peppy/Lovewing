// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

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
using OpenTK;
using OpenTK.Graphics;
using System.Linq;

namespace Lovewing.Game.Screens
{
    public class MainScreen : LovewingScreen
    {
        private readonly Background background;
        private readonly Sprite idol;
        private readonly Sprite notifIcon;
        private readonly LovewingButton notifBtn;
        private readonly Container toolbar;
        private readonly Container<Wedge> wedgeContainer;
        private readonly LovewingColors colors = new LovewingColors();

        public MainScreen()
        {
            Wedge home, management, liveshow;
            Children = new Drawable[]
            {
                background = new Background(@"Backgrounds/mainmenu")
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                idol = new Sprite
                {
                    X = 50,
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Size = new Vector2(300f, 600f),
                },
                toolbar = new Toolbar(),
                wedgeContainer = new Container<Wedge>
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Wedge[]
                    {
                        liveshow = new LiveShowWedge
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Margin = new MarginPadding { Right = 100 },
                            Children = new[]
                            {
                                new Container
                                {
                                    X = -100,
                                    Y = -220,
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children = new Drawable[]
                                    {
                                        new LovewingButton
                                        {
                                            Text = "test",
                                            Size = new Vector2(300, 200),
                                            BackgroundColour = colors.Blue,
                                            HoverColor = colors.Blue,
                                            TextColor = Color4.White,
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
                            Children = new Drawable []
                            {
                                new Container
                                {
                                    Y = 235,
                                    Padding = new MarginPadding(15),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Child = new LovewingButton
                                    {
                                        CornerRadius = 5,
                                        Size = new Vector2(630, 200),
                                        Text = "aye its a button",
                                        Anchor = Anchor.BottomCentre,
                                        Origin = Anchor.BottomCentre,
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
                                            Origin = Anchor.TopLeft,
                                        },
                                        notifBtn = new LovewingButton
                                        {
                                            CornerRadius = 100,
                                            Size = new Vector2(200, 200),
                                            Text = "Inbox",
                                            BackgroundColour = Color4.White,
                                            HoverColor = colors.Magenta,
                                            TextColor = colors.Magenta,
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
                                                    Colour = colors.Magenta,
                                                },
                                                new Circle
                                                {
                                                    Y = -40,
                                                    X = 40,
                                                    Size = new Vector2(25, 25),
                                                    Colour = Color4.Red,
                                                    Origin = Anchor.Centre,
                                                    Anchor = Anchor.Centre,
                                                }
                                            }
                                        },
                                    },
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
                                        Origin = Anchor.BottomCentre,
                                    }
                                }
                            }
                        },   
                    }
                },
            };

            home.StateChanged += SelectWedge;
            management.StateChanged += SelectWedge;
            liveshow.StateChanged += SelectWedge;

            Add(new[]
            {
                management.CreateButton("Idols"),
                home.CreateButton("Home"),
                liveshow.CreateButton("Liveshow"),
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
            if(vis == Visibility.Visible)
            {
                wedgeContainer.Children.Where(child => child != con).ToList().ForEach(wedge => wedge.Hide());
            }
        }
    }
}

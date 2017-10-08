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
        private readonly Sprite background;
        private readonly Sprite idol;
        private readonly Sprite notifIcon;
        private readonly LovewingButton notifBtn;
        private readonly Container<Wedge> wedgeContainer;
        private readonly LovewingColors colors = new LovewingColors();

        public MainScreen()
        {
            Wedge home, management;
            Children = new Drawable[]
            {
                background = new Sprite
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                idol = new Sprite
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Size = new Vector2(300f, 600f)
                },
                wedgeContainer = new Container<Wedge>
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Wedge[]
                    {
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
                                    X = -50f,
                                    Y = -220f,
                                    Padding = new MarginPadding(15f),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Children =  new Drawable[]
                                    {
                                        new LovewingDoubleButton(-0.4f, 140, -100, 30)
                                        {
                                            CornerRadius = 5,
                                            Size = new Vector2(350f, 200f),
                                            Text = "Story",
                                            BackgroundColour = colors.Magenta,
                                            Anchor = Anchor.TopLeft,
                                            Origin = Anchor.TopLeft,
                                        },
                                        notifBtn = new LovewingButton
                                        {
                                            CornerRadius = 5,
                                            Size = new Vector2(250f, 200f),
                                            Text = "Notifications",
                                            BackgroundColour = colors.Magenta,
                                            Anchor = Anchor.TopRight,
                                            Origin = Anchor.TopRight,
                                            Children = new Drawable[]
                                            {
                                                notifIcon = new Sprite
                                                {
                                                    Y = -10,
                                                    Scale = new Vector2(200),
                                                    Anchor = Anchor.Centre,
                                                    Origin = Anchor.Centre,
                                                    Colour = Color4.White,
                                                },
                                                new Circle
                                                {
                                                    Y = -45,
                                                    X = 35,
                                                    Size = new Vector2(30, 30),
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
                        management = new IdolManagementWedge
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Margin = new MarginPadding { Right = 50 }
                        }
                    }
                },
            };

            management.StateChanged += SelectWedge;
            home.StateChanged += SelectWedge;

            Add(new[]
            {
                management.CreateButton("Idols"),
                home.CreateButton("Home"),
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
            background.Texture = texStore.Get(@"Backgrounds/mainmenu");

            idol.Texture = texStore.Get(@"Idols/kotori");
            
            notifIcon.Texture = fontStore.Get(((char)FontAwesome.fa_inbox).ToString());
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

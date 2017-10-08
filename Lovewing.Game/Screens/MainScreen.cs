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
        private readonly Sprite avatar;
        private readonly LovewingButton notifBtn;
        private readonly Container toolbar;
        private readonly Container<Wedge> wedgeContainer;
        private readonly LovewingColors colors = new LovewingColors();

        public MainScreen()
        {
            Wedge home, management;
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
                toolbar = new Container
                {
                    X = -90,
                    Y = 7,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Depth = -1,
                    Children = new Drawable[]
                    {
                        new Circle
                        {
                            Size = new Vector2(80),
                            Colour = Color4.White,
                            BorderColour = new Color4(85, 85, 85, 255),
                            BorderThickness = 10,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    FillMode = FillMode.Fill,
                                    Colour = colors.Magenta,
                                },
                                avatar = new Sprite
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    FillMode = FillMode.Fit,
                                },
                            }
                        },
                    }
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

            home.StateChanged += SelectWedge;
            management.StateChanged += SelectWedge;

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
            idol.Texture = texStore.Get(@"Idols/kotori");
            
            notifIcon.Texture = fontStore.Get(((char)FontAwesome.fa_envelope_o).ToString());

            avatar.Texture = texStore.Get(@"https://owo.whats-th.is/455c65.png");
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

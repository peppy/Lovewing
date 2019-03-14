using Lovewing.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System.Collections.Generic;
using osu.Framework.Bindables;

namespace Lovewing.Graphics.Overlays
{
    public class Sidebar : OverlayContainer
    {
        private FillFlowContainer badges;
        private Container levelCon;
        private SpriteText levelText;
        private TextureStore texStore;
        private Page page = Page.Main;
        private Dictionary<Page, Container> pages = new Dictionary<Page, Container>();

        public bool IsOpen = false;

        public BindableInt Level { get; set; } = new BindableInt(1);

        public Sidebar()
        {
            Anchor = Anchor.CentreRight;
            Origin = Anchor.CentreRight;
            RelativeSizeAxes = Axes.Y;
            Width = 275;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Radius = 10,
                Colour = ColourInfo.SingleColour(Color4.Black).MultiplyAlpha(0.5f)
            };
            Depth = -2;
            State = Visibility.Hidden;
        }

        public void Toggle()
        {
            IsOpen = !IsOpen;
            ToggleVisibility();
        }

        public void ToPage(Page page)
        {
            if (page == this.page) return;

            if (page == Page.Main)
            {
                var last = pages[this.page];
                var next = pages[page];

                last.MoveToX(Width, 200, Easing.OutQuad);
                next.MoveToX(0, 200, Easing.OutQuad);
            }
            else
            {
                var last = pages[this.page];
                var next = pages[page];

                last.MoveToX(-Width, 200, Easing.InQuad);
                next.MoveToX(0, 200, Easing.InQuad);
            }

            this.page = page;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore) => this.texStore = texStore;

        protected override void LoadComplete()
        {
            Level.ValueChanged += val =>
            {
                levelCon.ResizeWidthTo(25 + 5 * val.ToString().Length, 200, Easing.OutQuad);
                levelText.Text = val.ToString();
            };

            AddRange(new Drawable[]
            {
                new Box
                {
                    Colour = LovewingColours.White,
                    RelativeSizeAxes = Axes.Both
                },
                pages[Page.Main] = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new BufferedContainer
                        {
                            Height = 250,
                            RelativeSizeAxes = Axes.X,
                            BlurSigma = Vector2.One,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Child = new Sprite
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.Both,
                                FillMode = FillMode.Fill,
                                Scale = new Vector2(1.5f),
                                Margin = new MarginPadding
                                {
                                    Right = 150,
                                    Bottom = 20
                                },
                                Texture = texStore?.Get(@"https://owo.whats-th.is/49e126.png"),
                                Colour = Color4.Gray
                            }
                        },
                        new SpriteText
                        {
                            Margin = new MarginPadding { Top = 150 },
                            Font = @"Noto Sans CJK Regular",
                            TextSize = 40,
                            AllowMultiline = false,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = @"Riko"
                        },
                        new Box
                        {
                            Margin = new MarginPadding { Top = 250 },
                            Height = 40,
                            RelativeSizeAxes = Axes.X,
                            Colour = new Color4(225, 167, 42, 255)
                        },
                        new CircularContainer
                        {
                            Height = 125,
                            Width = 125,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding { Top = 200 },
                            FillMode = FillMode.Fit,
                            Colour = Color4.White,
                            Masking = true,
                            BorderColour = new Color4(85, 85, 85, 255),
                            BorderThickness = 15,
                            Child = new Sprite
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                                FillMode = FillMode.Fit,
                                Texture = texStore?.Get(@"https://owo.whats-th.is/455c65.png")
                            }
                        },
                        badges = new FillFlowContainer
                        {
                            Padding = new MarginPadding(15),
                            Spacing = new Vector2(2),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = 50
                        },
                        levelCon = new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Width = 25 + 5 * Level.Value.ToString().Length,
                            Height = 30,
                            CornerRadius = 5,
                            Masking = true,
                            Margin = new MarginPadding { Top = 300 },
                            Children = new Drawable[]
                            {
                                new Circle
                                {
                                    Colour = LovewingColours.Blue,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both
                                },
                                levelText = new SpriteText
                                {
                                    Font = @"Muli Light",
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    TextSize = 30,
                                    Text = Level.Value.ToString()
                                }
                            }
                        },
                        new ScrollContainer
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.Both,
                            Margin = new MarginPadding { Top = 350 },
                            Child = new FillFlowContainer
                            {
                                Spacing = new Vector2(10),
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.Both,
                                Direction = FillDirection.Vertical,
                                Margin = new MarginPadding { Top = 150 },
                                Children = new Drawable[]
                                {
                                    new CategoryButton
                                    {
                                        Text = @"Friends",
                                        Icon = FontAwesome.fa_users
                                    },
                                    new CategoryButton
                                    {
                                        Text = @"Profile",
                                        Icon = FontAwesome.fa_user
                                    },
                                    new CategoryButton
                                    {
                                        Text = @"Events",
                                        Icon = FontAwesome.fa_clock_o
                                    },
                                    new CategoryButton
                                    {
                                        Text = @"Shoutout!",
                                        Icon = FontAwesome.fa_bullhorn
                                    },
                                    new CategoryButton
                                    {
                                        Text = @"Settings",
                                        Icon = FontAwesome.fa_wrench,
                                        Action = () => ToPage(Page.Settings)
                                    }
                                }
                            }
                        }
                    }
                },
                pages[Page.Settings] = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    X = Width,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = 100,
                            Children = new Drawable[]
                            {
                                new IconButton
                                {
                                    Margin = new MarginPadding { Left = 10 },
                                    Icon = FontAwesome.fa_chevron_left,
                                    Size = new Vector2(20),
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Action = () => ToPage(Page.Main),
                                    Colour = LovewingColours.Magenta
                                },
                                new SpriteText
                                {
                                    Font = @"Noto Sans CJK JP Regular",
                                    Text = @"Settings",
                                    Colour = LovewingColours.Magenta,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    TextSize = 30
                                }
                            }
                        }
                    }
                }
            });

            // idk why but this fixes it being open while not really open
            Show();
            Hide();
        }

        protected override void PopIn()
        {
            if (page != Page.Main)
            {
                page = Page.Main;

                pages[Page.Settings]?.MoveToX(Width);
                pages[Page.Main]?.MoveToX(0);
            }

            Content.MoveToX(0, 250, Easing.InQuad);
        }

        protected override void PopOut() => Content.MoveToX(Width, 250, Easing.OutQuad);

        public enum Page
        {
            Main,
            Settings
        }

        private class CategoryButton : LovewingSmallButton
        {
            public CategoryButton()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                ButtonColour = LovewingColours.LightMagenta;
                ShadowColour = LovewingColours.Magenta;
                TextColour = Color4.White;
                Size = new Vector2(250, 50);
            }
        }
    }
}

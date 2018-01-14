// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Overlay
{
    public class LovewingSidebar : OverlayContainer
    {
        private Container main;
        private Container settings;
        private string page = "main";

        public LovewingSidebar()
        {
            RelativeSizeAxes = Axes.Y;
            Width = 275;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Radius = 10,
                Colour = Color4.Black.Opacity(0.5f)
            };
        }

        protected override void PopIn()
        {
            if (page != "main")
            {
                page = "main";

                settings.MoveToX(Width);
                main.MoveToX(0);
            }

            Content.MoveToX(0, 250, Easing.InQuad);
        }

        protected override void PopOut()
        {
            Content.MoveToX(275, 250, Easing.OutQuad);
        }

        private void toSettings()
        {
            page = "settings";

            settings
                .MoveToX(0, 200, Easing.OutQuad);

            main.MoveToX(-Width, 200, Easing.OutQuad);
        }

        private void toMain()
        {
            page = "main";

            main
                .MoveToX(0, 200, Easing.InQuad);

            settings.MoveToX(Width, 200, Easing.InQuad);
        }

        [BackgroundDependencyLoader]
        private void load(LovewingColours colours, UserData user, TextureStore texStore)
        {
            FillFlowContainer badges;

            AddRange(new Drawable[]
            {
                new Box
                {
                    Colour = colours.White,
                    RelativeSizeAxes = Axes.Both
                },
                main = new Container
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
                            Children = new Drawable[]
                            {
                                new Sprite
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    FillMode = FillMode.Fill,
                                    Scale = new Vector2(1.5f),
                                    Margin = new MarginPadding
                                    {
                                        Right = 150,
                                        Top = -20
                                    },
                                    Texture = texStore.Get(user.UserBackground)
                                },
                                new Box
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black.Opacity(0.5f)
                                }
                            }
                        },
                        new SpriteText
                        {
                            Margin = new MarginPadding
                            {
                                Top = 150
                            },
                            Font = @"Noto Sans CJK Regular",
                            TextSize = 40,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = user.Username
                        },
                        new Box
                        {
                            Margin = new MarginPadding
                            {
                                Top = 250
                            },
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
                            Margin = new MarginPadding
                            {
                                Top = 200
                            },
                            FillMode = FillMode.Fit,
                            Colour = Color4.White,
                            Masking = true,
                            BorderColour = new Color4(85, 85, 85, 255),
                            BorderThickness = 15,
                            Children = new Drawable[]
                            {
                                new Sprite
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    FillMode = FillMode.Fit,
                                    Texture = texStore.Get(user.Avatar)
                                }
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
                        new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Width = 25 + 5 * user.Level.ToString().Length,
                            Height = 30,
                            CornerRadius = 5,
                            Masking = true,
                            Margin = new MarginPadding
                            {
                                Top = 300
                            },
                            Children = new Drawable[]
                            {
                                new Circle
                                {
                                    Colour = colours.Blue,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both
                                },
                                new SpriteText
                                {
                                    Font = @"Muli Light",
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    TextSize = 30,
                                    Text = user.Level.ToString()
                                }
                            }
                        },
                        new ScrollContainer
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.Both,
                            Margin = new MarginPadding
                            {
                                Top = 350
                            },
                            Children = new Drawable[]
                            {
                                new FillFlowContainer
                                {
                                    Spacing = new Vector2(10),
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Direction = FillDirection.Vertical,
                                    Margin = new MarginPadding
                                    {
                                        Top = 150
                                    },
                                    Children = new Drawable[]
                                    {
                                        new LovewingSmallButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            ButtonColour = colours.LightMagenta,
                                            ShadowColour = colours.Magenta,
                                            CornerRadius = 7,
                                            Size = new Vector2(250, 50),
                                            Text = "Friends",
                                            Icon = FontAwesome.fa_users
                                        },
                                        new LovewingSmallButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            ButtonColour = colours.LightMagenta,
                                            ShadowColour = colours.Magenta,
                                            CornerRadius = 7,
                                            Size = new Vector2(250, 50),
                                            Text = "Profile",
                                            Icon = FontAwesome.fa_user
                                        },
                                        new LovewingSmallButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            ButtonColour = colours.LightMagenta,
                                            ShadowColour = colours.Magenta,
                                            CornerRadius = 7,
                                            Size = new Vector2(250, 50),
                                            Text = "Events",
                                            Icon = FontAwesome.fa_clock_o
                                        },
                                        new LovewingSmallButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            ButtonColour = colours.LightMagenta,
                                            ShadowColour = colours.Magenta,
                                            CornerRadius = 7,
                                            Size = new Vector2(250, 50),
                                            Text = "Shoutout!",
                                            Icon = FontAwesome.fa_bullhorn
                                        },
                                        new LovewingSmallButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            ButtonColour = colours.LightMagenta,
                                            ShadowColour = colours.Magenta,
                                            CornerRadius = 7,
                                            Size = new Vector2(250, 50),
                                            Text = "Settings",
                                            Icon = FontAwesome.fa_wrench,
                                            Action = toSettings
                                        }
                                    }
                                }
                                /*new FillFlowContainer
                                {
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                    Direction = FillDirection.Vertical,
                                    Children = new Drawable[]
                                    {
                                        new LovewingHollowButton
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            BackgroundColour = colours.White,
                                            BorderColour = colours.Magenta,
                                            TextColour = colours.Magenta,
                                            IconColour = colours.Magenta,
                                            BorderThickness = 3,
                                            CornerRadius = 7,
                                            Size = new Vector2(250, 50),
                                            Text = "Home",
                                            Icon = FontAwesome.fa_home,
                                        }
                                    }
                                }*/
                            }
                        }
                    }
                },
                settings = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
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
                                    Margin = new MarginPadding
                                    {
                                        Left = 10
                                    },
                                    Icon = FontAwesome.fa_chevron_left,
                                    Size = new Vector2(20),
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Action = toMain,
                                    Colour = colours.Magenta
                                },
                                new SpriteText
                                {
                                    Font = "Noto Sans CJK JP Regular",
                                    Text = "Settings",
                                    Colour = colours.Magenta,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    TextSize = 20
                                },
                                new ScrollContainer
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Margin = new MarginPadding
                                    {
                                        Top = 100
                                    },
                                    Children = new Drawable[]
                                    {
                                        new FillFlowContainer
                                        {
                                            Spacing = new Vector2(10),
                                            Anchor = Anchor.TopCentre,
                                            Origin = Anchor.TopCentre,
                                            RelativeSizeAxes = Axes.Both,
                                            Direction = FillDirection.Vertical,
                                            Margin = new MarginPadding
                                            {
                                                Top = 50
                                            },
                                            Children = new Drawable[]
                                            {
                                                new VolumeSlider
                                                {
                                                    Anchor = Anchor.Centre,
                                                    Origin = Anchor.Centre,
                                                    Size = new Vector2(200, 50)
                                                }
                                             }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            settings.MoveToX(Width);

            user.Badges.Add(new Online.User.Badge());

            foreach (var badge in user.Badges)
            {
                badges.Add(new CircularContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    FillMode = FillMode.Fit,
                    BorderColour = badge.Colour,
                    BorderThickness = 6,
                    Masking = true,
                    Height = 40,
                    Width = 40,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Transparent
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = badge.Colour,
                            RelativeSizeAxes = Axes.Both,
                            Scale = new Vector2(0.5f),
                            Icon = badge.Icon
                        }
                    }
                });
            }
        }

        private class VolumeSlider : SliderBar<double>
        {
            private AudioManager audio;
            private readonly SpriteText text;

            protected readonly Box Box;
            protected readonly Circle SelectionBox;

            public VolumeSlider()
            {
                CurrentNumber.MinValue = 0;
                CurrentNumber.MaxValue = 100;
                CurrentNumber.Value = 50;

                Children = new Drawable[]
                {
                    text = new SpriteText
                    {
                        Text = "Volume",
                        TextSize = 20,
                        Font = "Noto Sans CJK JP Regular"
                    },
                    Box = new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 5,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre
                    },
                    SelectionBox = new Circle
                    {
                        Size = new Vector2(30),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(AudioManager audio, LovewingColours colours)
            {
                SelectionBox.Colour = colours.Magenta;
                Box.Colour = colours.LightMagenta;
                this.audio = audio;
                text.Colour = colours.Magenta;
            }

            protected override void UpdateValue(float value)
            {
                if (Box != null)
                    SelectionBox.MoveToX(value * Box.DrawWidth - 10);
                audio?.Volume.Set(value);
            }
        }
    }
}

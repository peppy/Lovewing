// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Allocation;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.Overlay
{
    public class LovewingSidebar : OverlayContainer
    {

        public LovewingSidebar()
        {
            RelativeSizeAxes = Axes.Y;
            Width = 275;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Radius = 10,
                Colour = Color4.Black.Opacity(0.5f),
            };
        }

        protected override void PopIn()
        {
            Content.MoveToX(0, 250, Easing.InQuad);
        }

        protected override void PopOut()
        {
            Content.MoveToX(275, 250, Easing.OutQuad);
        }

        [BackgroundDependencyLoader]
        private void load(LovewingColors colors, UserData user, TextureStore texStore)
        {

            FillFlowContainer badges;

            AddRange(new Drawable[]
            {
                new Box
                {
                    Colour = colors.White,
                    RelativeSizeAxes = Axes.Both,
                },
                new Container
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
                            BlurSigma = new Vector2(1),
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
                                        Top = -20,
                                    },
                                    Texture = texStore.Get(user.UserBackground),
                                },
                                new Box
                                {
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black.Opacity(0.5f),
                                }
                            },
                        },
                        new SpriteText
                        {
                            Margin = new MarginPadding
                            {
                                Top = 150,
                            },
                            TextSize = 40,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = user.Username,
                        },
                        new Box
                        {
                            Margin = new MarginPadding
                            {
                                Top = 250,
                            },
                            Height = 40,
                            RelativeSizeAxes = Axes.X,
                            Colour = new Color4(225, 167, 42, 255),
                        },
                        new CircularContainer
                        {
                            Height = 125,
                            Width = 125,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Margin = new MarginPadding
                            {
                                Top = 200,
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
                                    Texture = texStore.Get(user.Avatar),
                                },
                            }
                        },
                        badges = new FillFlowContainer
                        {
                            Padding = new MarginPadding(15),
                            Spacing = new Vector2(2),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = 50,
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
                                Top = 300,
                            },
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Colour = colors.Blue,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                },
                                new SpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    TextSize = 30,
                                    Text = user.Level.ToString(),
                                }
                            }
                        }
                    }
                }
            });

            user.Badges.Add(new Online.User.Badge());

            foreach (var badge in user.Badges)
            {
                badges.Add(new CircularContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
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
                            Colour = Color4.Transparent,
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = badge.Colour,
                            RelativeSizeAxes = Axes.Both,
                            Scale = new Vector2(0.5f),
                            Icon = badge.Icon,
                        },
                    }
                });
            }
        }
    }
}

// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class Toolbar : FillFlowContainer
    {
        public Toolbar()
        {
            Direction = FillDirection.Horizontal;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore, LovewingColors colors)
        {
            Padding = new MarginPadding { Right = 75, Top = 5 };
            Spacing = new Vector2(75, 0);

            Sprite avatar;

            Children = new Drawable[]
            {
                new CircularContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit,
                    Colour = Color4.White,
                    Masking = true,
                    BorderColour = new Color4(85, 85, 85, 255),
                    BorderThickness = 10,
                    Children = new Drawable[]
                    {
                        avatar = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            FillMode = FillMode.Fit,
                        },
                    }
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Height = 0.4f,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        new IconButton
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.BottomRight,
                            Size = new Vector2(20),
                            Colour = Color4.LightGreen,
                            Icon = FontAwesome.fa_plus,
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Text = @"1.337",
                            TextSize = 40,
                        },
                        new CircularContainer
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            FillMode = FillMode.Fit,
                            BorderColour = colors.Magenta,
                            BorderThickness = 6,
                            Masking = true,
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
                                    Colour = colors.Magenta,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(0.5f),
                                    Icon = FontAwesome.fa_heart,
                                },
                            }
                        },
                    }
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Height = 0.4f,
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        new IconButton
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.BottomRight,
                            Size = new Vector2(20),
                            Colour = Color4.LightGreen,
                            Icon = FontAwesome.fa_plus,
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Text = @"1.201.102",
                            TextSize = 40,
                        },
                        new CircularContainer
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.Both,
                            FillMode = FillMode.Fit,
                            BorderColour = colors.Yellow,
                            BorderThickness = 6,
                            Masking = true,
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
                                    Colour = colors.Yellow,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(0.5f),
                                    Icon = FontAwesome.fa_star,
                                },
                            }
                        },
                    }
                },
            };

            avatar.Texture = texStore.Get(@"https://owo.whats-th.is/455c65.png");
        }
    }
}

// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Online;
using Lovewing.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK;
using OpenTK.Graphics;
using System;

namespace Lovewing.Game.Graphics.Overlay
{
    public class LovewingToolbar : FillFlowContainer
    {
        public Action ButtonAction;

        public LovewingToolbar()
        {
            Direction = FillDirection.Horizontal;
            Origin = Anchor.TopRight;
            Anchor = Anchor.TopRight;
            RelativeSizeAxes = Axes.X;
            Height = 100;
            Depth = -1;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore, LovewingColors colors, UserData user)
        {
            Padding = new MarginPadding { Right = 75, Top = 5 };
            Spacing = new Vector2(75, 0);

            Children = new Drawable[]
            {
                new IconButton
                {
                    Icon = FontAwesome.fa_ellipsis_v,
                    Action = ButtonAction,
                    Margin = new MarginPadding { Top = 17.5f },
                    Size = new Vector2(15, 55),
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                    Colour = Color4.White,
                },
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
                            Text = user.Loveca.ToString(),
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
                            Text = user.Coins.ToString(),
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
        }
    }
}

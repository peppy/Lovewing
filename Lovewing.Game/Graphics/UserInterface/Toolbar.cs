using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    class Toolbar : Container
    {
        private readonly LovewingColors colors = new LovewingColors();
        private readonly Sprite lovecaIcon;
        private readonly Sprite starIcon;
        private readonly Sprite avatar;
        private readonly IconButton lovecaBtn;
        private readonly IconButton starBtn;

        public Toolbar()
        {
            X = -90;
            Y = 7;
            Anchor = Anchor.TopRight;
            Origin = Anchor.TopRight;
            Depth = -1;
            Children = new Drawable[]
            {
                new Circle
                {
                    X = -20,
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
                new Container
                {
                    X = -80,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        new Circle
                        {
                            Y = 40,
                            X = -70,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Size = new Vector2(30),
                            BorderColour = colors.LightMagenta,
                            BorderThickness = 5,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    FillMode = FillMode.Fill,
                                    Colour = Color4.Transparent,
                                },
                                lovecaIcon = new Sprite
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Colour = colors.LightMagenta,
                                    Size = new Vector2(12),
                                },
                            }
                        },
                        new SpriteText
                        {
                            Y = 40,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Text = @"1.337",
                            TextSize = 40,
                        },
                        lovecaBtn = new IconButton
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Y = 25,
                            X = 25,
                            IconSize = new Vector2(20),
                            Colour = Color4.LightGreen,
                        },
                    }
                },
                new Container
                {
                    X = -240,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        new Circle
                        {
                            Y = 40,
                            X = -110,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Size = new Vector2(30),
                            BorderColour = colors.Yellow,
                            BorderThickness = 5,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    FillMode = FillMode.Fill,
                                    Colour = Color4.Transparent,
                                },
                                starIcon = new Sprite
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Colour = colors.Yellow,
                                    Size = new Vector2(12),
                                },
                            }
                        },
                        new SpriteText
                        {
                            Y = 40,
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Text = @"1.201.102",
                            TextSize = 40,
                        },
                        starBtn = new IconButton
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Y = 25,
                            X = 25,
                            IconSize = new Vector2(20),
                            Colour = Color4.LightGreen,
                        },
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(FontStore fontStore, TextureStore texStore)
        {
            lovecaBtn.Texture = fontStore.Get(((char)FontAwesome.fa_plus).ToString());
            starBtn.Texture = fontStore.Get(((char)FontAwesome.fa_plus).ToString());

            lovecaIcon.Texture = fontStore.Get(((char)FontAwesome.fa_heart).ToString());
            starIcon.Texture = fontStore.Get(((char)FontAwesome.fa_star).ToString());

            avatar.Texture = texStore.Get(@"https://owo.whats-th.is/455c65.png");
        }
    }
}

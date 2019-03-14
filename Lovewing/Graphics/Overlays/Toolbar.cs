using Lovewing.Graphics.Sprites;
using Lovewing.Graphics.UserInterface;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System;
using osu.Framework.Bindables;

namespace Lovewing.Graphics.Overlays
{
    public class Toolbar : FillFlowContainer
    {
        private Sprite avatar;
        private TextureStore texStore;
        private CurrencyComponent loveca;
        private CurrencyComponent coins;

        public Action ButtonAction { get; set; }
        public Bindable<int> Loveca { get; set; } = new Bindable<int>(0);
        public Bindable<int> Coins { get; set; } = new Bindable<int>(0);
        public Bindable<int> Health { get; set; } = new Bindable<int>(100);

        public Toolbar()
        {
            Direction = FillDirection.Horizontal;
            Origin = Anchor.TopRight;
            Anchor = Anchor.TopRight;
            RelativeSizeAxes = Axes.X;
            Height = 100;
            Depth = -1;
            Padding = new MarginPadding { Right = 75, Top = 5 };
            Spacing = new Vector2(75, 0);
        }

        protected override void LoadComplete()
        {
            Children = new Drawable[]
            {
                new IconButton
                {
                    Icon = FontAwesome.fa_ellipsis_v,
                    Action = ButtonAction,
                    Margin = new MarginPadding { Top = 17.5f },
                    Size = new Vector2(10, 50),
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                    Colour = Color4.White
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
                    Child = avatar = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit,
                        Texture = texStore?.Get(@"https://owo.whats-th.is/455c65.png")
                    }
                },
                loveca = new CurrencyComponent
                {
                    CurrencyColour = LovewingColours.Magenta,
                    CurrencyIcon = FontAwesome.fa_heart,
                    Value = Loveca,
                    CanAdd = true,
                    AddAction = () => Loveca.Value++,
                    HasBar = false
                },
                coins = new CurrencyComponent
                {
                    CurrencyColour = LovewingColours.Yellow,
                    CurrencyIcon = FontAwesome.fa_star,
                    Value = Coins,
                    CanAdd = true,
                    AddAction = () => Coins.Value++,
                    HasBar = false
                },
                new CurrencyComponent
                {
                    CurrencyColour = LovewingColours.Red,
                    CurrencyIcon = FontAwesome.fa_heartbeat,
                    Value = Health,
                    AddAction = () => Health.Value++,
                    CanAdd = true,
                    HasBar = true,
                    Type = CurrencyComponent.CurrencyType.Percentage
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore) => this.texStore = texStore;

        private class CurrencyComponent : FillFlowContainer
        {
            private SpriteText valueText;

            public Color4 CurrencyColour { get; set; }
            public FontAwesome CurrencyIcon { get; set; }
            public Bindable<int> Value { get; set; } = new Bindable<int>(0);
            public bool CanAdd { get; set; }
            public bool HasBar { get; set; }
            public CurrencyType Type { get; set; } = CurrencyType.Value;
            public Action AddAction { get; set; }

            public CurrencyComponent()
            {
                RelativeSizeAxes = Axes.Y;
                AutoSizeAxes = Axes.X;
                Height = 0.4f;
                Anchor = Anchor.CentreRight;
                Origin = Anchor.CentreRight;
                Direction = FillDirection.Horizontal;
                Spacing = new Vector2(10, 0);
            }

            protected override void LoadComplete()
            {
                Value.ValueChanged += val => valueText.Text = Type == CurrencyType.Percentage ? $"{val}/100" : val.ToString();

                Children = new Drawable[]
                {
                    new IconButton
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.BottomRight,
                        Size = new Vector2(20),
                        Colour = Color4.LightGreen,
                        Icon = FontAwesome.fa_plus,
                        Action = AddAction
                    },
                    valueText = new SpriteText
                    {
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Text = Type == CurrencyType.Percentage ? $"{Value.Value}/100" : Value.Value.ToString(),
                        TextSize = 40
                    },
                    new CircularContainer
                    {
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit,
                        BorderColour = CurrencyColour,
                        BorderThickness = 6,
                        Masking = true,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = LovewingColours.Transparent
                            },
                            new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Colour = CurrencyColour,
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(0.5f),
                                Icon = CurrencyIcon
                            }
                        }
                    }
                };
            }

            public enum CurrencyType
            {
                Percentage,
                Value
            }
        }
    }
}

// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class LovewingTabControl<T> : TabControl<T>
    {
        protected override Dropdown<T> CreateDropdown() => null;

        protected override TabItem<T> CreateTabItem(T value) => new LovewingTabItem(value) { BarColour = BarColour };

        private static bool isEnumType => typeof(T).IsEnum;

        public Color4 BarColour { get; set; }

        public LovewingTabControl()
        {
            TabContainer.Spacing = new Vector2(10f, 0f);

            if (isEnumType)
                foreach (var val in (T[])Enum.GetValues(typeof(T)))
                    AddItem(val);
        }

        [BackgroundDependencyLoader]
        private void load(LovewingColours colours)
        {
            if (accentColour == default(Color4))
                AccentColour = colours.White;
        }

        private Color4 accentColour;
        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                accentColour = value;
                var dropdown = Dropdown as IHasAccentColour;
                if (dropdown != null)
                    dropdown.AccentColour = value;
                foreach (var i in TabContainer.Children.OfType<IHasAccentColour>())
                    i.AccentColour = value;
            }
        }

        public class LovewingTabItem : TabItem<T>, IHasAccentColour
        {
            protected readonly SpriteText Text;
            protected readonly Box Bar;

            public Color4 BarColour
            {
                get => Bar.Colour;
                set => Bar.FadeColour(value);
            }

            private Color4 accentColour;
            public Color4 AccentColour
            {
                get => accentColour;
                set
                {
                    accentColour = value;
                    if (!Active)
                        Text.Colour = value;
                }
            }

            private const float transition_length = 500;

            private void fadeActive()
            {
                Bar.FadeIn(transition_length, Easing.OutQuint);
                Text.FadeColour(Color4.White, transition_length, Easing.OutQuint);
            }

            private void fadeInactive()
            {
                Bar.FadeOut(transition_length, Easing.OutQuint);
                Text.FadeColour(AccentColour, transition_length, Easing.OutQuint);
            }

            protected override bool OnHover(InputState state)
            {
                if (!Active)
                    fadeActive();
                return true;
            }

            protected override void OnHoverLost(InputState state)
            {
                if (!Active)
                    fadeInactive();
            }

            [BackgroundDependencyLoader]
            private void load(LovewingColours colours)
            {
                if (accentColour == default(Color4))
                    AccentColour = colours.White;
            }

            public LovewingTabItem(T value) : base(value)
            {
                AutoSizeAxes = Axes.X;
                RelativeSizeAxes = Axes.Y;

                Children = new Drawable[]
                {
                    Text = new SpriteText
                    {
                        Margin = new MarginPadding { Top = 5, Bottom = 5 },
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                        Text = (value as Enum)?.GetDescription() ?? value.ToString(),
                        TextSize = 14
                    },
                    Bar = new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 1,
                        Alpha = 0,
                        Colour = Color4.White,
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft
                    }
                };
            }

            protected override void OnActivated() => fadeActive();

            protected override void OnDeactivated() => fadeInactive();
        }
    }
}

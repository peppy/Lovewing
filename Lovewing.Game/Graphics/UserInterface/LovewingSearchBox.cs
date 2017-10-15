// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Lovewing.Game.Graphics.UserInterface
{
    class LovewingSearchBox : FocusedTextBox
    {
        protected virtual bool AllowCommit => false;

        public LovewingSearchBox()
        {
            AddRange(new Drawable[]
            {
                new SpriteIcon
                {
                    Icon = FontAwesome.fa_search,
                    Origin = Anchor.CentreRight,
                    Anchor = Anchor.CentreRight,
                    Margin = new MarginPadding { Right = 10 },
                    Size = new Vector2(20),
                }
            });

            PlaceholderText = "Search";
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (HandlePendingText(state))
                return true;

            if (!state.Keyboard.ControlPressed && !state.Keyboard.ShiftPressed)
                switch (args.Key)
                {
                    case Key.Left:
                    case Key.Right:
                    case Key.Up:
                    case Key.Down:
                        return false;
                }

            if (!AllowCommit)
                switch (args.Key)
                {
                    case Key.KeypadEnter:
                    case Key.Enter:
                        return false;
                }

            if (state.Keyboard.ShiftPressed && args.Key == Key.Delete)
                return false;

            return base.OnKeyDown(state, args);
        }
    }
}

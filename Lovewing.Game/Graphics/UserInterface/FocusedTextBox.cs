// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Input;
using OpenTK.Input;
using System;

namespace Lovewing.Game.Graphics.UserInterface
{
    class FocusedTextBox : LovewingTextBox
    {
        private bool focus;

        public Action Exit;
        public bool HoldFocus
        {
            get { return focus; }
            set
            {
                focus = value;
                if (!focus && HasFocus)
                    GetContainingInputManager().ChangeFocus(null);
            }
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                if (Text.Length > 0)
                    Text = string.Empty;
                else
                    Exit?.Invoke();
                return true;
            }

            return base.OnKeyDown(state, args);
        }

        public override bool RequestsFocus => HoldFocus;
    }
}

using System;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace Lovewing.Graphics.UserInterface
{
    public class FocusedTextBox : LovewingTextBox
    {
        private bool focus;

        public Action Exit => null;
        public bool HoldFocus
        {
            get => focus;
            set
            {
                focus = value;
                if (!focus && HasFocus)
                    GetContainingInputManager().ChangeFocus(null);
            }
        }

        protected override bool OnKeyDown(KeyDownEvent args)
        {
            if (args.Key == Key.Escape)
            {
                if (Text.Length > 0)
                    Text = string.Empty;
                else
                    Exit?.Invoke();

                return true;
            }

            return base.OnKeyDown(args);
        }

        public override bool RequestsFocus => HoldFocus;
    }
}

// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using osu.Framework.Allocation;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace Lovewing.Tests
{
    internal class LovewingTests : LovewingGame
    {
        //before we add stuff to the actual game, we need to write scratchpad tests for it.
        [BackgroundDependencyLoader]
        private void load ()
        {
            Children = new Drawable[]
            {
                new TestBrowser(),
                new CursorContainer(),
            };
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}

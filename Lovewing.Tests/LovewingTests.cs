using System;
using osu.Framework;
using osu.Framework.Desktop;
using osu.Framework.VisualTests;
using osu.Framework.Desktop.Tests;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics;
using osu.Framework.Platform;

namespace Lovewing.Tests
{
    internal class LovewingTests : Game
    {
        //before we add stuff to the actual game, we need to write scratchpad tests for it.
        [BackgroundDependencyLoader]
        private void load ()
        {
            Children = new Drawable[]
            {
            new TestBrowser();
            new CursorContainer();
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}

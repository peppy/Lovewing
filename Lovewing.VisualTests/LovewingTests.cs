// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Allocation;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu.Framework.Testing;
using Lovewing.Game;
using Lovewing.Game.Graphics;

namespace Lovewing.Tests
{
    internal class LovewingTests : LovewingGameBase
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

        protected override void LoadComplete()
        {
            base.LoadComplete();

            LoadComponentAsync(new Background("Backgrounds/mainmenu") { Depth = 10 }, AddInternal);
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}

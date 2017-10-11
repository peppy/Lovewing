// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu.Framework.Testing;
using Lovewing.Game.Graphics;

namespace Lovewing.Game
{
    internal class LovewingTests : LovewingGameBase
    {
        //before we add stuff to the actual game, we need to write scratchpad tests for it.
        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new Drawable[]
            {
                new TestBrowser(),
                new CursorContainer(),
            };

            LoadComponentAsync(new Background("Backgrounds/mainmenu") { Depth = 10 }, AddInternal);
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}

// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Sprites;

namespace Lovewing.Game.Graphics.Cursor
{
    class LovewingCursor : CursorContainer
    {
        protected override Drawable CreateCursor() => new Cursor();

        public class Cursor : Container
        {
            private Container cursor;

            public Cursor()
            {
                AutoSizeAxes = Axes.Both;
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore texStore)
            {
                Children = new Drawable[]
                {
                    cursor = new Container
                    {
                        AutoSizeAxes = Axes.Both,
                        Child = new Sprite
                        {
                            Texture = texStore.Get(@"http://vignette4.wikia.nocookie.net/emoticon-bank/images/a/ae/Ragnarok6354_-_General_Cursor_Contest_Entry.png"),
                        }
                    },
                };
            }
        }
    }
}

using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Lovewing.Graphics.Cursor
{
    public class LovewingCursor : CursorContainer
    {
        protected override Drawable CreateCursor() => new Cursor();

        public class Cursor : Container
        {
            public Cursor()
            {
                AutoSizeAxes = Axes.Both;
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore texStore)
            {
                Child = new Sprite
                {
                    Size = new Vector2(25),
                    Texture = texStore.Get(@"Cursor/default")
                };
            }
        }
    }
}

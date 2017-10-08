using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Containers;
using OpenTK;

namespace Lovewing.Game.Graphics.UserInterface
{
    class IconButton : ClickableContainer
    {

        public Vector2 IconSize
        {
            get { return Sprite.Size; }
            set { Sprite.Size = value; }
        }

        public Texture Texture
        {
            get { return Sprite.Texture; }
            set { Sprite.Texture = value; }
        }

        protected Sprite Sprite;

        public IconButton()
        {
            AddInternal(new Drawable[]
            {
                Sprite = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });
        }
    }
}

// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Lovewing.Game.Graphics
{
    public class Background : Sprite
    {
        private readonly string textureName;

        public Background(string textureName)
        {
            this.textureName = textureName;
            FillMode = FillMode.Fill;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore store)
        {
            Texture = store.Get(textureName);
        }
    }
}

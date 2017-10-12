// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics
{
    public class Background : BufferedContainer
    {
        public Sprite Sprite;

        private readonly string textureName;

        public Background(string textureName)
        {
            CacheDrawnFrameBuffer = true;

            this.textureName = textureName;
            RelativeSizeAxes = Axes.Both;
            Depth = float.MaxValue;

            Add(Sprite = new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Color4.DarkGray,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
            });
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            if (!string.IsNullOrEmpty(textureName))
                Sprite.Texture = textures.Get(textureName);
        }
    }
}

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
        private readonly string textureName;
        private readonly Sprite bg;

        public Background(string textureName = @"")
        {
            this.textureName = textureName;

            CacheDrawnFrameBuffer = true;
            RelativeSizeAxes = Axes.Both;
            Depth = float.MaxValue;

            Add(bg = new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Color4.DarkGray,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill,
            });
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
            if (!string.IsNullOrEmpty(textureName))
                bg.Texture = texStore.Get(textureName);
        }
    }
}

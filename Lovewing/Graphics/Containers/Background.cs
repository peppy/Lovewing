using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Lovewing.Graphics.Containers
{
    internal class Background : BufferedContainer
    {
        private readonly string textureName;
        private readonly Sprite sprite;
        private TextureStore texStore;

        public Background(string textureName)
        {
            this.textureName = textureName;

            CacheDrawnFrameBuffer = true;
            RelativeSizeAxes = Axes.Both;
            Depth = float.MaxValue;
            BlurSigma = new Vector2(2.5f);

            Add(sprite = new Sprite
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Colour = Color4.LightGray,
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fill
            });
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore) => this.texStore = texStore;

        protected override void LoadComplete()
        {
            if (!string.IsNullOrEmpty(textureName))
                sprite.Texture = texStore.Get(textureName);

            base.LoadComplete();
        }
    }
}

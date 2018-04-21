using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.IO.Stores;

namespace Lovewing.Graphics.Sprites
{
    public class SpriteIcon : CompositeDrawable
    {
        private readonly Sprite iconSprite;

        private FontStore fonts;
        private FontAwesome icon;

        public FontAwesome Icon
        {
            get => icon;
            set
            {
                if (icon == value) return;
                icon = value;

                if (IsLoaded)
                    iconSprite.Texture = fonts.Get(((char)icon).ToString());
            }
        }

        public SpriteIcon()
        {
            AddInternal(iconSprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre
            });
        }

        [BackgroundDependencyLoader]
        private void load(FontStore fonts) => this.fonts = fonts;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            iconSprite.Texture = fonts.Get(((char)icon).ToString());
        }
    }
}

// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Containers;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    class IconButton : ClickableContainer
    {

        public Vector2 IconSize
        {
            get { return Sprite.Size; }
            set { Sprite.Size = value; hover.Size = value; }
        }

        public Texture Texture
        {
            get { return Sprite.Texture; }
            set { Sprite.Texture = value; }
        }

        protected Sprite Sprite;

        private readonly Box hover;

        public IconButton()
        {
            AddInternal(new Drawable[]
            {
                Sprite = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                hover = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    Colour = Color4.White,
                }
            });
        }

        protected override bool OnHover(InputState state)
        {
            hover.FadeIn(200);
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            hover.FadeOut(200);
            base.OnHoverLost(state);
        }
    }
}

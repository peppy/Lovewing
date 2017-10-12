// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using OpenTK.Graphics;

namespace Lovewing.Game.Graphics.UserInterface
{
    public class IconButton : ClickableContainer
    {
        public FontAwesome Icon
        {
            get { return SpriteIcon.Icon; }
            set { SpriteIcon.Icon = value; }
        }

        protected SpriteIcon SpriteIcon;

        private readonly Box hover;

        public IconButton()
        {
            AddRangeInternal(new Drawable[]
            {
                SpriteIcon = new SpriteIcon
                {
                    RelativeSizeAxes = Axes.Both,
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

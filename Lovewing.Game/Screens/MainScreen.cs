// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Screens.Main;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Lovewing.Game.Screens
{
    public class MainScreen : LovewingScreen
    {
        private readonly Sprite background;

        public MainScreen()
        {
            Children = new Drawable[]{
                background = new Sprite()
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new HomeWedge(),
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore texStore)
        {
            background.Texture = texStore.Get(@"Backgrounds/mainmenu");
        }
    }
}

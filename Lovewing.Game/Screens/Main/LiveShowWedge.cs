// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Graphics;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace Lovewing.Game.Screens.Main
{
    public class LiveShowWedge : Wedge
    {
        private Color4 wedgeColor;
        private Color4 buttonColor;
        private Texture icon;
        protected override Color4 WedgeColor => wedgeColor;
        protected override Color4 ButtonColor => buttonColor;
        protected override Texture ButtonIcon => icon;

        [BackgroundDependencyLoader]
        private void load(LovewingColors colors, TextureStore texStore, FontStore fontStore)
        {
            wedgeColor = colors.DarkBlue;
            buttonColor = colors.Blue;

            icon = fontStore.Get(((char)FontAwesome.fa_music).ToString());
        }
    }
}

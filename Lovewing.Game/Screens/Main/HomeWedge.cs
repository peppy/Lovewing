// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using Lovewing.Game.Graphics;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace Lovewing.Game.Screens.Main
{
    public class HomeWedge : Wedge
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
            wedgeColor = colors.Magenta;
            buttonColor = colors.LightMagenta;

            icon = fontStore.Get(((char)FontAwesome.fa_home).ToString());
        }
    }
}

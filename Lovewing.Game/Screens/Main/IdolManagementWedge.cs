// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Graphics;
using OpenTK.Graphics;
using osu.Framework.Allocation;

namespace Lovewing.Game.Screens.Main
{
    public class IdolManagementWedge : Wedge
    {
        private Color4 wedgeColor;
        protected override Color4 WedgeColor => wedgeColor;

        [BackgroundDependencyLoader]
        private void load(LovewingColors colors)
        {
            wedgeColor = colors.LightYellow;
        }
    }
}

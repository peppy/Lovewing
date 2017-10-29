// Copyright (c) 2017 Clara.
// Licensed under the EPL-1.0 License

using osu.Framework.Screens;
using osu.Framework.Graphics;

namespace Lovewing.Game.Screens
{
    /// <summary>
    /// This class should be used to add general properties and methods
    /// </summary>
    public class LovewingScreen : Screen
    {
        protected override void OnEntering(Screen last)
        {
            Content.FadeIn(400);
            base.OnEntering(last);
        }

        protected override bool OnExiting(Screen next)
        {
            Content.FadeOut(400);
            return base.OnExiting(next);
        }
    }
}

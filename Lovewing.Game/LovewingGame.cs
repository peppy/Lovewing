// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Screens;
using osu.Framework.Allocation;

namespace Lovewing.Game
{
    public class LovewingGame : LovewingGameBase
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new MainScreen());
        }
    }
}
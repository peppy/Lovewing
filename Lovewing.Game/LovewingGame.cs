// Copyright (c) 2007-2017 Clara.
// Licensed under the MIT License

using Lovewing.Game.Screens;

namespace Lovewing.Game
{
    public class LovewingGame : LovewingGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            Add(new MainScreen());
        }
    }
}
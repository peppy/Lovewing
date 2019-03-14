using Lovewing.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Lovewing.Screens.Game
{
    public class GameScreen : Screen
    {
        protected override void LoadComplete()
        {
            AddRangeInternal(new Drawable[]
            {
                new Background(@"Backgrounds/game_default")
            });
        }
    }
}

// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE
// Modified for use in Lovewing.

namespace Lovewing.Game.Player.Judgements
{
    public class HitWindows
    {
        // We're using Mania's judgement values. It's more stable compared to other solutions.

        /// <summary>
        /// please be noted each hit Window is designed per difficulty
        /// for example, if Easy diff, use [hitWindow]_max
        /// </summary>

        #region Constants 
            
        /// <summary>
        /// Perfect hit 
        ///</summary>

        private const double perfect_min = 27.8;
        private const double perfect_mid = 38.8;
        private const double perfect_max = 44.8;

        /// <summary>
        /// Great Hit
        /// </summary>

        private const double great_min = 68; 
        private const double great_mid = 98;
        private const double great_max = 128;

        /// <summary>
        /// Good Hit
        /// </summary>

        private const double good_min = 134;
        private const double good_mid = 164;
        private const double good_max = 194;

        /// <summary> 
        /// OK Hit 
        /// </summary>

        private const double ok_min = 194;
        private const double ok_mid = 224;
        private const double ok_max = 254;


        /// <summary>
        /// You know you're bad if you get this judgement
        /// </summary>

        private const double bad_min = 242;
        private const double bad_mid = 272;
        private const double bad_max = 302;

        /// <summary>
        /// A Miss judgement. Feel bad if you get this.
        /// </summary>

        private const double miss_min = 316;
        private const double miss_mid = 346;
        private const double miss_max = 376;

        #endregion

        /// <summary>
        /// The actual hit windows
        /// </summary>

        public double Perfect = perfect_min;
        public double Great = great_mid;
        public double Good = good_mid;
        public double Ok = ok_mid;
        public double Bad = bad_mid;
        public double Miss = miss_mid;

        /// <summary>
        /// constructs the hit windows 
        /// </summary>
        public HitWindows () 
        {
        }

        /*
        /// <summary>
        /// Constructs hit windows by fitting a parameter to a 2-part piecewise linear function for each hit window.
        /// </summary>
        /// <param name="difficulty">The parameter.</param>
        public HitWindows(double difficulty)
        {
            Perfect = BeatmapDifficulty.DifficultyRange(difficulty, perfect_max, perfect_mid, perfect_min);
            Great = BeatmapDifficulty.DifficultyRange(difficulty, great_max, great_mid, great_min);
            Good = BeatmapDifficulty.DifficultyRange(difficulty, good_max, good_mid, good_min);
            Ok = BeatmapDifficulty.DifficultyRange(difficulty, ok_max, ok_mid, ok_min);
            Bad = BeatmapDifficulty.DifficultyRange(difficulty, bad_max, bad_mid, bad_min);
            Miss = BeatmapDifficulty.DifficultyRange(difficulty, miss_max, miss_mid, miss_min);
        }*/

        /// <summary>
        /// Constructs new hit windows which have been multiplied by a value.
        /// </summary>
        /// <param name="windows">The original hit windows.</param>
        /// <param name="value">The value to multiply each hit window by.</param>
        public static HitWindows operator *(HitWindows windows, double value)
        {
            return new HitWindows
            {
                Perfect = windows.Perfect * value,
                Great = windows.Great * value,
                Good = windows.Good * value,
                Ok = windows.Ok * value,
                Bad = windows.Bad * value,
                Miss = windows.Miss * value
            };
        }

        /// <summary>
        /// Constructs new hit windows which have been divided by a value.
        /// </summary>
        /// <param name="windows">The original hit windows.</param>
        /// <param name="value">The value to divide each hit window by.</param>
        public static HitWindows operator /(HitWindows windows, double value)
        {
            return new HitWindows
            {
                Perfect = windows.Perfect / value,
                Great = windows.Great / value,
                Good = windows.Good / value,
                Ok = windows.Ok / value,
                Bad = windows.Bad / value,
                Miss = windows.Miss / value
            };
        }
    }
}

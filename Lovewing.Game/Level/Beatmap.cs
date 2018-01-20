using System.Collections.Generic;

namespace Lovewing.Game.Level
{

    public class BeatmapRank
    {
        public uint Rank { get; set; } = 1;
        public uint RankMax { get; set; }
    }

    /// <summary>
    /// A loaded beatmap.
    /// </summary>
    public class Beatmap
    {
        public string SourceFile { get; set; } = "";
        public string SongName { get; set; } = "";
        public string Background { get; set; } = "";
        public double BPM { get; set; } = 192.0;
        public double NoteSpeed { get; set; } = 1.0;
        public uint Difficulty { get; set; }
        public List<BeatmapRank> Ranks { get; set; } = new List<BeatmapRank>();
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}

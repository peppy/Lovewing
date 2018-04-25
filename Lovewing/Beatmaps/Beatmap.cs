using System.Collections.Generic;

namespace Lovewing.Beatmaps
{
    public class BeatmapRank
    {
        public uint Rank { get; set; } = 1;
        public uint RankMax { get; set; }
    }

    public class Beatmap
    {
        public string Title { get; set; } = @"Unknown";
        public string Background { get; set; } = @"Backgrounds/game_default";
        public string Cover { get; set; } = @"Covers/muse";
        public string MusicFile { get; set; } = @"song.mp3";
        public string Author { get; set; } = @"Unknown";
        public double BPM { get; set; } = 192.0;
        public double NoteSpeed { get; set; } = 1.0;
        public uint Difficulty { get; set; } = 1;
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<string> Artists { get; set; } = new List<string>();
        public List<BeatmapRank> Ranks { get; set; } = new List<BeatmapRank>();
    }
}

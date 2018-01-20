using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Lovewing.Game.Level;
using Microsoft.CSharp.RuntimeBinder;

namespace Lovewing.Game.Loaders
{
    public class SIFTLoader : IBeatmapLoader
    {
        private Regex MusicFileRegex = new Regex("(.)_(%d+)", RegexOptions.IgnoreCase);

        public string GetFileExtension() { return ".rs"; }

        public bool CanLoadFile(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == GetFileExtension();
        }

        public async Task<Beatmap> Load(string path)
        {
            var beatmap = new Beatmap();
            var rsfile = await AsyncFileUtils.ReadTextFile(path);
            dynamic json = JsonConvert.DeserializeObject(rsfile);

            beatmap.SongName = json.song_name;
            beatmap.Difficulty = json.difficulty;

            try
            {
                // Try parsing out ranks.
                foreach (var rank in json.rank_info)
                {
                    var beatmapRank = new BeatmapRank
                    {
                        Rank = rank.rank,
                        RankMax = rank.rank_max
                    };
                }
            }
            catch (RuntimeBinderException e)
            {
                // No rank info.
            }

            try
            {
                beatmap.SourceFile = json.music_file;
            }
            catch (RuntimeBinderException e)
            {
                // No music_file defined
                var baseName = Path.GetFileNameWithoutExtension(path);
                var matches = MusicFileRegex.Matches(baseName);

                if(matches.Count == 2)
                {
                    beatmap.SourceFile = $"{matches[0].Value}_{matches[1].Value}.wav";
                }
            }

            try
            {
                beatmap.Background = Math.Min(json.song_info[0].star, 12).ToString();
            }
            catch (RuntimeBinderException e)
            {
                // No background
            }

            beatmap.NoteSpeed = json.song_info[0].notes_speed;

            // Read out each note
            foreach (var note in json.song_info[0].notes)
            {
                // Mandatory fields
                var beatmapNote = new Note
                {
                    Time = note.timing_sec,
                    Effect = 1,
                    EffectValue = note.effect_value,
                    Position = note.position - 1
                };

                // Optional fields
                try
                {
                    beatmapNote.Attribute = note.notes_attribute;
                }
                catch (RuntimeBinderException e)
                {
                    beatmapNote.Attribute = 10;
                }

                try
                {
                    beatmapNote.Level = note.notes_level;
                }
                catch (RuntimeBinderException e)
                {
                    beatmapNote.Level = 1;
                }

                // Effect
                if ((beatmapNote.Effect & 2u) > 0)
                {
                    // Token note
                    beatmapNote.Effect = 2u;
                }
                else if ((beatmapNote.Effect & 4u) > 0)
                {
                    // Long note
                    beatmapNote.Effect = 3u;
                }
                else if ((beatmapNote.Effect & 8u) > 0)
                {
                    // Star note
                    beatmapNote.Effect = 4u;
                }

                if ((beatmapNote.Effect & 32) > 0)
                {
                    // Swing note
                    beatmapNote.Effect += 10;
                }

                if (beatmapNote.Effect > 13)
                {
                    beatmapNote.Effect = 11;
                }

                beatmap.Notes.Add(beatmapNote);
            }

            // Sort ascending based on time.
            beatmap.Notes.Sort((note1, note2) => note1.Time.CompareTo(note2.Time));

            return beatmap;
        }
    }
}

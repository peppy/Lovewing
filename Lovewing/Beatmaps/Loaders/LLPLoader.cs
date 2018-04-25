using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace Lovewing.Beatmaps.Loaders
{
    public class LLPLoader : IBeatmapLoader
    {
        public string GetFileExtension() { return ".llp"; }

        public bool CanLoadFile(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == GetFileExtension();
        }

        public async Task<Beatmap> Load(string path)
        {
            var beatmap = new Beatmap();
            var llpFile = await AsyncFileUtils.ReadTextFile(path);
            dynamic json = JsonConvert.DeserializeObject(llpFile);

            beatmap.MusicFile = $"{json.audiofile}.wav";
            beatmap.BPM = json.speed;

            foreach (var lane in json.lane)
            {
                foreach (var note in lane)
                {
                    var beatmapNote = new Note
                    {
                        Effect = 1u,
                        EffectValue = 2.0,
                        Time = note.starttime / 1000.0,
                        Attribute = 1u,
                        Level = 1u,
                        Position = 8u - (uint)note.lane
                    };

                    if ((bool)note.longnote)
                    {
                        beatmapNote.Effect = 3;
                        beatmapNote.EffectValue = (note.endtime - note.starttime) / 1000.0;
                    }

                    beatmap.Notes.Add(beatmapNote);
                }
            }

            // Sort ascending based on time.
            beatmap.Notes.Sort((note1, note2) => note1.Time.CompareTo(note2.Time));

            return beatmap;
        }
    }
}

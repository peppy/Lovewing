using System.Threading.Tasks;
using Lovewing.Beatmaps;

namespace Lovewing.Beatmaps.Loaders
{
    interface IBeatmapLoader
    {
        string GetFileExtension();
        bool CanLoadFile(string path);
        Task<Beatmap> Load(string path);
    }
}

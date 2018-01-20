using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lovewing.Game.Level;

namespace Lovewing.Game.Loaders
{
    interface IBeatmapLoader
    {
        string GetFileExtension();
        bool CanLoadFile(string path);
        Task<Beatmap> Load(string path);
    }
}

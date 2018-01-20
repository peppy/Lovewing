using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lovewing.Game.Level
{
    public class Note
    {
        public double Time { get; set; } = 0.0;
        public uint Attribute { get; set; } = 0u;
        public uint Level { get; set; } = 0u;
        public uint Effect { get; set; } = 0u;
        public double EffectValue { get; set; } = 0.0;
        public uint Position { get; set; } = 0u;
    }
}

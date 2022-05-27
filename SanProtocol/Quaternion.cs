using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanBot.Packets
{
    public class Quaternion
    {
        public byte UnknownA { get; set; } = 0;
        public bool UnknownB { get; set; } = false;
        public bool ModifierFlag { get; set; } = false;

        public List<float> Values { get; set; } = new List<float>();

        public override string ToString()
        {
            return $"Quat({UnknownA},{UnknownB},{ModifierFlag})<{String.Join(",", Values)}>";
        }
    }
}

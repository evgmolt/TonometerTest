using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTestApp
{
    public class Patient
    {
        public bool Sex { get; set; }
        public int Age { get; set; }
        public string Comment { get; set; }
        public int Sys { get; set; }
        public int Dia { get; set; }
        public int Pulse { get; set; }
        public bool Arrythmia { get; set; }
        public string[] ToArray()
        {
            List<string> result = new()
            {
                Sex ? "M" : "F",
                Age.ToString(),
                Comment,
                Sys.ToString(),
                Dia.ToString(),
                Pulse.ToString(),
                Arrythmia ? "A" : "N"
            };
            return result.ToArray();
        }
    }

}

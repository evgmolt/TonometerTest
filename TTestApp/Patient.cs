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
            List<string> result = new List<string>();
            result.Add(Sex ? "M" : "F");
            result.Add(Age.ToString());
            result.Add(Comment);
            result.Add(Sys.ToString());
            result.Add(Dia.ToString());
            result.Add(Pulse.ToString());
            result.Add(Arrythmia ? "A" : "N");
            return result.ToArray();
        }
    }

}

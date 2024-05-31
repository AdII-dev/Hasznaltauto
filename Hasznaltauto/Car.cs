using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasznaltauto
{
    public class Car
    {
        public string Marka { get; set; }
        public string Tipus { get; set; }
        public string Uzemanyag { get; set; }
        public string Kivitel { get; set; }
        public string Hajtas { get; set; }
        public int Evjarat { get; set; }
        public int Ar { get; set; }
        public string ImgPath { get; set; }

        public Car(string line)
        {
            var fields = line.Split(';');
            Marka = fields[0];
            Tipus = fields[1];
            Uzemanyag = fields[2];
            Kivitel = fields[3];
            Hajtas = fields[4];
            Evjarat = int.Parse(fields[5]);
            Ar = int.Parse(fields[6]);
            ImgPath = fields[7];
        }
    }
}

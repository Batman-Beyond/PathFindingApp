using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingApp
{
    public class GridElement
    {
        public bool isObsticle { get; set; }
        public bool isPath { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        //public int gCost { get; set; }
        //public int hCost { get; set; }
       // public int fCost { get { return gCost + hCost; } }
        public typeOfFields TypeOfFields { get; set; }
    }

    public enum typeOfFields
    {
        Obsticle,
        Start,
        End
    }
}

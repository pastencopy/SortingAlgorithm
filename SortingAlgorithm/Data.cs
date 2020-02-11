using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithm
{
    class Data : IComparable<Data>
    {
        public static Random rnd = new Random();

        public int Value;

        public enum STATE
        {
            NONE,
            SELECTED,
        }

        public STATE dataState;

        public Data(int max)
        {
            this.Value = rnd.Next(1,max);

            //Drawing
            this.dataState = STATE.NONE;
        }

        public int CompareTo(Data other)
        {
            return this.Value.CompareTo(other.Value);
        }
    }
}

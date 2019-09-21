using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDli_P1
{
    public class elementDB
    {
        public int[] node;
        public double pipeOD;
        public double pipeTHK;
        public double Xdir;
        public double Ydir;
        public double Zdir;
        public double trans_ratio;
        public double velocity;
        public bool loop_dir;

        public elementDB()
        {
            this.node = new int[2] { 0, 0 };
            this.pipeOD = 0;
            this.pipeTHK = 0;
            this.Xdir = 0;
            this.Ydir = 0;
            this.Zdir = 0;
            this.trans_ratio = 1;
            this.velocity = 0;
            this.loop_dir = true;
        }
    }
}

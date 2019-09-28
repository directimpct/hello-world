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
        public bool bend;
        public int bend_node;

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
            this.bend = false;
            this.bend_node = 0;
        }
        public static elementDB operator + (elementDB a, elementDB b)
        {
            elementDB c = new elementDB();
            if (a.loop_dir)
            {
                c.node[0] = a.node[0];
                c.node[1] = b.node[1];
                c.Xdir = a.Xdir + b.Xdir;
                c.Ydir = a.Ydir + b.Ydir;
                c.Zdir = a.Zdir + b.Zdir;
            } else
            {
                c.node[0] = b.node[0];
                c.node[1] = a.node[1];
                c.Xdir = a.Xdir + b.Xdir;
                c.Ydir = a.Ydir + b.Ydir;
                c.Zdir = a.Zdir + b.Zdir;
            }
            return c;
        }
    }
}

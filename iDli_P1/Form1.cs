using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace iDli_P1
{
    public partial class Form1 : Form
    {

        public int[,] teenodes = new int[10,2];
        public int[] endnodes = new int[10];
        public int[,] branches = new int[200,2];
        public int[] tempbranch = new int[2];
        public int[] Ptempbranch = new int[2];
        elementDB[,] NewelementDB = new elementDB[8, 200];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String pAth_xml = "D:\\sample.XML";
            XmlDocument cXML = new XmlDocument();
            cXML.Load(pAth_xml);

            XmlNamespaceManager namespacemanger = new XmlNamespaceManager(cXML.NameTable);
            namespacemanger.AddNamespace("a", "COADE");
            namespacemanger.AddNamespace("b", "");
            string temp = "";
            int from_node = 0;
            int to_node = 0;
            int i = 0;
            int flag = 0;
            int counter = 0;

            XmlNodeList nodeList = cXML.DocumentElement.SelectNodes("/a:CAESARII/b:PIPINGMODEL/PIPINGELEMENT", namespacemanger);

            int[,] nodecount = new int[nodeList.Count+1,2];

            foreach (XmlNode node in nodeList)
            {
                temp = node.Attributes.GetNamedItem("FROM_NODE").Value;
                from_node = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
                for (i = 0; i < counter; i++)
                {
                    if (nodecount[i, 0] == from_node)
                    {
                        nodecount[i, 1]++;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    nodecount[counter, 0] = from_node;
                    nodecount[counter, 1]++;
                    counter++;
                }
                flag = 0;

                temp = node.Attributes.GetNamedItem("TO_NODE").Value;
                to_node = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
                for (i = 0; i < counter; i++)
                {
                    if (nodecount[i, 0] == to_node)
                    {
                        nodecount[i, 1]++;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    nodecount[counter, 0] = to_node;
                    nodecount[counter, 1]++;
                    counter++;
                }
                flag = 0;


               //Console.WriteLine(from_node + " " + to_node);
            }

            int j = 0;
            int k = 0;
            for (i = 0; i < nodecount.GetLength(0); i++)
            { if (nodecount[i, 1] == 1)
                {
                    listBox1.Items.Add(nodecount[i, 0]);
                    endnodes[k] = nodecount[i, 0];
                    k++;
                }
                else if (nodecount[i, 1] == 3)
                {
                    listBox2.Items.Add(nodecount[i, 0]);
                    teenodes[j,0] = nodecount[i, 0];
                    j++;
                }
            }

            for (j = 0; j < listBox1.Items.Count; j++)
                for (k = 0; k < 200; k++)
                    NewelementDB[j, k] = new elementDB();

            //string temp = "";
            string tdia = "";
            string tthk = "";
            i = 0;
            foreach (XmlNode node in nodeList)
            {
                temp = node.Attributes.GetNamedItem("FROM_NODE").Value;
                NewelementDB[0, i].node[0] = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
                temp = node.Attributes.GetNamedItem("TO_NODE").Value;
                NewelementDB[0, i].node[1] = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
                if (node.SelectSingleNode("BEND") != null)
                {
                    temp = node.SelectSingleNode("BEND").Attributes.GetNamedItem("NODE1").Value;
                    NewelementDB[0, i].bend_node = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
                    NewelementDB[0, i].bend = true;
                } 
                temp = node.Attributes.GetNamedItem("DIAMETER").Value;
                if (temp == "-1.010100")
                    temp = tdia;
                else
                    tdia = temp;
                NewelementDB[0, i].pipeOD = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
                temp = node.Attributes.GetNamedItem("WALL_THICK").Value;
                if (temp == "-1.010100")
                    temp = tthk;
                else
                    tthk = temp;
                NewelementDB[0, i].pipeTHK = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
                temp = node.Attributes.GetNamedItem("DELTA_X").Value;
                if (temp == "-1.010100")
                    temp = "0.000000";
                NewelementDB[0, i].Xdir = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
                temp = node.Attributes.GetNamedItem("DELTA_Y").Value;
                if (temp == "-1.010100")
                    temp = "0.000000";
                NewelementDB[0, i].Ydir = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
                temp = node.Attributes.GetNamedItem("DELTA_Z").Value;
                if (temp == "-1.010100")
                    temp = "0.000000";
                NewelementDB[0, i].Zdir = Convert.ToDouble(temp.Substring(0, temp.Length - 4));

                //Console.WriteLine(NewelementDB[0, i].node[0] + " " + NewelementDB[0, i].node[1] + " " + NewelementDB[0, i].pipeOD + " " + NewelementDB[0, i].pipeTHK + " " + NewelementDB[0, i].Xdir + " " + NewelementDB[0, i].Ydir + " " + NewelementDB[0, i].Zdir + " Bend node is " + NewelementDB[0,i].bend_node);
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int start_node = 0;
            start_node = Convert.ToInt32(listBox1.GetItemText(listBox1.SelectedItem));
           

            

            //for (int j = 0; j < listBox1.Items.Count; j++)
            //    for (int k = 0; k < 200; k++)
            //        NewelementDB[j, k] = new elementDB();

            //String pAth_xml = "D:\\sample.XML";
            //XmlDocument cXML = new XmlDocument();
            //cXML.Load(pAth_xml);

            //XmlNamespaceManager namespacemanger = new XmlNamespaceManager(cXML.NameTable);
            //namespacemanger.AddNamespace("a", "COADE");
            //namespacemanger.AddNamespace("b", "");
            //string temp = "";
            //string tdia = "";
            //string tthk = "";
            int i = 0;
            //int flag = 0;
            

            //XmlNodeList nodeList = cXML.DocumentElement.SelectNodes("/a:CAESARII/b:PIPINGMODEL/PIPINGELEMENT", namespacemanger);

            //foreach (XmlNode node in nodeList)
            //{
            //    temp = node.Attributes.GetNamedItem("FROM_NODE").Value;
            //    NewelementDB[0, i].node[0] = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
            //    temp = node.Attributes.GetNamedItem("TO_NODE").Value;
            //    NewelementDB[0, i].node[1] = Convert.ToInt32(temp.Substring(0, temp.Length - 7));
            //    temp = node.Attributes.GetNamedItem("DIAMETER").Value;
            //    if (temp == "-1.010100")
            //        temp = tdia;
            //    else
            //        tdia = temp;
            //    NewelementDB[0, i].pipeOD = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
            //    temp = node.Attributes.GetNamedItem("WALL_THICK").Value;
            //    if (temp == "-1.010100")
            //        temp = tthk;
            //    else
            //        tthk = temp;
            //    NewelementDB[0, i].pipeTHK = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
            //    temp = node.Attributes.GetNamedItem("DELTA_X").Value;
            //    if (temp == "-1.010100")
            //        temp = "0.000000";
            //    NewelementDB[0, i].Xdir = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
            //    temp = node.Attributes.GetNamedItem("DELTA_Y").Value;
            //    if (temp == "-1.010100")
            //        temp = "0.000000";
            //    NewelementDB[0, i].Ydir = Convert.ToDouble(temp.Substring(0, temp.Length - 4));
            //    temp = node.Attributes.GetNamedItem("DELTA_Z").Value;
            //    if (temp == "-1.010100")
            //        temp = "0.000000";
            //    NewelementDB[0, i].Zdir = Convert.ToDouble(temp.Substring(0, temp.Length - 4));

            //    //Console.WriteLine(NewelementDB[0, i].node[0] + " " + NewelementDB[0, i].node[1] + " " + NewelementDB[0, i].pipeOD + " " + NewelementDB[0, i].pipeTHK + " " + NewelementDB[0, i].Xdir + " " + NewelementDB[0, i].Ydir + " " + NewelementDB[0, i].Zdir);
            //    i++;
            //}

            int[,] thestack = new int[10, 3];
            for (int j = 0; j < 10; j++)
                for (int k = 0; k < 3; k++)
                    thestack[j, k] = 0;
            int counter = 1;
            int temp_node = 0;
            int old_temp = start_node;
            int index = 0;
            int bcounter = 0;
            int tbcounter = 0;
            bool toggle_var = true;
            bool flag = true;
            i = 0;
            temp_node = start_node;
            //MessageBox.Show(Convert.ToString(listBox1.Items.Count));
            while (counter < listBox1.Items.Count) // figure while thingie
            {
                    index = Searchelement(temp_node, NewelementDB,0);
                if(index == 121088) //I know you are going to forget this part ... again
                {
                    counter--; // what this fellow does is that whenever it meets a dead end with no options left to go on either sides, it will wipe the entire loop clean and add the previous branch node to the block list and re run the loop
                    for (int k = counter; k < bcounter; k++)
                    {
                        branches[k, 0] = 0;
                        branches[k, 1] = 0;
                    }
                    branches[counter - 1, 0] = Ptempbranch[0];
                    branches[counter - 1, 1] = Ptempbranch[1];
                    counter++;
                    bcounter = counter;

                    for (int k = 0; k < i; k++)
                    {
                        NewelementDB[counter, k] = new elementDB();
                    }
                    i = 0;
                    temp_node = start_node;
                    index = Searchelement(temp_node, NewelementDB, 0);

                }
                if (i==0 || flag)
                {
                    NewelementDB[counter, i] = NewelementDB[0, Math.Abs(index)];
                    flag = false;
                }
                else if(NewelementDB[0, Math.Abs(index)].bend == true || Searchteenode(temp_node, ref teenodes))
                {
                    i--;
                    NewelementDB[counter, i] = NewelementDB[counter, i] + NewelementDB[0, Math.Abs(index)];
                    flag = true;
                }
                else
                {
                    i--;
                    NewelementDB[counter, i] = NewelementDB[counter, i] + NewelementDB[0, Math.Abs(index)]; 
                }
                //Console.WriteLine(Convert.ToString(index));
                //Console.WriteLine(NewelementDB[counter, i].node[0]);
                // Console.WriteLine(Convert.ToString(i));
                if (i == 0 && index < 0)
                    toggle_var = false;
                old_temp = temp_node;
                    if(index<0 || (index ==0 && i!=0))
                    {
                    NewelementDB[counter, i].loop_dir = !toggle_var;
                        temp_node = NewelementDB[0, Math.Abs(index)].node[0];
                    

                    branches[bcounter, 1] = old_temp;
                    branches[bcounter, 0] = temp_node;
                     //if (NewelementDB[0, Math.Abs(index)].bend == true || Searchteenode(temp_node, ref teenodes) || Searchteenode(old_temp, ref teenodes))
                    
                        i++;

                    if (Searchteenode(temp_node, ref teenodes) || Searchteenode(old_temp, ref teenodes))
                    {
                        Ptempbranch[1] = tempbranch[1];
                        Ptempbranch[0] = tempbranch[0];
                        tempbranch[1] = old_temp;
                        tempbranch[0] = temp_node;
                        tbcounter++;
                    }
                    bcounter++;
                }
                    else
                    {
                    NewelementDB[counter, i].loop_dir = toggle_var;
                    temp_node = NewelementDB[0, Math.Abs(index)].node[1];
                        i++;
                    branches[bcounter, 0] = old_temp;
                    branches[bcounter, 1] = temp_node;
                    if (Searchteenode(temp_node, ref teenodes) || Searchteenode(old_temp, ref teenodes))
                    {
                        Ptempbranch[1] = tempbranch[1];
                        Ptempbranch[0] = tempbranch[0];
                        tempbranch[0] = old_temp;
                        tempbranch[1] = temp_node;
                        tbcounter++;
                    }
                    bcounter++;
                }

                //Console.WriteLine("branch is written here" + branches[bcounter-1, 0] + " and " + branches[bcounter-1, 1]);
                //Console.WriteLine(temp_node);


                if (Searchnode(temp_node, endnodes))
                {
                    
                    for (int k = counter; k < bcounter; k++)
                    {
                        branches[k, 0] = 0;
                        branches[k, 1] = 0;
                    }
                    //for (int k = 0; k < tbcounter; k++)
                    //{
                        branches[counter-1, 0] = tempbranch[0];// this one only take the last branch and adds it to the block loop
                        branches[counter-1, 1] = tempbranch[1];
                    //}
                    counter++;
                    bcounter = counter;
                    i = 0;
                    temp_node = start_node;
                    
                    //Console.WriteLine("reset has happened heressssssssssssssss");


                }
            }
            int u = 0;
            for (int v = 1; v <= 5; v++)


            {
                for (u = 0; NewelementDB[v, u].node[0] != 0; u++)
                    Console.WriteLine("Loop No." + v+ " is from node " + NewelementDB[v, u].node[0] + " to node " + NewelementDB[v, u].node[1]);

                    //Console.WriteLine("Loop No." + v+ " is from node " + NewelementDB[v, u].node[0] + " to node " + NewelementDB[v, u].node[1] + "  direct " + NewelementDB[v, u].loop_dir);
            }

        }
        public bool Searchnode(int Snode, int[] Sarray)
        {
            for (int j = 0; j < 10; j++)
            {
                if (Snode == Sarray[j])
                    return true;
            }
                return false;
        }
        public bool Searchteenode(int Snode, ref int[,] Sarray)
        {
            for (int j = 0; j < 10; j++)
            {
                if (Snode == Sarray[j, 0])
                {
                    //if (Sarray[j, 1] == 1)
                    //{
                    //    Sarray[j, 1] ++;
                        return true;
                    //}
                    //else
                    //{
                    //    Sarray[j, 1]++;
                    //    return false;
                    //}
                }
            }
            return false;
        }

        public int Searchelement(int Snode, elementDB[,] Selement, int loopno)
        {
            int flag = 0;
                for (int i = 0; i < Selement.GetLength(1); i++)
                {
                //Console.WriteLine("am i looping even");
                if (Snode == Selement[loopno, i].node[0])
                    {
                    //Console.WriteLine(Convert.ToString(Snode)+ "  aaaaaaaaaa   " +i);

                    for (int j = 0; j < branches.GetLength(0); j++)
                    {
                        if (branches[j, 0] != 0)
                        {
                           // Console.WriteLine(Snode + " the branch A " + Selement[loopno, i].node[1]);
                           // Console.WriteLine(branches[j, 0] + " the branch val A " + branches[j, 1]);
                            if ((Snode == branches[j, 0] && branches[j, 1] == Selement[loopno, i].node[1]) || (Snode == branches[j, 1] && branches[j, 0] == Selement[loopno, i].node[0]))
                            {
                                //i++;
                                flag = 1;
                                //Console.WriteLine("i was hereaaaaaaaaaaaa");
                            }

                        }
                    }
                        if (flag == 0)
                        {
                            flag = 0;
                            return i;
                        }
                    
                    }
                    if (Snode == Selement[loopno, i].node[1])
                    {
                    //Console.WriteLine(Convert.ToString(Snode) + "  bbbbbbbbbbb  " + i);

                    for (int j = 0; j < branches.GetLength(0); j++)
                    {
                        if (branches[j, 0] != 0)
                        {
                            //Console.WriteLine(branches[j, 1] + " the branch B " + branches[j, 0]);
                            if ((Snode == branches[j, 1] && branches[j, 0] == Selement[loopno, i].node[0]) || (Snode == branches[j, 0] && branches[j, 1] == Selement[loopno, i].node[1]))
                            {
                                //i++;
                                flag = 1;
                                //Console.WriteLine("i was herebbbbbbbbbbbb");
                            }

                        }
                    }
                        if (flag == 0)
                        {
                            flag = 0;
                            return -1 * i;
                        }
                    
                }
                flag = 0;
                }
            //}
            //Console.WriteLine("i shouldnt be heresdfgvgggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
            return 121088;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }

}

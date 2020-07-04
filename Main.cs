using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMoving
{
    public partial class Main : Form
    {
        Logic l = new Logic();
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<char, int[]> dict = new Dictionary<char, int[]>();
                Dictionary<char, int[]> new_dict = new Dictionary<char, int[]>();
                Dictionary<char, int[]> sorted_new_dict = new Dictionary<char, int[]>();
                dict = new Dictionary<char, int[]>(l.parse_input(textBox1.Text.ToString()));
                new_dict = new Dictionary<char, int[]>(l.parse_input(textBox2.Text.ToString()));
                Dictionary<char, int[]> init = new Dictionary<char, int[]>();
                foreach (KeyValuePair<char, int[]> kvp in dict)
                {
                    init.Add(kvp.Key, new int[] { kvp.Value[0], kvp.Value[1] });
                }
                char[] moved = new char[100];
                int[][] stepsmoved = new int[100][];
                sorted_new_dict = l.sort_Dictionary(new_dict);
                int steps = l.ComputeSteps(moved, stepsmoved, dict, sorted_new_dict);
                for (int i = 0; i < steps; i++)
                {
                    Console.WriteLine(moved[i] + "-> (" + stepsmoved[i][0] + " , " + stepsmoved[i][1] + " )");
                }
                Form1 f = new Form1(new_dict.Count, moved, stepsmoved, init,steps);
                f.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Enter a proper initial state and/or final state.");
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}

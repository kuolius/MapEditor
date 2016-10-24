using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class newMap : Form
    {
        State state;
        int[][][][][] map;
        bool loaded;

        

        public newMap()
        {
            InitializeComponent();
            state = State.newMap;
            loaded = false;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("There is empty space left", "Error", MessageBoxButtons.OK);
            else
            {
                map = new int[Convert.ToInt32(textBox1.Text)][][][][];
                for (int i = 0; i < map.Length; i++)
                    map[i] = new int[Convert.ToInt32(textBox2.Text)][][][];
                for (int i = 0; i < map.Length; i++)
                    for(int j=0;j<map[i].Length;j++)
                        map[i][j] = new int[10][][];
                for (int i = 0; i < map.Length; i++)
                    for (int j = 0; j < map[i].Length; j++)
                        for(int k=0;k<map[i][j].Length;k++)
                            map[i][j][k] = new int[10][];
                for (int i = 0; i < map.Length; i++)
                    for (int j = 0; j < map[i].Length; j++)
                        for (int k = 0; k < map[i][j].Length; k++)
                            for (int l = 0; l < map[i][j][k].Length;l++)
                                map[i][j][k][l] = new int[3] {0,0,0};
                loaded = true;
                state = State.editScreen;
               
                
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.state = State.menuScreen;
            base.OnFormClosing(e);
        }
       

       
        public void update(out State state, EditScreen editScreen)
        {
            state = this.state;
            if (loaded)
            {
                editScreen.map = map;
                editScreen.InitializeMap();
                
                Dispose();
            }
        }
    }
}

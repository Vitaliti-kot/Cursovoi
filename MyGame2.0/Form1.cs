using MyGame2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            button1.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player player = new Player(textBox1.Text);
            Form playArea = new PlayArea(player);
            this.Visible = false;
            playArea.Show();
            playArea.FormClosed += PlayArea_FormClosed;
        }

        private void PlayArea_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}

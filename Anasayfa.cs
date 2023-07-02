using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinik
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hasta hasta= new Hasta();
            hasta.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Randevu randevu = new Randevu();
            randevu.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Tedavi tedavi = new Tedavi();
            tedavi.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Receteler receteler=new Receteler();
            receteler.ShowDialog();
            this.Close();
        }
    }
}

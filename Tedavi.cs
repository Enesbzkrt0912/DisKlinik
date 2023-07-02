using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace DisKlinik
{
    public partial class Tedavi : Form
    {
        public Tedavi()
        {
            InitializeComponent();
        }

        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from TedaviTbl";
            DataSet ds = Hs.ShowHasta(query);
            TedaviDGV.DataSource = ds.Tables[0];
        }

        void filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from TedaviTbl where TAd like '%" + ARATB.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            TedaviDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            TedaviAdiTb.Text = "";
            TutarTb.Text = "";
            AciklamaTb.Text = "";          
        }

        private void Tedavi_Load(object sender, EventArgs e)
        {
            uyeler();
            Reset();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "insert into  TedaviTbl values ('" + TedaviAdiTb.Text + "','" + TutarTb.Text + "','" + AciklamaTb.Text + "')";

            Hastalar Hs = new Hastalar();

            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("eklendi");
                uyeler();
                Reset();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        int key = 0;
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("güncellenecek hastayı seçin");
            }
            else
            {
                try
                {
                    string query = "update TedaviTbl set TAd='" + TedaviAdiTb.Text + "',TUcret='" + TutarTb.Text + "',TAciklama='" + AciklamaTb.Text + "' where TId=" + key + ";";
                    Hs.HastaSil(query);
                    MessageBox.Show("hasta Güncellendi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("hasta seçin");
            }
            else
            {
                try
                {
                    string query = "delete from TedaviTbl where TId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("hasta silindi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void TedaviDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TedaviAdiTb.Text = TedaviDGV.SelectedRows[0].Cells[1].Value.ToString();
            TutarTb.Text= TedaviDGV.SelectedRows[0].Cells[2].Value.ToString();
            AciklamaTb.Text= TedaviDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (TedaviAdiTb.Text=="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TedaviDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void TedaviDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ARATB_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Hasta hasta =new Hasta();
            hasta.ShowDialog(); 
            this.Close();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Randevu randevu =new Randevu();
            randevu.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Receteler receteler =new Receteler();
            receteler.ShowDialog();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
    

namespace DisKlinik
{
    public partial class Randevu : Form
    {
        public Randevu()
        {
            InitializeComponent();
        }

        ConnectionString MyCon=new ConnectionString();

        private void fillHasta()
        {
            SqlConnection baglanti=MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut=new SqlCommand("select HAd from HastaTbl",baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd",typeof(string));   
            dt.Load(rdr);
            RadCb.ValueMember = "HAd";
            RadCb.DataSource= dt;   
            baglanti.Close();
        }

        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select TAd from TedaviTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TAd", typeof(string));
            dt.Load(rdr);
            RtedaviCb.ValueMember = "TAd";
            RtedaviCb.DataSource = dt;
            baglanti.Close();
        }

        private void Randevu_Load(object sender, EventArgs e)
        {
            fillHasta();
            fillTedavi();
            uyeler();
            Reset();
        }

        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from RandevuTbl";
            DataSet ds = Hs.ShowHasta(query);
            RandevuDgv.DataSource = ds.Tables[0];
        }

        void filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from RandevuTbl where Hasta like '%" + araTb.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            RandevuDgv.DataSource = ds.Tables[0];
        }


        void Reset()
        {
            RadCb.SelectedIndex = -1;
            RtedaviCb.SelectedIndex = -1;
            Rtarih.Text = "";
            SaatCb.Text = "";
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "insert into  RandevuTbl values ('" + RadCb.SelectedValue.ToString() + "','" + RtedaviCb.SelectedValue.ToString() + "','" + Rtarih.Text + "','"+SaatCb.Text+"')";

            Hastalar Hs = new Hastalar();

            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Randevu eklendi");
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
                MessageBox.Show("güncellenecek randevuyu  seçin");
            }
            else
            {
                try
                {
                    string query = "update RandevuTbl set Hasta='" + RadCb.SelectedValue.ToString() + "',Tedavi='" + RtedaviCb.SelectedValue.ToString() + "',RTarih='" + Rtarih.Text + "',RSaat='" + SaatCb.Text + "' where RId=" + key + ";";
                    Hs.HastaSil(query);
                    MessageBox.Show("randevu  Güncellendi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void RandevuDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RadCb.SelectedValue = RandevuDgv.SelectedRows[0].Cells[1].Value.ToString();
            RtedaviCb.SelectedValue = RandevuDgv.SelectedRows[0].Cells[2].Value.ToString();
            Rtarih.Text = RandevuDgv.SelectedRows[0].Cells[3].Value.ToString();
            SaatCb.Text = RandevuDgv.SelectedRows[0].Cells[4].Value.ToString();

            if (RadCb.SelectedIndex ==-1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RandevuDgv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek randevuyu  seçin");
            }
            else
            {
                try
                {
                    string query = "delete from Randevuere RId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("randevu  silindi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void araTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Hasta hasta =new Hasta();
            hasta.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            Tedavi tedavi = new Tedavi();
            tedavi.ShowDialog();
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

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
    public partial class Hasta : Form
    {
        public Hasta()
        {
            InitializeComponent();
        }

            void uyeler()
            {
                Hastalar Hs = new Hastalar();
                string query = "select * from HastaTbl";
                DataSet ds = Hs.ShowHasta(query);
                HastaDGV.DataSource = ds.Tables[0];
            }

        void filter() // arama kısmı 
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTbl where HAd like '%"+AraTb.Text+"%'";
            DataSet ds = Hs.ShowHasta(query);
            HastaDGV.DataSource = ds.Tables[0];
        }


        //TEXBOX TEMİZLEME 
        void Reset()
        {
            HAdSoyadTb.Text = "";
            HTelefonTb.Text = "";
            AdresTb.Text = "";
            HDogumTarih.Text = "";
            HCinsiyetCb.SelectedItem = "";
            AlerjiTb.Text = "";
        }

        private void Hasta_Load(object sender, EventArgs e)
        {
            uyeler(); 
            Reset();    
        }


        //HASTA EKLEME
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "insert into  HastaTbl values ('" + HAdSoyadTb.Text + "','" + HTelefonTb.Text + "','" + AdresTb.Text + "','" + HDogumTarih.Text + "','" + HCinsiyetCb.SelectedItem.ToString() + "','" + AlerjiTb.Text + "')";

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
        //ÇIKIŞ
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //DATA GRİD SEÇİLEN HASTAYI TEXBOXLARA YAZDIRMA 
        int key = 0;
        private void HastaDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text  = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();   
            HTelefonTb.Text  = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            AdresTb.Text     = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
            HDogumTarih.Text = HastaDGV.SelectedRows[0].Cells[4].Value.ToString();
            HCinsiyetCb.SelectedItem = HastaDGV.SelectedRows[0].Cells[5].Value.ToString();
            AlerjiTb.Text = HastaDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (HAdSoyadTb.Text=="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());  
            }
        }


        //HASTA SİLME 
        private void guna2GradientButton3_Click(object sender, EventArgs e)//hasta silme
        {
            Hastalar Hs = new Hastalar();
            if (key==0)
            {
                MessageBox.Show("hasta seçin");
            }
            else
            {
                try
                {
                    string query = "delete from HastaTbl where HId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("hasta silindi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex )
                {
                   MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)//hasta güncelle
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
                    string query = "update HastaTbl set HAd='" + HAdSoyadTb.Text + "',Htelefon='" + HTelefonTb.Text + "',HAdres='" + AdresTb.Text + "',HDTarih='" + HDogumTarih.Text + "',HCinsiyet='" + HCinsiyetCb.SelectedItem.ToString() + "',HAlerji='" + AlerjiTb.Text + "' where HId=" + key + ";";
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

        private void AraTb_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Randevu randevu = new Randevu();
            randevu.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            Tedavi tedavi=new Tedavi();
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

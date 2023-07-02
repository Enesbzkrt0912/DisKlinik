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
using static Guna.UI2.Native.WinApi;

namespace DisKlinik
{
    public partial class Receteler : Form
    {
        public Receteler()
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
            rdr=komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HAd",typeof(string));
            dt.Load(rdr);
            HastaASCb.ValueMember = "HAd";
            HastaASCb.DataSource = dt;
            baglanti.Close();
        }

        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from RandevuTbl where Hasta='"+HastaASCb.SelectedValue.ToString()+"'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda=new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TedaviTb.Text = dr["Tedavi"].ToString();    
            }
            baglanti.Close();
        }

        private void fillPrice()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TedaviTbl where TAd='" + TedaviTb.Text+ "'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TutarTb.Text = dr["TUcret"].ToString();
            }
            baglanti.Close();
        }



        private void Receteler_Load(object sender, EventArgs e)
        {
            fillHasta();
            uyeler();
            Reset();
        }

        private void HastaASCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillTedavi();
        }


        void uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTbl";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }


        void filter()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTbl where HasAd like '%" + AraTB.Text + "%'";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }


        //TEXBOX TEMİZLEME 
        void Reset()
        {
            HastaASCb.SelectedItem = "";
            TutarTb.Text = "";
            IlaclarTb.Text = "";
            MiktarTb.Text = "";
            TedaviTb.Text = "";
        }



        private void TutarTb_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TedaviTb_TextChanged(object sender, EventArgs e)
        {
            fillPrice() ;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string query = "insert into  ReceteTbl values ('" + HastaASCb.SelectedValue.ToString() + "','" + TedaviTb.Text + "','" + TutarTb.Text + ",'" + IlaclarTb.Text + "'," + MiktarTb.Text + ")";

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
        private void ReceteDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HastaASCb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            TedaviTb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            TutarTb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            IlaclarTb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            MiktarTb.Text = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();

            if (TedaviTb.Text=="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ReceteDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Hastalar Hs= new Hastalar();
            if (key==0)
            {
                MessageBox.Show("silinecek reçeteyi seçin");
            }
            else 
            {
                try
                {
                    string query = "delete from ReceteTbl where RecId=" + key + "";
                    Hs.HastaSil(query);
                    MessageBox.Show("reçete silindi");
                    uyeler();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void AraTB_TextChanged(object sender, EventArgs e)
        {
            filter();
        }



        Bitmap bitmap;

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            int height = ReceteDGV.Height;
            ReceteDGV.Height = ReceteDGV.RowCount * ReceteDGV.RowTemplate.Height * 2;
            bitmap=new Bitmap(ReceteDGV.Width, ReceteDGV.Height);
            ReceteDGV.DrawToBitmap(bitmap,new Rectangle(0,10,ReceteDGV.Width,ReceteDGV.Height));
            ReceteDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0); 
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Hasta hasta = new Hasta();
            hasta.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Randevu randevu = new Randevu();    
            randevu.ShowDialog();
            this.Close();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            Tedavi tedavi = new Tedavi();
            tedavi.ShowDialog();    
            this.Close();
        }
    }
}

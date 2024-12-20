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

namespace Kitaplik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select*From TblKitaplik", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        string durum = "";
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblKitaplik(KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut.Parameters.AddWithValue("@p3", CmbKategori.Text);
            komut.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            komut.Parameters.AddWithValue("@p5", durum);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kitap Sisteme Kaydedildi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            CmbKategori.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="True")
            {
                radioButton2.Checked= true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TblKitaplik where Kitapid=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kitap Listeden Silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TblKitaplik set KitapAd=@p1,Yazar=@p2,Sayfa=@p3,Tur=@p4,Durum=@p5 where Kitapid=@p6",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut.Parameters.AddWithValue("@p3", TxtSayfa.Text);
            komut.Parameters.AddWithValue("@p4", CmbKategori.Text);
            if(radioButton1.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked == true) ;
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            komut.Parameters.AddWithValue("@p6", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select*From TblKitaplik where KitapAd=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKitapBul.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select*From TblKitaplik where KitapAd like '%"+TxtKitapBul.Text+"%'", bgl.baglanti()); //like, sql ya da access de bizim istediğimiz bir arama alanı içerisinde sadece ilgili alanların olup olmadığına bakar.
            //%a% içerisinde a geçip geçemeyenleri bulur.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}

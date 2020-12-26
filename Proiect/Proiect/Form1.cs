using Proiect.Components;
using Proiect.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class Form1 : Form
    {
        private string ConnectionString = "Data Source=DESKTOP-PVMUVAC;Initial Catalog=Cabinet;Integrated Security=True";
        private CryptoConversionHandler conversion = new CryptoConversionHandler();
        private Popup popup = Popup.PopupControl;


        public Form1()
        {
            InitializeComponent();
        }

        private async void btnConectare_Click(object sender, EventArgs e)
        {
            this.popup.RemovePopup();
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[ConectareDoctor]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Text", SqlDbType.NVarChar).Value = textBoxUser.Text;
                    using(SHA512 sha = new SHA512Managed())
                    {
                        var hash = conversion.ByteArrayToHexString(sha.ComputeHash(conversion.StringToByteArray(textBoxParola.Text)));
                        cmd.Parameters.Add("@Parola", SqlDbType.NVarChar).Value = hash;

                    }

                    try
                    {
                        sql.Open();
                        var result = await cmd.ExecuteReaderAsync();
                        DataTable dataTable = new DataTable();
                        dataTable.Load(result);
                        sql.Close();

                        if(dataTable.Rows.Count == 0)
                        {
                            this.popup.DisplayPopup("danger", "Utilizator sau parola gresita!", 5000);
                        }
                        else
                        {
                            this.tabControl1.TabPages.Remove(this.tabPageConectare);
                        }

                    }
                    catch
                    {

                    }
                   
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'cabinetDataSet1.ViewPacienti4' table. You can move, or remove it, as needed.
                this.viewPacienti4TableAdapter.Fill(this.cabinetDataSet1.ViewPacienti4);
            }
            catch
            {

            }
      



            this.popup.BindControl(this.flowLayoutPanel1, this.pictureBox2, this.label3);
        }

        private void btnAnulare_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnAdaugarePacient_Click(object sender, EventArgs e)
        {
            Pacient p = new Pacient();
            AdaugarePacient ap = new AdaugarePacient(p,0);

            if(ap.ShowDialog() == DialogResult.OK)
            {
                var p2 = ap.GetObject;

                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using(SqlCommand cmd = new SqlCommand("[dbo].[AdaugarePacient]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nume",p.Nume);
                        cmd.Parameters.AddWithValue("@Prenume", p.Prenume);
                        cmd.Parameters.AddWithValue("@DataNasterii", p.DataNasterii);
                        cmd.Parameters.AddWithValue("@Sex", p.Sex);
                        cmd.Parameters.AddWithValue("@StatutSocial", p.StatutSocial);
                        cmd.Parameters.AddWithValue("@Adresa", p.Adresa);
                        cmd.Parameters.AddWithValue("@NrTelefon", p.NrTelefon);
                        cmd.Parameters.AddWithValue("@Email", p.Email);
                        cmd.Parameters.AddWithValue("@Cetatenie", p.Cetatenie);
                        cmd.Parameters.AddWithValue("@GrupSangvin", p.GrupSangvin);
                        cmd.Parameters.AddWithValue("@RH", p.RH);
                        cmd.Parameters.AddWithValue("@Masa", p.Masa);
                        cmd.Parameters.AddWithValue("@CNP", p.CNP);
                        cmd.Parameters.AddWithValue("@Inaltime", p.Inaltime);
                        cmd.Parameters.AddWithValue("@Alergii", p.Alergii);
                        cmd.Parameters.AddWithValue("@Ocupatie", p.Ocupatie);
                        cmd.Parameters.AddWithValue("@AntecedenteHeredoColaterale", p.AntecedenteHeredoColaterale);
                        cmd.Parameters.AddWithValue("@AntecedentePersonale", p.AntecedentePersonale);
                        cmd.Parameters.AddWithValue("@ConditiiMunca", p.ConditiiMunca);


                        try
                        {
                            sql.Open();
                            await cmd.ExecuteNonQueryAsync();
                            sql.Close();
                            this.popup.DisplayPopup("success", "Pacientul a fost adaugat cu succes", 5000);

                        }
                        catch (Exception exp)
                        {
                            this.popup.DisplayPopup("danger", exp.Message, 5000);
                        }

                    }
                    
                }




                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT [Nume],[Prenume],[DataNasterii],[Sex],[NrTelefon],[StatutSocial],[Adresa],[GrupSangvin],[RH],[Masa],[Inaltime] FROM [Cabinet].[dbo].[ViewPacienti]",sql))
                    {
                        try
                        {
                            sql.Open();
                            DataTable dt = new DataTable();
                            var result = await cmd.ExecuteReaderAsync();
                            dt.Load(result);
                            sql.Close();

                            dataGridView1.DataSource = dt;
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }
                }


            }

        }

        private async void radioButtonSortareAscendent_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;
            if(radio.Checked == true)
            {
                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SortarePacienti]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SortareDupaNume", 1);
                        cmd.Parameters.AddWithValue("@SortareAscendenta", 1);

                        try
                        {
                            sql.Open();
                            DataTable dt = new DataTable();
                            var result = await cmd.ExecuteReaderAsync();
                            dt.Load(result);
                            sql.Close();

                            //dataGridView1.DataSource = dt;
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }
        }

        private async void radioButtonSortareDescendent_CheckedChanged(object sender, EventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio.Checked == true)
            {
                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SortarePacienti]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SortareDupaNume", 1);
                        cmd.Parameters.AddWithValue("@SortareAscendenta", 0);

                        try
                        {
                            sql.Open();
                            DataTable dt = new DataTable();
                            var result = await cmd.ExecuteReaderAsync();
                            dt.Load(result);
                            sql.Close();

                            dataGridView1.DataSource = dt;
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }
        }

        private async void radioButtonIstoricCrescator_CheckedChanged(object sender, EventArgs e)
        {

            var radio = sender as RadioButton;
            if (radio.Checked == true)
            {
                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SortarePacienti]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SortareDupaNume", 0);
                        cmd.Parameters.AddWithValue("@SortareAscendenta", 1);

                        try
                        {
                            sql.Open();
                            DataTable dt = new DataTable();
                            var result = await cmd.ExecuteReaderAsync();
                            dt.Load(result);
                            sql.Close();

                            dataGridView1.DataSource = dt;
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }

        }

        private async void radioButtonIstoricDescrescator_CheckedChanged(object sender, EventArgs e)
        {

            var radio = sender as RadioButton;
            if (radio.Checked == true)
            {
                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SortarePacienti]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SortareDupaNume", 0);
                        cmd.Parameters.AddWithValue("@SortareAscendenta", 0);

                        try
                        {
                            sql.Open();
                            DataTable dt = new DataTable();
                            var result = await cmd.ExecuteReaderAsync();
                            dt.Load(result);
                            sql.Close();

                            dataGridView1.DataSource = dt;
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CautarePacient]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nume", (sender as TextBox).Text);

                    try
                    {
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private async void btnStergerePacient_Click(object sender, EventArgs e)
        {
            var cnp = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();

            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[StergerePacient]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CNP", cnp);

                    try
                    {
                        sql.Open();
                        await cmd.ExecuteNonQueryAsync();
                        sql.Close();

                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }
    }
}

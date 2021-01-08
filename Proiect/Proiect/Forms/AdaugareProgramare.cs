using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect.Forms
{
    public partial class AdaugareProgramare : Form
    {
        private DataGridView dgv;
        public AdaugareProgramare(DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;

        }
        private string ConnectionString = @"Data Source=DESKTOP-EAASVM8\SQLEXPRESS;Initial Catalog=Cabinet;Integrated Security=True";

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            String text = (sender as TextBox).Text;


            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Nume, Prenume FROM [Cabinet].[dbo].[Pacient] WHERE Nume LIKE '" + text+"%'", sql))
                {
                    try
                    {
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        listBox1.Items.Clear();

                        foreach(DataRow nume in dt.Rows)
                        {
                            listBox1.Items.Add(nume["Nume"]);
                        }


                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }


        }

        private async void AdaugareProgramare_Load(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Nume, Prenume FROM [Cabinet].[dbo].[Pacient]", sql))
                {
                    try
                    {
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        listBox1.Items.Clear();

                        foreach (DataRow nume in dt.Rows)
                        {
                            listBox1.Items.Add(nume["Nume"] + " " + nume["Prenume"]);
                        }
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;
                    }

                }

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(this.listBox1.SelectedIndex != -1)
            {
                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[AdaugareProgramare]", sql))
                    {
                        DateTime myDate = DateTime.ParseExact($"{this.dateTimePicker1.Value.ToString().Split(' ')[0]} {textBox2.Text}:{textBox3.Text}", "dd-MMM-yy HH:mm",
                                           System.Globalization.CultureInfo.InvariantCulture);


                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nume", listBox1.SelectedItem.ToString().Split(' ')[0]);
                        cmd.Parameters.AddWithValue("@Prenume", listBox1.SelectedItem.ToString().Split(' ')[1]);
                        cmd.Parameters.AddWithValue("@Data", myDate);
                        cmd.Parameters.AddWithValue("@Mentiuni", textBox4.Text);


                        try
                        {
                            sql.Open();
                            await cmd.ExecuteNonQueryAsync();
                            sql.Close();
                           
                            this.DialogResult = DialogResult.OK;
                        }
                        catch (Exception exp)
                        {
                        }

                    }

                }
            }
            else
            {
                MessageBox.Show("Alegeti un pacient","Atentie",MessageBoxButtons.OK);
            }
            
        }
    }
}

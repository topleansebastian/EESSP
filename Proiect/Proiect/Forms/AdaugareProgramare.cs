using Proiect.Components;
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
        private Popup p = Popup.PopupControl;

        public AdaugareProgramare(DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;
            p.BindControl(this.flowLayoutPanel1, this.pictureBox2, this.label6);

        }
        private string ConnectionString = @"Data Source=DESKTOP-EAASVM8\SQLEXPRESS;Initial Catalog=Cabinet;Integrated Security=True";
        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception exp)
            {
                p.DisplayPopup("danger", exp.Message, 3000);
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

            try
            {
                if (int.TryParse(textBox2.Text, out int a1) == false)
                    throw new Exception("Numarul orelor trebuie sa fie un numar");
                if (int.TryParse(textBox3.Text, out int a2) == false)
                    throw new Exception("Numarul minutelor trebuie sa fie un numar");
                if (int.Parse(textBox2.Text)>23 && int.Parse(textBox2.Text)<0)
                    throw new Exception("Intervalul de ore este 00:00 - 23:59");
                if (int.Parse(textBox3.Text) > 59 && int.Parse(textBox3.Text) < 0)
                    throw new Exception("Intervalul de ore este 00:00 - 23:59");



                if (this.listBox1.SelectedIndex != -1)
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
                    MessageBox.Show("Alegeti un pacient", "Atentie", MessageBoxButtons.OK);
                }



            }
            catch (Exception exp)
            {
                p.DisplayPopup("danger", exp.Message, 3000);
            }

            



            
        }
    }
}

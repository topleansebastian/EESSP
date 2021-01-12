using CrystalDecisions.CrystalReports.Engine;
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
        private string ConnectionString = @"Data Source=DESKTOP-EAASVM8\SQLEXPRESS;Initial Catalog=Cabinet;Integrated Security=True";
        private CryptoConversionHandler conversion = new CryptoConversionHandler();
        private Popup popup = Popup.PopupControl;

        List<TabPage> tabs = new List<TabPage>();

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
                            this.tabControl1.TabPages.RemoveByKey("tabPageConectare");
                            foreach(TabPage t in tabs)
                            {
                                if(t.Name != "tabPageConectare" && t.Name != "tabPageEditarePacient" && t.Name != "tabPageConsultatii" && t.Name != "tabPageConsultatii")
                                {
                                    this.tabControl1.TabPages.Add(t);
                                }
                            }
                        }

                    }
                    catch(Exception exp)
                    {

                    }
                   
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cabinetDataSet6.ViewConsultatii' table. You can move, or remove it, as needed.
            this.viewConsultatiiTableAdapter.Fill(this.cabinetDataSet6.ViewConsultatii);
            // TODO: This line of code loads data into the 'cabinetDataSet6.ViewProgramari' table. You can move, or remove it, as needed.
            this.viewProgramariTableAdapter.Fill(this.cabinetDataSet6.ViewProgramari);
            // TODO: This line of code loads data into the 'cabinetDataSet6.ViewPacienti' table. You can move, or remove it, as needed.
            this.viewPacientiTableAdapter.Fill(this.cabinetDataSet6.ViewPacienti);



            foreach (TabPage t in tabControl1.TabPages)
            {
                tabs.Add(t);

                if(t.Name != "tabPageConectare")
                {
                    tabControl1.TabPages.Remove(t);
                }

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

            if(MessageBox.Show("Stergere pacient","Stergerea pacientului este definitiva. Continuati?",MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void tabPageConectare_Click(object sender, EventArgs e)
        {

        }

        private async void radioButtonAfisareAstazi_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SelectareConsultatii]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDoctor", 1);
                    cmd.Parameters.AddWithValue("@PerioadaStart", DateTime.Today);
                    cmd.Parameters.AddWithValue("@PerioadaStop", DateTime.Today.AddDays(1));


                    try
                    {
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        dataGridViewProgramari.DataSource = dt;
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private async void radioButtonAfisare7zile_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SelectareConsultatii]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDoctor", 1);
                    cmd.Parameters.AddWithValue("@PerioadaStart", DateTime.Today);
                    cmd.Parameters.AddWithValue("@PerioadaStop", DateTime.Today.AddDays(8));
                    try
                    {
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        dataGridViewProgramari.DataSource = dt;
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private async void radioButtonAfisare30zile_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SelectareConsultatii]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDoctor", 1);
                    cmd.Parameters.AddWithValue("@PerioadaStart", DateTime.Today);
                    cmd.Parameters.AddWithValue("@PerioadaStop", DateTime.Today.AddDays(31));
                    try
                    {
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        dataGridViewProgramari.DataSource = dt;
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private async void radioButtonAfisareCustom_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonAfisareCustom.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;

                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SelectareConsultatii]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdDoctor", 1);
                        cmd.Parameters.AddWithValue("@PerioadaStart", this.dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@PerioadaStop", this.dateTimePicker2.Value);
                        try
                        {
                            sql.Open();
                            DataTable dt = new DataTable();
                            var result = await cmd.ExecuteReaderAsync();
                            dt.Load(result);
                            sql.Close();

                            dataGridViewProgramari.DataSource = dt;
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
            
        }

        private void btnAdaugareProgramare_Click(object sender, EventArgs e)
        {
            AdaugareProgramare ap = new AdaugareProgramare(dataGridViewProgramari);
            if(ap.ShowDialog() == DialogResult.OK)
            {
                this.popup.DisplayPopup("success", "Programarea a fost adaugata cu success", 5000);


            }
        }

        private void btnEditarePacient_Click(object sender, EventArgs e)
        {

        }

        private async void buttonStergereProgramare_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Stergere programare", "Stergerea programarii este definitiva. Continuati?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var cnp = this.dataGridViewProgramari.CurrentRow.Cells[4].Value.ToString();
                var data = this.dataGridViewProgramari.CurrentRow.Cells[1].Value.ToString();

                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[StergereProgramare]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CNP", cnp);
                        cmd.Parameters.AddWithValue("@Data", data);

                        try
                        {
                            sql.Open();
                            await cmd.ExecuteNonQueryAsync();
                            sql.Close();

                            dataGridViewProgramari.Rows.Remove(dataGridViewProgramari.CurrentRow);
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }

        }

        private async void textBox13_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[CautareConsultatie]", sql))
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

                        dataGridView2.DataSource = dt;
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private async void buttonAdaugareConsultatie_Click(object sender, EventArgs e)
        {
           
                foreach (TabPage p in tabs)
                {
                    if (p.Name == "tabPageConsultatii")
                    {
                        using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT Nume,Prenume,CNP FROM [Cabinet].[dbo].[Pacient]", sql))
                            {
                                try
                                {
                                    sql.Open();
                                    DataTable dt = new DataTable();
                                    var result = await cmd.ExecuteReaderAsync();
                                    dt.Load(result);
                                    sql.Close();

                                    for(int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        listBox1.Items.Add($"{dt.Rows[i][0]} {dt.Rows[i][1]}-{dt.Rows[i][2]}");
                                    }
                                }
                                catch (Exception exp)
                                {
                                    var x = exp.Message;

                                }

                            }

                        }

                        tabControl1.TabPages.Add(p);
                        tabControl1.SelectedTab = p;

                    }
                }
            
            
        }

        private async void buttonStergereConsultatie_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Stergere consultatie", "Stergerea consultatiei este definitiva. Continuati?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var cnp = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
                var data = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();

                using (SqlConnection sql = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[StergereConsultatie]", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CNP", cnp);
                        cmd.Parameters.AddWithValue("@Data", data);

                        try
                        {
                            sql.Open();
                            await cmd.ExecuteNonQueryAsync();
                            sql.Close();

                            dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
                        }
                        catch (Exception exp)
                        {
                            var x = exp.Message;

                        }

                    }

                }
            }
        }

        private async void btnFinalizareConsultatie_Click(object sender, EventArgs e)
        {
            

            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[AdaugareConsultatie]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CNP", label30.Text);
                    cmd.Parameters.AddWithValue("@Data", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@Simptome", textBox2.Text);
                    cmd.Parameters.AddWithValue("@ConcediuMedical", checkBox1.Checked);
                    cmd.Parameters.AddWithValue("@Diagnostic", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Prescriptii", textBox4.Text);

                    try
                    {
                        sql.Open();
                        await cmd.ExecuteNonQueryAsync();
                        sql.Close();

                        tabControl1.TabPages.Remove(tabs.FirstOrDefault((p) => p.Name == "tabPageConsultatii"));
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private async void textBox14_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT Nume,Prenume,CNP FROM [Cabinet].[dbo].[Pacient] where Nume LIKE '{(sender as TextBox).Text}%'", sql))
                {
                    try
                    {
                        listBox1.Items.Clear();
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            listBox1.Items.Add($"{dt.Rows[i][0]} {dt.Rows[i][1]}-{dt.Rows[i][2]}");
                        }
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
        }

        private async void textBox14_TextChanged_1(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT Nume,Prenume,CNP FROM [Cabinet].[dbo].[Pacient] where Nume LIKE '{(sender as TextBox).Text}%'", sql))
                {
                    try
                    {
                        listBox1.Items.Clear();
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            listBox1.Items.Add($"{dt.Rows[i][0]} {dt.Rows[i][1]}-{dt.Rows[i][2]}");
                        }
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                panel6.Visible = false;
                label28.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                label29.Text = listBox1.SelectedItem.ToString().Split(' ')[1].Split('-')[0];
                label30.Text = listBox1.SelectedItem.ToString().Split(' ')[1].Split('-')[1];
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM ViewConsultatii", sql))
                {
                    try
                    {
          
                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();
                        dataGridView2.DataSource = dt;

                        
                    }
                    catch (Exception exp)
                    {
                        var x = exp.Message;

                    }

                }

            }
        }
        
        private async void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM ViewProgramari", sql))
                {
                    try
                    {

                        sql.Open();
                        DataTable dt = new DataTable();
                        var result = await cmd.ExecuteReaderAsync();
                        dt.Load(result);
                        sql.Close();
                        dataGridViewProgramari.DataSource = dt;


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

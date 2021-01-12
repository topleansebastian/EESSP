using Proiect.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect.Forms
{
    public partial class AdaugarePacient : Form
    {
        private Popup popup = Popup.PopupControl;
        private Pacient p;
        private int mode; // 0- adaugare, 1- editare
        public AdaugarePacient(Pacient p, int mode)
        {
            InitializeComponent();
            this.p = p;
            this.mode = mode;
            popup.BindControl(this.flowLayoutPanel1, this.pictureBox2, this.label6);
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            try
            {
                p.Adresa = textBoxAdresa.Text;
                p.Alergii = textBoxAlergii.Text;
                p.AntecedenteHeredoColaterale = textBoxAntecedenteHC.Text;
                p.AntecedentePersonale = textBoxAntecedenteP.Text;
                p.Cetatenie = textBoxCetatenie.Text;
                p.ConditiiMunca = textBoxConditiiMunca.Text;
                p.DataNasterii = dateTimePicker1.Value;
                p.Email = textBoxEmail.Text;
                p.GrupSangvin = comboBoxGrupSangvin.SelectedItem.ToString();
                if(float.TryParse(textBoxInaltime.Text, out float a) == true)
                {
                    p.Inaltime = float.Parse(textBoxInaltime.Text);
                }
                else
                {
                    throw new Exception("Inaltime incorecta. Este necesar un numar valid!");
                }
                if (float.TryParse(textBoxInaltime.Text, out float a1) == true)
                {
                    p.Masa = float.Parse(textBoxMasa.Text);
                }
                else
                {
                    throw new Exception("Inaltime incorecta. Este necesar un numar valid!");
                }
                if (EsteNumar(textBoxTelefon.Text))
                    p.NrTelefon = textBoxTelefon.Text;
                else throw new Exception("Numar telefon invalid");
                p.Nume = textBoxNume.Text;
                p.Ocupatie = textBoxOcupatie.Text;
                p.Prenume = textBoxPrenume.Text;
                p.StatutSocial = textBoxStatusSocial.Text;
                p.RH = radioButtonRHNeg.Checked == true ? false : true;
                p.Sex = radioButtonSexM.Checked == true ? true : false;
                if (textBoxCNP.Text.Length == 13 && EsteNumar(textBoxCNP.Text) == true)
                    p.CNP = textBoxCNP.Text;
                else throw new Exception("CNP-ul trebuie sa contina 13 numere");

              

                this.DialogResult = DialogResult.OK;
            }
            catch(Exception exp)
            {
                popup.DisplayPopup("danger", exp.Message, 3000);
            }
            
        }

        public Pacient GetObject { get { return this.p; } }

        private bool EsteNumar(String n)
        {
            foreach(char c in n)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}

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
        private Pacient p;
        private int mode; // 0- adaugare, 1- editare
        public AdaugarePacient(Pacient p, int mode)
        {
            InitializeComponent();
            this.p = p;
            this.mode = mode;
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
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
            p.Inaltime = float.Parse(textBoxInaltime.Text);
            p.Masa = float.Parse(textBoxMasa.Text);
            p.NrTelefon = textBoxTelefon.Text;
            p.Nume = textBoxNume.Text;
            p.Ocupatie = textBoxOcupatie.Text;
            p.Prenume = textBoxPrenume.Text;
            p.StatutSocial = textBoxStatusSocial.Text;
            p.RH = radioButtonRHNeg.Checked == true ? false : true;
            p.Sex = radioButtonSexM.Checked == true ? true : false;
            p.CNP = textBoxCNP.Text;


            this.DialogResult = DialogResult.OK;
        }

        public Pacient GetObject { get { return this.p; } }
    }
}

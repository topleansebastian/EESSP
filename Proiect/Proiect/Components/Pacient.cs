using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Components
{
    public class Pacient
    {
        public String Nume { get; set; }
        public String Prenume { get; set; }
        public DateTime DataNasterii { get; set; }
        public bool Sex { get; set; }
        public String StatutSocial { get; set; }
        public String Adresa { get; set; }
        public String NrTelefon { get; set; }
        public String Email { get; set; }
        public String Cetatenie { get; set; }
        public String GrupSangvin { get; set; }
        public bool RH { get; set; }
        public float Masa { get; set; }
        public float Inaltime { get; set; }
        public String Alergii { get; set; }
        public String Ocupatie { get; set; }
        public String AntecedenteHeredoColaterale { get; set; }
        public String AntecedentePersonale { get; set; }
        public String ConditiiMunca { get; set; }
        public String CNP { get; set; }
    }
}

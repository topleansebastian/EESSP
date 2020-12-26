using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect.Components
{
    class Popup
    {
        private FlowLayoutPanel panel;
        private PictureBox picture;
        private Label text;

        private Timer timer = new Timer();


        private bool canDisplay = true;

        private Popup()
        {
   
            
        }

        public void BindControl(FlowLayoutPanel panel, PictureBox picture, Label text)
        {
            this.panel = panel;
            this.picture = picture;
            this.text = text;

            this.text.ForeColor = Color.White;
            this.picture.Size = new Size(54, 54);
            this.text.AutoSize = true;
            this.text.Size = new Size(120, 30);
            this.picture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public static Popup PopupControl = new Popup();


        public void DisplayPopup(String type, String message, int timeout)
        {
            this.canDisplay = false;
            this.text.Text = message;
            


            this.timer.Interval = timeout;
            this.timer.Tick += Timer_Tick;
            

            switch (type.ToLower())
            {
                case "danger":
                    this.panel.BackColor = Color.Red;
                    this.picture.ImageLocation = @"C:\Users\Seba\Desktop\Proiect BDIS_EESSP\Proiect\Proiect\Images\danger.png";
                    break;
                case "info":
                    this.panel.BackColor = Color.DarkTurquoise;
                    this.picture.ImageLocation = @"C:\Users\Seba\Desktop\Proiect BDIS_EESSP\Proiect\Proiect\Images\info.png";

                    break;
                case "success":
                    this.panel.BackColor = Color.Green;
                    this.picture.ImageLocation = @"C:\Users\Seba\Desktop\Proiect BDIS_EESSP\Proiect\Proiect\Images\success.png";
                    break;
            }

            this.panel.Controls.Add(this.picture);
            this.panel.Controls.Add(this.text);
            this.panel.Visible = true;
            this.timer.Start();
        }

        public void RemovePopup()
        {
            this.timer.Stop();
            this.panel.Visible = false;
            this.canDisplay = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            this.panel.Visible = false;
            this.timer.Stop();
            this.canDisplay = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sheet_Designation_all_type
{
    public partial class Form1 : Form
    {
        Graphics grid_box;
        Graphics half_box;
        //Origion 
        double delta_lambda = 0;
        double delta_phi = 0;
        int grid_number = 0;
        char plot_ID = 'A';
        int y_in_million = 0;
        int x_in_million = 0;

        public Form1()
        {
            InitializeComponent();
        }
        public void caculate_number(double latitude,double longitude)
        {
            //cauculate 1:100w
            int a = (int)Math.Floor(latitude / 4) + 1;
            int b = (int)Math.Floor(longitude / 6) + 31;
            int c = (int)(4 / delta_phi) - (int)Math.Floor(latitude % 4 / delta_phi);
            int d = (int)Math.Floor((longitude % 6) / delta_lambda) + 1;
            y_in_million = a;
            x_in_million = b-31;
            label1.Text = (char)(a + 64) + b.ToString().PadLeft(2, '0') + plot_ID + c.ToString().PadLeft(3, '0') + d.ToString().PadLeft(3, '0');
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            //load plotting scale
            comboBox1.Items.Add("1:50w");
            comboBox1.Items.Add("1:25w");
            comboBox1.Items.Add("1:10w");
            comboBox1.Items.Add("1:5w");




            //draw the half box
            Pen penline = new Pen(Color.Blue);
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Focus();
            half_box = pictureBox2.CreateGraphics();
            Bitmap new1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics gpn = Graphics.FromImage(new1);

            Pen pointpen = new Pen(Color.Green, 2);
            float basic_gap = pictureBox2.Height / 22;

            for (int i = 0; i < 22; i++)
            {
                gpn.DrawPie(penline, pictureBox2.Width / 2 - (i + 1) * basic_gap, -(i + 1) * basic_gap, (i + 1) * basic_gap * 2, (i + 1) * basic_gap * 2, 0, 180);
            }

            for (int i = 0; i < 180; i += 6)
            {
                gpn.DrawPie(penline, pictureBox2.Width / 2 - 22 * basic_gap, -22 * basic_gap, 22 * basic_gap * 2, 22 * basic_gap * 2, i, 180);
            }
            pictureBox2.BackgroundImage = new1;
            pictureBox2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //eco
            int grid_box_width = pictureBox1.Width;
            int grid_box_height = pictureBox1.Height;


            //Judge the Selected
            if (delta_lambda==0 && delta_phi==0 && grid_number==0)
            {
                MessageBox.Show("Please selected plotting scale");
            }else
            {
                // Draw the grid Box
                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
                pictureBox1.Focus();
                grid_box = pictureBox1.CreateGraphics();
                Bitmap pic1 = new Bitmap(grid_box_width, grid_box_height);
                Graphics gb = Graphics.FromImage(pic1);
                Pen penline = new Pen(Color.Blue);
                Font font1 = new Font("Cambria Math", 200 / grid_number, FontStyle.Regular);

                for (int i=0;i <= (grid_number + 1); i++)     //绘制竖线
                {
                    gb.DrawLine(penline, (i + 1) * grid_box_width / (grid_number + 1), 0, (i + 1) * grid_box_width / (grid_number + 1), grid_box_height);
                    gb.DrawString(Convert.ToString(i), font1, Brushes.Black, i * grid_box_width / (grid_number + 1), 0);
                }
                for (int i = 0; i <= (grid_number + 1); i++)      //绘制横线
                {
                    gb.DrawLine(penline, 0, (i + 1) * grid_box_height / (grid_number + 1), grid_box_width, (i + 1) * grid_box_height / (grid_number + 1));
                    gb.DrawString(Convert.ToString(i), font1, Brushes.Black, 0, i * grid_box_height / (grid_number + 1));
                }




                caculate_number((double)this.numericUpDown1.Value, (double)this.numericUpDown2.Value);

                double x = Math.Abs((double)this.numericUpDown1.Value - y_in_million * 4) / 4 * (pictureBox1.Width - pictureBox1.Width / (grid_number + 1)) + pictureBox1.Width / (grid_number + 1);
                double y = Math.Abs((double)this.numericUpDown2.Value - x_in_million * 6) / 6 * (pictureBox1.Height-pictureBox1.Height/(grid_number+1))+ pictureBox1.Height / (grid_number + 1);

                Pen penpoint = new Pen(Color.Red, 2);
                gb.DrawRectangle(penpoint, (float)x, (float)y, 2, 2);

                //刷线图像
                pictureBox1.BackgroundImage = pic1;
                pictureBox1.Refresh();              







                //Draw the 100w 
                pictureBox2.BorderStyle = BorderStyle.FixedSingle;
                pictureBox2.Focus();
                half_box = pictureBox2.CreateGraphics();
                Bitmap new1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                Graphics gpn = Graphics.FromImage(new1);

                Pen pointpen = new Pen(Color.Green, 2);
                Pen redpen = new Pen(Color.Red, 2);
                float basic_gap = pictureBox2.Height / 22;

                for (int i = 0; i < 22; i++)
                {
                    gpn.DrawPie(penline, pictureBox2.Width / 2 - (i + 1) * basic_gap, -(i + 1) * basic_gap, (i + 1) * basic_gap * 2, (i + 1) * basic_gap * 2, 0, 180);
                }

                for (int i = 0; i < 180; i += 6)
                {
                    gpn.DrawPie(penline, pictureBox2.Width / 2 - 22 * basic_gap, -22 * basic_gap, 22 * basic_gap * 2, 22 * basic_gap * 2, i, 180);
                }
                gpn.DrawPie(redpen, pictureBox2.Width / 2 - (22 - y_in_million + 1) * basic_gap, -(22 - y_in_million + 1) * basic_gap, (22 - y_in_million + 1) * basic_gap * 2, (22 - y_in_million + 1) * basic_gap * 2, 180 - x_in_million * 6, 6);
                gpn.DrawPie(pointpen, pictureBox2.Width / 2 - (22 - y_in_million) * basic_gap, -(22 - y_in_million) * basic_gap, (22 - y_in_million) * basic_gap * 2, (22 - y_in_million) * basic_gap * 2, 180 - x_in_million * 6, 6);
               

                pictureBox2.BackgroundImage = new1;
                pictureBox2.Refresh();
            }
                

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "1:50w":
                    delta_lambda = 3; delta_phi = 2; grid_number = 2; plot_ID = 'B'; break;
                case "1:25w":
                    delta_lambda = 1.5; delta_phi = 1; grid_number = 4; plot_ID = 'C'; break;
                case "1:10w":
                    delta_lambda = 3 / 6.0; delta_phi = 20 / 60.0; grid_number = 12; plot_ID = 'D'; break;
                case "1:5w":
                    delta_lambda = 15 / 60.0; delta_phi = 10 / 60.0; grid_number = 24; plot_ID = 'E'; break;
                default:
                    MessageBox.Show("Please selected plotting scale"); break;
            }
            if (delta_lambda == 0 && delta_phi == 0 && grid_number == 0)
            {
                MessageBox.Show("Please selected plotting scale");
            }
            else
            {
                int grid_box_width = pictureBox1.Width;
                int grid_box_height = pictureBox1.Height;
                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
                pictureBox1.Focus();
                grid_box = pictureBox1.CreateGraphics();
                Bitmap pic1 = new Bitmap(grid_box_width, grid_box_height);
                Graphics gb = Graphics.FromImage(pic1);
                Pen penline = new Pen(Color.Blue);
                Font font1 = new Font("Cambria Math", 200 / grid_number, FontStyle.Regular);

                for (int i = 0; i <= (grid_number + 1); i++)     //axis-y
                {
                    gb.DrawLine(penline, (i + 1) * grid_box_width / (grid_number + 1), 0, (i + 1) * grid_box_width / (grid_number + 1), grid_box_height);
                    gb.DrawString(Convert.ToString(i), font1, Brushes.Black, i * grid_box_width / (grid_number + 1), 0);
                }
                for (int i = 0; i <= (grid_number + 1); i++)      //axis-x
                {
                    gb.DrawLine(penline, 0, (i + 1) * grid_box_height / (grid_number + 1), grid_box_width, (i + 1) * grid_box_height / (grid_number + 1));
                    gb.DrawString(Convert.ToString(i), font1, Brushes.Black, 0, i * grid_box_height / (grid_number + 1));
                }
                pictureBox1.BackgroundImage = pic1;
                pictureBox1.Refresh();
            }


        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            button1_Click(null,null);
        }
    }
}

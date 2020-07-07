using System;
using System.Windows.Forms;

namespace TextEditor2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string GetText1
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string GetText2
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public string GetText3
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public bool Validation()
        {
            try
            {
                if (Convert.ToInt32(textBox3.Text) < 0)
                    return false;
                else
                    return true;
            }
            catch { return false; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Validation())
            { 
            Form1.row.Cells[0].Value = textBox1.Text;
            Form1.row.Cells[1].Value = textBox2.Text;
            Form1.row.Cells[2].Value = textBox3.Text;
            Close();
            }
            else
            {
                string message = "Wrong input of price";
                string caption = "Error";
                MessageBox.Show(message,caption,MessageBoxButtons.OK ,MessageBoxIcon.Exclamation); 
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}

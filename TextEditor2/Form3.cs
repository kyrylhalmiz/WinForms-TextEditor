using System;
using System.Windows.Forms;

namespace TextEditor2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
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
        private void button1_Click(object sender, EventArgs e)
        {

            if (Validation())
            {
                Form1.row.Cells[0].Value = textBox1.Text;
                Form1.row.Cells[1].Value = textBox2.Text;
                Form1.row.Cells[2].Value = textBox3.Text;
                this.Close();
            }
            else
            {
                string message = "Wrong input of price";
                string caption = "Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

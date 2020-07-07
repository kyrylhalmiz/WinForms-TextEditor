using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TextEditor2
{
    public partial class Form1 : Form
    {
        public static int indexRow;
        public static DataGridViewRow row;
        public Form1()
        {
            InitializeComponent();
        }
        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            openFileDialog.Filter = "CSV files (*.csv) |*.csv";
            openFileDialog.ShowDialog();


            DataTable dt = new DataTable() { TableName = "myTable" };

            string[] lines = File.ReadAllLines(openFileDialog.FileName);
            if (lines.Length > 0)
            {

                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(';');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string word in headerLabels)
                    {
                        dr[word] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            Text = Path.GetFileName(openFileDialog.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] line = new string[dataGridView1.ColumnCount];
            using (StreamWriter writer = new StreamWriter(openFileDialog.FileName))
            {
                Text = Path.GetFileName(openFileDialog.FileName);
                string[] headers = new string[dataGridView1.ColumnCount];
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    headers[i] = dataGridView1.Columns[i].Name;
                }
                writer.WriteLine(string.Join(";", headers));
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        line[j] = (string)(dataGridView1.Rows[i].Cells[j].Value ?? "");
                    }
                    writer.WriteLine(string.Join(";", line));
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int columnCount = dataGridView1.ColumnCount;
            string[] line = new string[columnCount];
            saveFileDialog1.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Text = Path.GetFileName(openFileDialog.FileName);
                var extension = Path.GetExtension(saveFileDialog1.FileName);
                switch (extension)
                {
                    case ".xml":

                        using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                        {

                            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                            serializer.Serialize(writer, dataGridView1.DataSource);

                        }
                        break;
                    case ".csv":
                        using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                        {
                            string[] headers = new string[dataGridView1.ColumnCount];

                            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                            {
                                headers[i] = dataGridView1.Columns[i].Name;
                            }
                            writer.WriteLine(string.Join(";", headers));

                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                {
                                    line[j] = (string)(dataGridView1.Rows[i].Cells[j].Value ?? "");
                                }
                                writer.WriteLine(string.Join(";", line));

                            }
                        }
                        break;
                    default: break;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.GetText1 = row.Cells[0].Value.ToString();
            f2.GetText2 = row.Cells[1].Value.ToString();
            f2.GetText3 = row.Cells[2].Value.ToString();

            f2.ShowDialog();
            Text = $"{Path.GetFileName(openFileDialog.FileName)}*";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            indexRow = dataGridView1.RowCount;
            AddRow((DataTable)dataGridView1.DataSource);
            Text = $"{Path.GetFileName(openFileDialog.FileName)}*";
            row = dataGridView1.Rows[indexRow];
            Form3 form3 = new Form3();
            form3.ShowDialog();
            Text = $"{Path.GetFileName(openFileDialog.FileName)}*";
        }
        

        private void AddRow(DataTable table)
        {
            DataRow newRow = table.NewRow();
            table.Rows.Add(newRow);
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexRow = e.RowIndex;
                row = dataGridView1.Rows[indexRow];
            }
            catch { }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            menuToolStripMenuItem.Enabled = false;
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            toolStripProgressBar1.Increment(3);
            double percent = (Convert.ToDouble(toolStripProgressBar1.Value) / toolStripProgressBar1.Maximum) * 100;
            toolStripTextBox1.Text = $"Caclulations :{percent}%";
            if (toolStripProgressBar1.Value >= toolStripProgressBar1.Maximum)
            {
                timer1.Stop();

                DialogResult res = MessageBox.Show("Complete", "Calculations status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!timer1.Enabled)
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    menuToolStripMenuItem.Enabled = true;
                    toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

                }
            }
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }
    }

}

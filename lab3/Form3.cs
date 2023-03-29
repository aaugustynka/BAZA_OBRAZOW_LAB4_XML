using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form3 : Form
    {
        Form1 form;
        string wyszukiwarka;
        public Form3(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            for (int i = 0; i < form.CountRows(); i++)
            {
                DataGridViewRow row = form.GetRows(i);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(wyszukiwarka) == true)
                    {
                        DataGridViewRow row_copy = (DataGridViewRow)row.Clone();
                        row_copy.Cells[0].Value = row.Cells[0].Value;
                        row_copy.Cells[1].Value = row.Cells[1].Value;
                        row_copy.Cells[2].Value = row.Cells[2].Value;
                        row_copy.Cells[3].Value = row.Cells[3].Value;
                        dataGridView2.Rows.Add(row_copy);
                        break;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            wyszukiwarka = textBox1.Text;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

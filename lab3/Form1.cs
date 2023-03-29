using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public string rok_t, technika_t, sciezka, tytul_t, tworca_t;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            bool empty = true;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                empty = true;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                        empty = false;
                }
                if (empty == false)
                    dataGridView1.Rows.RemoveAt(row.Index);
            }
        }

        public void dataGrid_Add(string tytul, string tworca, string rok, string technika)
        {
            dataGridView1.Rows.Add(new object[] { tytul, tworca, rok, technika });
            tytul_t = ""; tworca_t = ""; rok_t = ""; technika_t = "";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dataGrid_Load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGrid_Save();

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void dataGrid_Save()
        {
            label1.Text = "";
            int l = 0; bool empty = true;
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter sw = new StreamWriter(sfd.FileName, true, Encoding.ASCII);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        l = 0;
                        empty = true;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null)
                                empty = false;
                        }
                        if (empty == false)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (l == 0)
                                    sw.Write(cell.Value);
                                else
                                    sw.Write("," + cell.Value);
                                l++;
                            }
                            sw.Write("\n");
                        }
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                label1.Text = e.Message;
            }
        }

        public void dataGrid_Load()
        {
            label1.Text = "";
            try
            {
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.Filter = "CSV|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = System.IO.File.ReadAllLines(sfd.FileName);
                    foreach (string line in lines)
                    {
                        string[] cells = line.Split(',');
                        dataGridView1.Rows.Add(new object[] { cells[0], cells[1], cells[2], cells[3] });
                    }
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(this);
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGrid_SavetoXML();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGrid_LoadfromXML();
        }

        public void dataGrid_LoadfromXML()
        {
            label1.Text = "";
            try
            {
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.Filter = "XML|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(sfd.FileName);
                    dataGridView1.Rows.Clear();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        DataGridViewRow row2 = new DataGridViewRow();
                        foreach (DataColumn column in ds.Tables[0].Columns)
                        {
                            row2.Cells.Add(new DataGridViewTextBoxCell { Value = row[column] });
                        }
                        dataGridView1.Rows.Add(row2);
                    }
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }

        }


        public void dataGrid_SavetoXML()
        {
            label1.Text = ""; bool empty = true;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("TYTUŁ");
                DataColumn dc2 = new DataColumn("TWÓRCA");
                DataColumn dc3 = new DataColumn("ROK POWSTANIA");
                DataColumn dc4 = new DataColumn("TECHNIKA");
                dt.Columns.Add(dc);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    empty = true;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                            empty = false;
                    }
                    if (empty == false)
                    {
                        DataRow dr = dt.NewRow();
                        if (row.Cells[0].Value != null)
                            dr["TYTUŁ"] = row.Cells[0].Value.ToString();
                        else
                            dr["TYTUŁ"] = "";
                        if (row.Cells[1].Value != null)
                            dr["TWÓRCA"] = row.Cells[1].Value.ToString();
                        else
                            dr["TWÓRCA"] = "";
                        if (row.Cells[2].Value != null)
                            dr["ROK POWSTANIA"] = row.Cells[2].Value.ToString();
                        else
                            dr["ROK POWSTANIA"] = "";
                        if (row.Cells[3].Value != null)
                            dr["TECHNIKA"] = row.Cells[3].Value.ToString();
                        else
                            dr["TECHNIKA"] = "";
                        dt.Rows.Add(dr);
                    }
                }
                ds.Tables.Add(dt);
                DataGridView dataGridView2 = new DataGridView();
                dataGridView2.DataSource = ds.Tables[0];
                DataTable dt2 = (DataTable)dataGridView2.DataSource;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    dt2.WriteXml(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

     
        public DataGridViewRow GetRows(int index)
        {
            return dataGridView1.Rows[index];
        }

        public int CountRows()
        {   
            return dataGridView1.Rows.Count; 
        
        }
    }
}






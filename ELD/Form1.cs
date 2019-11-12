using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace ELD
{
    public partial class Form1 : Form
    {
        static string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\ELD\ELD\Files\Database1.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }
        public void Search()
        {
            try
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    SqlCommand com;
                    com = new SqlCommand("SELECT * FROM [dbo].[Document] WHERE [ID] LIKE N'%" + textBox1.Text + "%'  OR [Title] LIKE N'%" + textBox1.Text + "%' OR [Date] LIKE N'%" + textBox1.Text + "%' OR [Location] LIKE N'%" + textBox1.Text + "%'", conn);
                    SqlDataReader sqlR = com.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    dataGridView1.Rows.Clear();
                    while (sqlR.Read())
                    {
                        data.Add(new string[4]);
                        data[data.Count - 1][0] = sqlR[0].ToString();
                        data[data.Count - 1][1] = sqlR[1].ToString();
                        data[data.Count - 1][2] = sqlR[2].ToString();
                        data[data.Count - 1][3] = sqlR[3].ToString();
                    }
                    sqlR.Close();
                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    {

                        DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                        if (result == DialogResult.OK) // Test result.
                        {
                            
                            string file = openFileDialog1.FileName;
                            string[] f = file.Split('\\');
                            string title = f[(f.Length) - 1];
                            string location = @"C:\Users\User\Desktop\ELD\ELD\Files\" + title;
                            File.Copy(file, location, true);
                            DateTime aDateTime = DateTime.Now;
                            
                            SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Document] WHERE [Title] = N'" + title + "'", conn);
                            int i = Convert.ToInt32(prov.ExecuteScalar());
                            if (i == 0)
                            {
                                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Document] (Title, Location, Date) Values (N'" + title + "', N'" + location + "', N'" + aDateTime + "')", conn);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Документ добавлен!");
                            }
                            else
                            {
                                MessageBox.Show("Названия не должны повторяться. Переименуйте файл и повторите попытку!");
                            }
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Search();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            Search();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Process.Start(@"C:\Users\User\Desktop\ELD\ELD\Files\" + s);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

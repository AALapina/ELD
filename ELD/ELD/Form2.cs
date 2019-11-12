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

namespace ELD
{
    public partial class Form2 : Form
    {
        static string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        SqlCommand cmd;
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    if (((!(string.IsNullOrEmpty(textBox2.Text)) && !(string.IsNullOrWhiteSpace(textBox2.Text))) && (!(string.IsNullOrEmpty(textBox3.Text)) && !(string.IsNullOrWhiteSpace(textBox3.Text))) && (!(string.IsNullOrEmpty(textBox4.Text)) && !(string.IsNullOrWhiteSpace(textBox4.Text)))))
                    {
                        SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[All] WHERE [Title] = '" + textBox2.Text + "'", conn);
                        int i = Convert.ToInt32(prov.ExecuteScalar());
                        if (i == 0)
                        {

                            using (var cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = string.Format("INSERT INTO [dbo].[All] ([Title], [Date], [Author])" +
                                "VALUES (@Title, @Date, @Author);");
                                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = textBox2.Text;
                                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = textBox3.Text;
                                cmd.Parameters.Add("@Author", SqlDbType.NVarChar, 50).Value = textBox4.Text;
                                cmd.ExecuteNonQuery();
                            }
                            MessageBox.Show("Документ добавлен");
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнены!");
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
                                // to get the only file name
                            string fn = f[(f.Length) - 1];
                            string dest = @"C:\Users\User\source\repos\WindowsFormsApp1\WindowsFormsApp1\" + fn;

                                //to copy the file to the destination folder
                            File.Copy(file, dest, true);

                                //to save to the database
                            string q = "insert into [data_file] values('" + fn + "','" + dest + "')";
                            cmd = new SqlCommand(q, conn);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("success");
                        }
                        
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}

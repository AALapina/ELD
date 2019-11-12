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
        static string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\ELD\ELD\Files\Database1.mdf;Integrated Security=True";

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
                    if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                        int i = Convert.ToInt32(prov.ExecuteScalar());
                        if (i == 1)
                        {
                            SqlCommand com = new SqlCommand("SELECT [ID] FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                            textBox1.Text = com.ExecuteScalar().ToString();
                            com = new SqlCommand("SELECT [Title] FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                            textBox2.Text = com.ExecuteScalar().ToString();
                            com = new SqlCommand("SELECT [Location] FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                            textBox3.Text = com.ExecuteScalar().ToString();
                        }
                        else
                        {
                            MessageBox.Show("Документа с таким ID не существует!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Поле ID должно быть заполнено!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    DialogResult result = MessageBox.Show("Вы точно хотите удалить документ?", "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        conn.Open();
                        if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                        {
                            SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                            int i = Convert.ToInt32(prov.ExecuteScalar());
                            if (i == 1)
                            {
                                SqlCommand com = new SqlCommand("DELETE FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                                textBox2.Text = string.Empty;
                                textBox3.Text = string.Empty;
                                SqlCommand f = new SqlCommand("SELECT Location FROM [dbo].[Document] WHERE [Id] = '" + textBox1.Text + "'", conn);
                                string j = Convert.ToString(f.ExecuteScalar());
                                File.Delete(j);

                                com.ExecuteNonQuery();
                                MessageBox.Show("Документ удален!");
                            }
                            else
                            {
                                MessageBox.Show("Документа с таким ID не существует!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле ID должно быть заполнено!");
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

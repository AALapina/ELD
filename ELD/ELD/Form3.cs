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

namespace ELD
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        static string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("Хотите сохранить изменения?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (var conn = new SqlConnection(ConnString))
                    {
                        conn.Open();
                        if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                        {
                            SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                            int i = Convert.ToInt32(prov.ExecuteScalar());
                            if (i == 1)
                            {
                                SqlCommand com = new SqlCommand("UPDATE [dbo].[All] SET [ID] = N'" + textBox1.Text + "', [Date] = N'" + textBox3.Text + "', [Author] = N'" + textBox4.Text + "', [Title] = '" + textBox2.Text + "'", conn);
                                com.ExecuteNonQuery();
                                MessageBox.Show("Изменения сохраненны");
                            }
                            else
                            {
                                MessageBox.Show("Документа с таким ID не существует!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Все поля должны быть заполнены!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
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
                        SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                        int i = Convert.ToInt32(prov.ExecuteScalar());
                        if (i == 1)
                        {
                            SqlCommand com = new SqlCommand("SELECT [ID] FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                            textBox3.Text = com.ExecuteScalar().ToString();
                            com = new SqlCommand("SELECT [Date] FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                            textBox4.Text = com.ExecuteScalar().ToString();
                            com = new SqlCommand("SELECT [Author] FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                            textBox2.Text = com.ExecuteScalar().ToString();
                            com = new SqlCommand("SELECT [Title] FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);

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
                            SqlCommand prov = new SqlCommand("SELECT COUNT(*) FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                            int i = Convert.ToInt32(prov.ExecuteScalar());
                            if (i == 1)
                            {
                                SqlCommand com = new SqlCommand("DELETE FROM [dbo].[All] WHERE [ID] = '" + textBox1.Text + "'", conn);
                                textBox2.Text = string.Empty;
                                textBox3.Text = string.Empty;
                                textBox4.Text = string.Empty;

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

﻿using System;
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
        static string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\ELD\ELD\Files\Database1.mdf;Integrated Security=True";
        public Form3()
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
        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

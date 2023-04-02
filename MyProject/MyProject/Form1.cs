using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProject
{
    public partial class Form1 : Form
    {
        int a = 0;
        int b = 0;
        int c1, c2, c3 = 0;
        string d1, d2, d3 = "";
        int x = 0;
        string ConS = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\DBY.mdb";
        private OleDbConnection dbCon;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) // Выход
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Регистрация
        {
            if (a == 0)
            {
                c1 = 0;
                c2 = 0;
                c3 = 0;
                b = 0;
                x = 0;
                textBox1.Visible = true;
                textBox1.Text = "Введите логин";
                textBox1.ForeColor = Color.Gray;

                textBox2.Visible = true;
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Введите пароль";
                textBox2.ForeColor = Color.Gray;

                textBox3.Visible = true;
                textBox3.Text = "Повторите пароль";
                textBox3.ForeColor = Color.Gray;

                checkBox1.Visible = false;
                a = 1;
            }
            if (a == 1 || a>0 || a!=0)
            {
                if (textBox1.Text != "" && textBox1.Text != "Введите логин" || textBox2.Text != "" && textBox2.Text != "Введите пароль" || textBox3.Text != "" && textBox3.Text != "Повторите пароль")
                {
                    string s1 = textBox1.Text;
                    string s2 = textBox2.Text;
                    string q = "INSERT INTO accounts (login, pass) VALUES (@login, @pass)";
                    if (textBox2.Text == textBox3.Text)
                    {
                        try
                        {
                            dbCon = new OleDbConnection(ConS);
                            dbCon.Open();
                            using (dbCon)
                            {
                                OleDbCommand com = new OleDbCommand(q, dbCon);
                                com.Parameters.AddWithValue("@login", s1);
                                com.Parameters.AddWithValue("@pass", s2);
                                com.ExecuteNonQuery();
                            }
                            dbCon.Close();
                            MessageBox.Show("Регистрация успешна!.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            a = 0;
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка соединения с БД!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show(Convert.ToString(a));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли не свопадают, введите пароль заново!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox2.Text = "";
                        textBox3.Text = "";
                        return;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Вход
        {
            if (b == 0)
            {
                c1 = 0;
                c2 = 0;
                c3 = 0;
                a = 0;
                x = 1;
                textBox1.Visible = true;
                textBox1.Text = "Введите логин";
                textBox1.ForeColor = Color.Gray;

                textBox2.Visible = true;
                textBox2.Text = "Введите пароль";
                textBox2.ForeColor = Color.Gray;

                textBox3.Visible = false;
                textBox3.Text = "Повторите пароль";
                textBox3.ForeColor = Color.Gray;

                checkBox1.Visible = true;
                b = 1;
            }
            if (b == 1 || b > 0 || b != 0)
            {
                if (textBox1.Text != "" && textBox1.Text != "Введите логин" || textBox2.Text != "" && textBox2.Text != "Введите пароль")
                {
                    string s1 = textBox1.Text;
                    string s2 = textBox2.Text;
                    string q = "SELECT COUNT(*) FROM accounts WHERE (login = \"" + s1 + "\" AND pass = \"" + s2 + "\")";
                    if (textBox1.Text != textBox2.Text)
                    {
                        try
                        {
                            dbCon = new OleDbConnection(ConS);
                            dbCon.Open();
                            using (dbCon)
                            {
                                OleDbCommand com = new OleDbCommand(q, dbCon);
                                string res = com.ExecuteScalar().ToString();
                                com.ExecuteNonQuery();
                                if (res != "0")
                                {
                                    MessageBox.Show("Добро пожаловать!.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Неверный Логин/Пароль!.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    return;
                                }
                            }
                            dbCon.Close();
                            b = 0;
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка соединения с БД!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show(Convert.ToString(a));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин и пароль не могут совпадать!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        return;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //событие чекбокса
        {
            if (checkBox1.Checked == true && checkBox1.Visible == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else 
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
        //-----------------------------------------------------------------------События текстбоксов 1 2 3
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (c1 == 0)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
                c1 = 1;
            }
            if (c1 == 1 || c1>0 || c1!=0)
            {
                d1 = textBox1.Text;
                textBox1.Text = d1;
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (c2 == 0)
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                c2 = 1;
            }
            if (c2 == 1 || c2 > 0 || c2 != 0)
            {
                d2 = textBox2.Text;
                textBox2.Text = d2;
            }
            if (x == 1)
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (c3 == 0)
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
                c3 = 1;
            }
            if (c3 == 1 || c3 > 0 || c3 != 0)
            {
                d3 = textBox3.Text;
                textBox3.Text = d3;
            }
        }
    }
}

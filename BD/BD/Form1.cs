using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace BD
{
    public partial class Form1 : Form
    {
        const string msgcon = "Host=localhost;Port=5432;Username=postgres;Password=P@ssw0rd;Database=DB";
        bool status_connection = false;
        string command;
        string txt;
        public Form1()
        {
            InitializeComponent();
            textBox1.Multiline = true; // Разрешаем многострочный ввод
            textBox1.ScrollBars = ScrollBars.Vertical; // Устанавливаем вертикальную полосу прокрутки
            label1.Text = txt;
            label1.Text = txt + " NOT CONNECTED";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            status_connection = true;
            label1.Text = txt + " OK";
            using (var connection = new NpgsqlConnection(msgcon))
            {
                if (status_connection)
                {
                    connection.Open();
                }
            }
        }
        private void GetSQL(string sql)
        {
            using (var cmd = new NpgsqlCommand(sql))
            {
                var adapter = new NpgsqlDataAdapter(sql, msgcon);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                // Привязываем DataTable к DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }
        private void GetTable(int id)
        {
            if (status_connection)
            {
                switch (id)
                {
                    case 1: command = "SELECT * FROM faculties;"; break;
                    case 2: command = "SELECT * FROM groups;"; break;
                    case 3: command = "SELECT * FROM students;"; break;
                    case 4: command = "SELECT * FROM lecturers;"; break;
                    case 5: command = "SELECT * FROM disciplines;"; break;
                    case 6: command = "SELECT * FROM exams;"; break;

                }
                using (var cmd = new NpgsqlCommand(command))
                {
                    var adapter = new NpgsqlDataAdapter(command, msgcon);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            else 
            { 
                label1.Text = txt + "ERROR"; 
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GetTable(1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            GetTable(2);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            GetTable(3);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            GetTable(4);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            GetTable(5);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            GetTable(6);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && status_connection)
            {
                GetSQL(textBox1.Text);
            }
            else
            {
                label1.Text = txt + "ERROR";
                return; 
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            status_connection = false;
            label1.Text = txt + " DISCONNECT";
        }
    }
}

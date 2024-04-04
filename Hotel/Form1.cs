using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Hotel
{
    
    public partial class Form1 : Form
    {
        DB db = new DB();

        int selectedrow;
        public Form1()
        {
            InitializeComponent();           
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("firstname", "Имя");
            dataGridView1.Columns.Add("secondname", "Фамилия");
            dataGridView1.Columns.Add("thirdname", "Отчество");
            dataGridView1.Columns.Add("status", "Статус");
            dataGridView1.Columns.Add("dateon", "Дата заезда");
            dataGridView1.Columns.Add("dateoff", "Дата выезда");
            dataGridView1.Columns.Add("birthday", "День рождения");
        }

        private void ReadRow(DataGridView dgw, IDataReader record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetDateTime(5), record.GetDateTime(6), record.GetDateTime(7));
        }

        private void RefreshdataView(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string selstring = $"select * from users";

            MySqlCommand command = new MySqlCommand(selstring, db.GetConnection());

            db.OpenConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadRow(dgw, reader);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (richTextBox1.Text == "Поиск...")
                richTextBox1.Text = "";   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Start();
            CreateColums();
            RefreshdataView(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedrow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedrow];

                label5.Text = row.Cells[4].Value.ToString();
                label6.Text = row.Cells[1].Value.ToString() + " " + row.Cells[2].Value.ToString() + " " + row.Cells[3].Value.ToString();
                label7.Text = "Дата въезда " + row.Cells[5].Value.ToString();
                label8.Text = "Дата выезда " + row.Cells[6].Value.ToString();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            richTextBox2.Text = DateTime.Now.ToLongTimeString();
        }
    }
}

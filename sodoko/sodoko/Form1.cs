using System.IO;
using System.Windows.Forms;

namespace sodoko
{
    public partial class Form1 : Form
    {
        TextBox[,] cells;
        public Form1()
        {
            InitializeComponent();

            tableLayoutPanel1.BackgroundImage = Properties.Resources._01;

            cells = new TextBox[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new TextBox();
                    cells[i, j].Multiline = true;
                    cells[i, j].TextAlign = HorizontalAlignment.Center;
                    cells[i, j].Font = new Font("Tahoma", 16);
                    cells[i, j].Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
                    tableLayoutPanel1.Controls.Add(cells[i, j]);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Click += new EventHandler(textBox_Click);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].TextChanged += new EventHandler(textBox_TextChanged);
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();

            if (x.ShowDialog() == DialogResult.OK)
            {
                reset();
                string file_path = x.FileName;

                StreamReader my_file_reader = new StreamReader(file_path);

                string big_text = my_file_reader.ReadToEnd();

                char[] my_seperators = { ' ', '\r' };
                big_text = big_text.Replace("\n", "");

                string[] numbers = big_text.Split(my_seperators);

                int index = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (numbers[index] != "0")
                        {
                            cells[j, i].ReadOnly = true;
                            cells[j, i].Text = numbers[index];
                        }
                        index++;
                    }
                }
            }
        }
        private void reset()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[j, i].ReadOnly = false;
                    cells[j, i].Text = "";
                }
            }
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private bool checkNumber()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].Text.Length < 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void textBox_Click(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                TextBox textBox = (TextBox)sender;
                textBox.MaxLength = 1;
                textBox.BackColor = Color.White;
            }
            color_white();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.BackColor = Color.White;
           
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^1-9]"))
            {
                MessageBox.Show("Please enter only 1-9.");
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
            color_white();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool row, cloumn;

            row = check_row();
            cloumn = check_cloumn();

            if (row == true && cloumn == true)
            {
                check_textBox();
            }
        }

        private bool check_row()
        { 
            int m = 0;
            int n = 0;
            bool status = true;

            for (int i = 0; i < 9; i++)
            {
                while (n < 9)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (cells[i, j].Text == cells[m, n].Text && cells[i, j].Text != "" && n != j)
                        {
                            cells[m, n].BackColor = Color.Pink;
                            status= false;  
                        }
                    }
                    n++;
                }
                n = 0;
                m++;
            }
            return status;  
        }

        private bool check_cloumn()
        {
            int m = 0;
            int n = 0;
            bool status = true;

            for (int i = 0; i < 9; i++)
            {
                while (n < 9)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (cells[j, i].Text == cells[n, m].Text && cells[j, i].Text != "" && j != n )
                        {
                            cells[n, m].BackColor = Color.Pink;
                            status = false;
                        }
                    }
                    n++;
                }
                n = 0;
                m++;
            }
            return status;
        }
        private void color_white()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].BackColor = Color.White;
                }
            }
        }

        private void check_textBox()
        {
            int count = 0;
            
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (cells[i, j].Text != "")
                    {
                        count++;
                    }
                    if (count == 81)
                    {
                        MessageBox.Show("YOUWIN");
                    }
                }
            }
        }
    }
}
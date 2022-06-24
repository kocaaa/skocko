using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Data.SqlClient;


namespace Skocko
{
    public partial class Form1 : Form
    {
        int potez;
        int mesto;
        int vreme;
        int[] solution;
        int[] trenutni;

        bool stanje;

        string nick = "";

        SqlConnection connection = DatabaseConnector.Instance.connection;

        //SoundPlayer soundPlayer = new SoundPlayer(@"C:\Users\koca\source\repos\Skocko\Skocko\music\music.wav");
        SoundPlayer soundPlayer = new SoundPlayer(@"..\..\music\music.wav");


        public Form1()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            potez = 1;
            mesto = 0;
            vreme = 0;

            stanje = true;

            trenutni = new int[4];

            SoundPlayer soundPlayer = new SoundPlayer(@"..\..\music\music.wav");
            soundPlayer.Play();

            timer1.Start();

            ResetAll();
            PopuniNajbolje();


            

            if(nick == "")
                label3.Text = "Ime nije odabrano, \nrezultat nece biti sacuvan.";
            else
                label3.Text = "Trenutno igrate kao: " + nick;

            PictureBox[] g = GetGuesses(potez);
            PictureBox[] r = GetResults(potez);


            solution = GetNewSolution();
            
            
            
            //PrikaziResenje();
            ObrisiResenje();
        }


        public PictureBox[] GetGuesses(int potez)
        {
            PictureBox[] guess = new PictureBox[4];

            switch (potez)
            {
                case 1:
                    guess[0] = pictureBox1;
                    guess[1] = pictureBox2;
                    guess[2] = pictureBox3;
                    guess[3] = pictureBox4;
                    return guess;

                case 2:
                    guess[0] = pictureBox5;
                    guess[1] = pictureBox6;
                    guess[2] = pictureBox7;
                    guess[3] = pictureBox8;
                    return guess;
                case 3:
                    guess[0] = pictureBox9;
                    guess[1] = pictureBox10;
                    guess[2] = pictureBox11;
                    guess[3] = pictureBox12;
                    return guess;
                case 4:
                    guess[0] = pictureBox13;
                    guess[1] = pictureBox14;
                    guess[2] = pictureBox15;
                    guess[3] = pictureBox16;
                    return guess;

                case 5:
                    guess[0] = pictureBox17;
                    guess[1] = pictureBox18;
                    guess[2] = pictureBox19;
                    guess[3] = pictureBox20;
                    return guess;

                case 6:
                    guess[0] = pictureBox21;
                    guess[1] = pictureBox22;
                    guess[2] = pictureBox23;
                    guess[3] = pictureBox24;
                    return guess;
                default:
                    return new PictureBox[0];
            }
        }

        public PictureBox[] GetResults(int potez)
        {
            PictureBox[] result = new PictureBox[4];

            switch (potez)
            {
                case 1:
                    result[0] = pictureBox29;
                    result[1] = pictureBox30;
                    result[2] = pictureBox31;
                    result[3] = pictureBox32;
                    return result;
                case 2:
                    result[0] = pictureBox33;
                    result[1] = pictureBox34;
                    result[2] = pictureBox35;
                    result[3] = pictureBox36;
                    return result;
                case 3:
                    result[0] = pictureBox37;
                    result[1] = pictureBox38;
                    result[2] = pictureBox39;
                    result[3] = pictureBox40;
                    return result;
                case 4:
                    result[0] = pictureBox41;
                    result[1] = pictureBox42;
                    result[2] = pictureBox43;
                    result[3] = pictureBox44;
                    return result;
                case 5:
                    result[0] = pictureBox45;
                    result[1] = pictureBox46;
                    result[2] = pictureBox47;
                    result[3] = pictureBox48;
                    return result;
                case 6:
                    result[0] = pictureBox49;
                    result[1] = pictureBox50;
                    result[2] = pictureBox51;
                    result[3] = pictureBox52;
                    return result;
                default:
                    return new PictureBox[0];
            }
        }

        public PictureBox[] GetSolution()
        {
            PictureBox[] solution = new PictureBox[4];

            solution[0] = pictureBox25;
            solution[1] = pictureBox26;
            solution[2] = pictureBox27;
            solution[3] = pictureBox28;

            return solution;
        }

        public Image GetImage(int potez)
        {
            switch (potez)
            {
                case 1:
                    return Properties.Resources.skocko1;
                case 2:
                    return Properties.Resources.tref1;
                case 3:
                    return Properties.Resources.pik2;
                case 4:
                    return Properties.Resources.herc;
                case 5:
                    return Properties.Resources.karo1;
                case 6:
                    return Properties.Resources.zvezda1;
                default:
                    return Properties.Resources.skocko1;
            }
        }

        public int[] GetNewSolution()
        {
            Random random = new Random();
            int[] solution = new int[4];
            int number;

            for (int i = 0; i < 4; i++)
            {
                number = random.Next(1, 7);
                solution[i] = number;
            }

            return solution;
        }

        public void SrediPogodjene()
        {
            List<int> l = new List<int>();
            List<int> d = new List<int>();

            int pogodak = 0;
            int polupogodak = 0;

            for(int i = 0; i < 4; i++)
            {
                if (solution[i] == trenutni[i])
                    pogodak++;
                else
                {
                    l.Add(trenutni[i]);
                    d.Add(solution[i]);
                }
            }

            foreach(int t in l)
            {
                if (d.Contains(t))
                {
                    polupogodak++;
                    d.Remove(t);
                }
            }

            PictureBox[] results = GetResults(potez);

            for(int i = 0; i < pogodak; i++)
            {
                results[i].Image = Properties.Resources.pogodak1;
            }
            for(int i = pogodak; i < pogodak + polupogodak; i++)
            {
                results[i].Image = Properties.Resources.polupogodak1;
            }


            if(pogodak == 4)
            {
                timer1.Stop();

                PrikaziResenje();

                DodajScore(nick, vreme);

                DialogResult dialogResult = MessageBox.Show("Pogodak! Da li zelite da igrate ponovo ?", "Kraj igre", MessageBoxButtons.YesNo);

                if(dialogResult == DialogResult.Yes)
                {
                    init();
                    potez = 0;
                }
                else if(dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }



        }


        private void button1_Click(object sender, EventArgs e)
        {
            SrediGuess(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SrediGuess(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SrediGuess(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SrediGuess(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SrediGuess(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SrediGuess(6);
        }


        public void SrediGuess(int value)
        {
            if (stanje)
            {
                PictureBox[] guesses = GetGuesses(potez);
                guesses[mesto].Image = GetImage(value);

                trenutni[mesto] = value;

                mesto++;

                if (mesto == 4)
                {
                    SrediPogodjene();

                    trenutni = new int[4];
                    mesto = 0;
                    potez++;
                }

                CheckEnd();
            }
        }

        public void CheckEnd()
        {
            if (potez == 7)
            {
                timer1.Stop();
                PrikaziResenje();
                DialogResult dialogResult = MessageBox.Show("Poraz. Da li zelite da igrate ponovo ?", "Kraj igre", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    init();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            init();
        }

        public void ResetAll()
        {
            for(int i = 1; i <= 6; i++)
            {
                PictureBox[] g = GetGuesses(i);
                PictureBox[] r = GetResults(i);

                foreach (PictureBox pb in g)
                    pb.Image = null;

                foreach (PictureBox pb in r)
                    pb.Image = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            vreme++;
            label1.Text = vreme.ToString();
        }

        private void DodajScore(string nick, int vreme)
        {
            if(nick != "")
            {
                connection.Open();

                string query = "insert into rezultati(nick, vreme) values (@nick,@vreme)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@nick", nick);
                command.Parameters.AddWithValue("@vreme", vreme.ToString());

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                nick = textBox1.Text;
                label3.Text = "Trenutno igrate kao: " + nick;
                textBox1.Text = "";
                init();
            }
            else
            {
                nick = textBox1.Text;
                label3.Text = "Ime nije odabrano, \nrezultat nece biti sacuvan.";
            }
        }

        public void PrikaziResenje()
        {
            PictureBox[] s = GetSolution();

            for (int i = 0; i < 4; i++)
                s[i].Image = GetImage(solution[i]);
        }

        public void ObrisiResenje()
        {
            PictureBox[] s = GetSolution();

            for (int i = 0; i < 4; i++)
                s[i].Image = null;
        }

        public void PopuniNajbolje()
        {
            listView1.Items.Clear();

            connection.Open();

            string query = "select top 10 * from rezultati order by vreme asc";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string[] s = new string[2];
                s[0] = reader["nick"].ToString();
                s[1] = reader["vreme"].ToString();

                ListViewItem item = new ListViewItem(s);

                listView1.Items.Add(item);
            }

            connection.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PrikaziResenje();
            stanje = false;
            timer1.Stop();
        }
    }
}

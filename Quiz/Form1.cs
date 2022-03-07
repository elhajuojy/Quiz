using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        QuizEntities db = new QuizEntities();

        public void click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
           
            var query = from l in db.langs
                        join q in db.Questions
                        on l.id_lang equals q.id_lang
                        select new { lang = l.name_Lang , question =q.Question1,answer =q.answer,
                        opt1=q.opt1,
                        opt2 = q.opt2,
                        opt3 = q.opt3
                        };

            QuizForm quizfrm = new QuizForm();
            foreach(var le in query)
            {

                //quizfrm.labelQuestion.Text = le.question;
                //quizfrm.labelLanguage.Text = le.lang;
                //quizfrm.radioButton1.Text = le.opt1;
                //quizfrm.radioButton2.Text = le.opt3;
                //quizfrm.radioButton3.Text = le.opt2;
                //quizfrm.radioButton4.Text = le.answer;
                quizfrm.lang = le.lang;

            }
            this.Hide();

            quizfrm.Closed += (s, args) => this.Close();
            quizfrm.Show();



        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
            var lanlist = db.langs.Select(c => new { c.name_Lang });

            foreach(var l in lanlist)
            {
                Button btn = new Button();
                btn.Text = l.name_Lang;
                btn.ForeColor = Color.BlueViolet;
                btn.Size = new Size(150,45);
                btn.Font = new Font("Poppins", 12,FontStyle.Bold);
                btn.Name = "btn" + l.name_Lang;
                btn.Click += click;
                
                flowLayoutPanel1.Controls.Add(btn);

            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textSearch.Text = string.Empty;
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            var Query = db.langs.Where(lan => lan.name_Lang.Contains(textSearch.Text));

            foreach (var l in Query)
            {
                Button btn = new Button();
                btn.Text = l.name_Lang;
                btn.ForeColor = Color.BlueViolet;
                btn.Size = new Size(150, 45);
                btn.Font = new Font("Poppins", 12, FontStyle.Bold);
                btn.Name = "btn" + l.name_Lang;
                flowLayoutPanel1.Controls.Add(btn);


            }


        }
    }
}

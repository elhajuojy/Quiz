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
    public partial class QuizForm : Form
    {
        public string lang;
        BindingSource bs;
        QuizEntities db = new QuizEntities();
        private int counter = 15;
        public int score = 0;
        
        
        public QuizForm()
        {
            InitializeComponent();
        }

        private void QuizForm_Load(object sender, EventArgs e)
        {

            //hide answer 
            labelAnswer.Visible = false;




            var query = from l in db.langs
                        join q in db.Questions
                        on l.id_lang equals q.id_lang
                        where l.name_Lang == lang
                        select new
                        {
                            lang = l.name_Lang,
                            question = q.Question1,
                            answer = q.answer,
                            opt1 = q.opt1,
                            opt2 = q.opt2,
                            opt3 = q.opt3
                        };


            bs = new BindingSource();
            bs.DataSource = query.ToList();
            
            labelQuestion.DataBindings.Add("Text", bs,"question");
            labelLanguage.DataBindings.Add("Text", bs, "lang");
            radioButton1.DataBindings.Add("Text", bs, "opt1");
            radioButton3.DataBindings.Add("Text", bs, "opt3");
            radioButton2.DataBindings.Add("Text", bs, "opt2");
            radioButton4.DataBindings.Add("Text", bs, "answer");
            labelAnswer.DataBindings.Add("Text", bs, "answer"); 
            
            //timer
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            lblCountDown.Text = counter.ToString();

        }
      

        private void btnExit_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();



            f1.Closed += (s, args) => this.Close();
            f1.Show();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (labelAnswer.Text ==checkedButton.Text) 
            {
                score++;
            }


            bs.MoveNext();

            if (bs.Position == bs.Count - 2)
            {
                btnNext.Text = "End";
                btnExit.Visible = false;
                MessageBox.Show(score.ToString());
                btnNext.Click += btnExit_Click;

                //button1.Text = "End";
                //button1.Location = new Point(850, 501);
                //button2.Visible = false;

            }

            
            counter = 15;
            timer1_Tick(sender, e);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
                btnNext.PerformClick();
            lblCountDown.Text = counter.ToString();
        }
    }
}

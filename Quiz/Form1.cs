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
                flowLayoutPanel1.Controls.Add(btn);

            }


        }
    }
}

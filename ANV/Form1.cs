using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANV
{
    public partial class Form1 : Form
    {
        public Bitmap HandlerTexture = Properties.Resources.handler,
                     TargetTexture = Properties.Resources.target;//присвоение текстур цели и курсору

        private Point _targetPosition = new Point(300, 300);//точки отрисовки цели
        private Point _direction = Point.Empty;//точки отрисовки курсора

        private int _score = 5000;

        public bool closing = true;

        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);//необходимо для постоянного обновления формы

            UpdateStyles();
            this.WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            timer2.Interval = rand.Next(25, 100);

            _direction.X = rand.Next(-1, 2);
            _direction.Y = rand.Next(-1, 2);//задание рандомного перемещения цели
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            _targetPosition.X += _direction.X * 10;
            _targetPosition.Y += _direction.Y * 10;//ускорение по осям

            var localPosition = this.PointToClient(Cursor.Position);
            var handlerRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);

            if (_targetPosition.X < 0 || _targetPosition.X > 500)
            {
                _direction.X *= -1;
            }
            if (_targetPosition.Y < 0 || _targetPosition.Y > 500)
            {
                _direction.Y *= -1;
            }//возвращение цели, если вышла за границы формы

            Point between = new Point(localPosition.X - _targetPosition.X, localPosition.Y - _targetPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y));//высчитывание совпадения курсора и цели

            if (distance < 40)
                AddScore(1);
            if (distance < 30)
                AddScore(1);
            if (distance < 20)
                AddScore(2);
            if (distance < 10)
                AddScore(3);
            if (distance > 50)
                AddScore(1);
            if (distance > 60)
                AddScore(-2);
            //условия для начисления очков

            Finish();

            g.DrawImage(HandlerTexture, handlerRect);
            g.DrawImage(TargetTexture, targetRect);//отрисовка цели и курсора
        }

        private void AddScore(int score)
        {
            _score += score;
            label1.Text = _score.ToString();
        }//начисление очков

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = closing;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
        }

        public void Finish()
        {
            KeyGenus kg = new KeyGenus();

            if (_score >= 100000)
            {
                timer1.Stop();
                timer2.Stop();
                closing = false;
            }
            if (_score == 3000)
            {
                kg.ShowDialog();
            }
            if (closing == false)
                Application.Exit();
            if (kg.code == "1")
                closing = false;
        }//завершает игру, если score меньше или равно 100
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiAPR_5
{
    public partial class Form1 : Form
    {
        private const int SCALE_MODE = 50;
        private const float EPS = 0.001F;
        private const int COUNT_STUDY_POINTS = 4;
        private const int MAX_ITERATION_COUNT = 100;
        private readonly int[] ERMITH_POLYNOMS = new int[] { 1, 4, 4, 16 };

        private int[] weights = new int[COUNT_STUDY_POINTS];
        private Point[] studyPoints = new Point[COUNT_STUDY_POINTS];
        private int[] tempFunctionsValues = new int[COUNT_STUDY_POINTS];

        Graphics graphics;
        BufferedGraphicsContext bufferedGraphicsContext;
        BufferedGraphics bufferedGraphics;

        public Form1()
        {
            InitializeComponent();

            graphics = pictureBox1.CreateGraphics();
            bufferedGraphicsContext = new BufferedGraphicsContext();
            bufferedGraphics = bufferedGraphicsContext.Allocate(graphics,
                new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            bufferedGraphics.Graphics.Clear(Color.Black);  // Фон графика черный
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != '-')
                e.KeyChar = '\0';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tempIteration, tempPoint;
            bool isFind;
            int potentialFunction;
            PointF point = new PointF();

            studyPoints[0].X = int.Parse(textBox1.Text);
            studyPoints[0].Y = int.Parse(textBox2.Text);
            studyPoints[1].X = int.Parse(textBox3.Text);
            studyPoints[1].Y = int.Parse(textBox4.Text);
            studyPoints[2].X = int.Parse(textBox5.Text);
            studyPoints[2].Y = int.Parse(textBox6.Text);
            studyPoints[3].X = int.Parse(textBox7.Text);
            studyPoints[3].Y = int.Parse(textBox8.Text);

            weights[0] = ERMITH_POLYNOMS[0];
            weights[1] = ERMITH_POLYNOMS[1] * studyPoints[0].X;
            weights[2] = ERMITH_POLYNOMS[2] * studyPoints[0].Y;
            weights[3] = ERMITH_POLYNOMS[3] * studyPoints[0].X * studyPoints[0].Y;

            tempIteration = 0;
            do
            {
                tempPoint = 0;
                isFind = false;
                do
                {
                    tempFunctionsValues[0] = weights[0];
                    tempFunctionsValues[1] = weights[1] * studyPoints[tempPoint + 1].X;
                    tempFunctionsValues[2] = weights[2] * studyPoints[tempPoint + 1].Y;
                    tempFunctionsValues[3] = weights[3] * studyPoints[tempPoint + 1].X *
                        studyPoints[tempPoint + 1].Y;

                    potentialFunction = 0;
                    for (int i = 0; i < COUNT_STUDY_POINTS; i++)
                        potentialFunction += tempFunctionsValues[i];

                    tempFunctionsValues[0] = ERMITH_POLYNOMS[0];
                    tempFunctionsValues[1] = ERMITH_POLYNOMS[1] * studyPoints[tempPoint + 1].X;
                    tempFunctionsValues[2] = ERMITH_POLYNOMS[2] * studyPoints[tempPoint + 1].Y;
                    tempFunctionsValues[3] = ERMITH_POLYNOMS[3] * studyPoints[tempPoint + 1].X *
                        studyPoints[tempPoint + 1].Y;

                    if ((tempPoint + 1 <= 1) && (potentialFunction <= 0))
                    {
                        isFind = true;
                        for (int i = 0; i < COUNT_STUDY_POINTS; i++)
                            weights[i] += tempFunctionsValues[i];
                    }
                    else if ((tempPoint + 1 > 1) && (potentialFunction > 0))
                    {
                        isFind = true;
                        for (int i = 0; i < COUNT_STUDY_POINTS; i++)
                            weights[i] -= tempFunctionsValues[i];
                    }
                    if (tempPoint < 2)
                        tempPoint++;
                    else
                        tempPoint = -1;
                }
                while (tempPoint != 0);
                tempIteration++;
            }
            while ((tempIteration != MAX_ITERATION_COUNT) && !isFind);

            if (tempIteration == MAX_ITERATION_COUNT)
                MessageBox.Show("Данная обучающая выборка не позволяет построить разделяющую функцию.");
            else
            {
                bufferedGraphics.Graphics.Clear(Color.Black);  // Фон графика черный
                bufferedGraphics.Graphics.DrawRectangle(new Pen(Color.Red, 1), 0, 0,
                    pictureBox1.Width - 1, pictureBox1.Height - 1);
                bufferedGraphics.Graphics.DrawLine(new Pen(Color.Red, 1),
                    pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height - 1);
                bufferedGraphics.Graphics.DrawLine(new Pen(Color.Red, 1),
                    0, pictureBox1.Height / 2, pictureBox1.Width - 1, pictureBox1.Height / 2);
            }

            listBox1.Items.Add("Разделяющая функция:");
            listBox1.Items.Add(string.Format("y = -({0} + {1}*x)/({2} + {3}*x)",
                weights[0], weights[1], weights[2], weights[3]));

            point.X = -(pictureBox1.Width / (2 * SCALE_MODE));
            while (point.X < (pictureBox1.Width / (2 * SCALE_MODE)))
            {
                point.Y = -(weights[0] + weights[1] * point.X) / (weights[2] + weights[3] * point.X);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.Red),
                    (int)(point.X * SCALE_MODE + pictureBox1.Width / 2),
                    (int)(-point.Y * SCALE_MODE + pictureBox1.Height / 2), 1, 1);
                point.X += EPS;
            }

            bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White),
                    (int)(studyPoints[0].X * SCALE_MODE + pictureBox1.Width / 2 - 5),
                    (int)(-studyPoints[0].Y * SCALE_MODE + pictureBox1.Height / 2 - 5), 10, 10);
            bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White),
                    (int)(studyPoints[1].X * SCALE_MODE + pictureBox1.Width / 2 - 5),
                    (int)(-studyPoints[1].Y * SCALE_MODE + pictureBox1.Height / 2 - 5), 10, 10);
            bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.DimGray),
                    (int)(studyPoints[2].X * SCALE_MODE + pictureBox1.Width / 2 - 5),
                    (int)(-studyPoints[2].Y * SCALE_MODE + pictureBox1.Height / 2 - 5), 10, 10);
            bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.DimGray),
                    (int)(studyPoints[3].X * SCALE_MODE + pictureBox1.Width / 2 - 5),
                    (int)(-studyPoints[3].Y * SCALE_MODE + pictureBox1.Height / 2 - 5), 10, 10);

            bufferedGraphics.Render();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point testPoint = new Point();
            int[] testFunctionsValues = new int[COUNT_STUDY_POINTS];
            int testPotentialFunction;

            testPoint.X = int.Parse(textBox9.Text);
            testPoint.Y = int.Parse(textBox10.Text);

            testFunctionsValues[0] = weights[0];
            testFunctionsValues[1] = weights[1] * testPoint.X;
            testFunctionsValues[2] = weights[2] * testPoint.Y;
            testFunctionsValues[3] = weights[3] * testPoint.X * testPoint.Y;

            testPotentialFunction = 0;
            for (int i = 0; i < COUNT_STUDY_POINTS; i++)
                testPotentialFunction += testFunctionsValues[i];

            // Определение класса тестовой точки
            if (testPotentialFunction > 0)
            {
                MessageBox.Show("Тестовый объект относится к классу 1.");
                // Рисуем точку белого цвета для класса 1
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White),
                    (int)(testPoint.X * SCALE_MODE + pictureBox1.Width / 2 - 5),
                    (int)(-testPoint.Y * SCALE_MODE + pictureBox1.Height / 2 - 5), 10, 10);
            }
            else
            {
                MessageBox.Show("Тестовый объект относится к классу 2.");
                // Рисуем точку темно-серого цвета для класса 2
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.DimGray),
                    (int)(testPoint.X * SCALE_MODE + pictureBox1.Width / 2 - 5),
                    (int)(-testPoint.Y * SCALE_MODE + pictureBox1.Height / 2 - 5), 10, 10);
            }

            bufferedGraphics.Render();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

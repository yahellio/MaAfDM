using System;
using System.Drawing;
using System.Windows.Forms;

namespace MIAPR_6
{
    public partial class Form1 : Form
    {
        private double[,] distances;

        public Form1()
        {
            InitializeComponent();
            ApplyModernUI();
        }

        private void ApplyModernUI()
        {
            // Применяем шрифт и цветовую схему
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = Color.FromArgb(34, 34, 34);  // Темный фон
            this.ForeColor = Color.White; // Белый текст

            // Устанавливаем стиль кнопок с закругленными углами
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 123, 255); // Синий цвет для рамки
            button1.BackColor = Color.FromArgb(52, 58, 64); // Темный цвет кнопки
            button1.ForeColor = Color.White; // Белый текст на кнопке
            button1.Font = new Font("Segoe UI", 12, FontStyle.Bold); // Шрифт для кнопки

            // Изменяем стиль TextBox
            textBox1.BackColor = Color.FromArgb(52, 58, 64);  // Тёмный фон для текстового поля
            textBox1.ForeColor = Color.White;  // Белый текст
            textBox1.Font = new Font("Segoe UI", 12);

            // Изменяем стиль DataGridView
            dataGridView.BackgroundColor = Color.FromArgb(40, 44, 52); // Темный фон
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64); // Темный фон ячеек
            dataGridView.DefaultCellStyle.ForeColor = Color.White; // Белый текст в ячейках
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255); // Синий фон для выделенной ячейки

            // Устанавливаем стиль для легенды на графике
            foreach (var legend in chart1.Legends)
            {
                legend.BackColor = Color.FromArgb(52, 58, 64);  // Темный фон для легенды
                legend.ForeColor = Color.White; // Белый текст
                legend.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Шрифт
            }

            // Настройка графика
            chart1.BackColor = Color.Black;
            chart1.ChartAreas[0].BackColor = Color.Black;
            chart1.ChartAreas[0].AxisX.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.LineColor = Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != '-')
                e.KeyChar = '\0';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            distances = SetRandomGrid(int.Parse(textBox1.Text));

            var hierarchical = new HierarchicalGrouping(distances, int.Parse(textBox1.Text));

            hierarchical.FindGroups();

            // Настройка цвета линий на графике
            foreach (var series in chart1.Series)
            {
                series.BorderColor = Color.Cyan;
                series.BorderWidth = 2;
                series.MarkerSize = 0;
                series.IsValueShownAsLabel = true;
                series.LabelForeColor = Color.White;
            }

            // Рисуем график с анимацией
            hierarchical.Draw(chart1);
        }

        private double[,] SetRandomGrid(int size)
        {
            dataGridView.ColumnCount = size + 1;
            dataGridView.RowCount = size + 1;

            for (int i = 0; i < size; i++)
            {
                dataGridView[0, i + 1].Value = string.Format("x{0}", i + 1);
                dataGridView[i + 1, 0].Value = string.Format("x{0}", i + 1);
            }

            var result = new double[size, size];
            var rand = new Random();

            for (int i = 0; i < size; i++)
                result[i, i] = 0;
            for (int i = 1; i < size; i++)
                for (int j = 0; j < i; j++)
                {
                    result[i, j] = rand.Next(size + 5) + 1;
                    result[j, i] = result[i, j];
                }
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    dataGridView[i + 1, j + 1].Value = result[i, j];
            if (radioBtnMaximum.Checked)
            {
                for (int i = 1; i < int.Parse(textBox1.Text); i++)
                    for (int j = 0; j < i; j++)
                    {
                        result[i, j] = size + 6 - result[i, j];
                        result[j, i] = result[i, j];
                    }
            }

            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Дополнительные действия при загрузке формы, если необходимо
        }
    }
}

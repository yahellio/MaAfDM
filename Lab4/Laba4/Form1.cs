using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laba4
{
    public partial class Form1 : Form
    {
        private Perceptron perceptron;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox_Classes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
                e.KeyChar = '\0';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            perceptron = new Perceptron(int.Parse(textBox_Classes.Text),
                int.Parse(textBox_Objects.Text), int.Parse(textBox_Attributes.Text));
            perceptron.Calculate();
            perceptron.FillListBox(listBox, listBoxFunctions);

            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = int.Parse(textBox_Attributes.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var testObject = new Perceptron.PerceptronObject();

            int[] numbers = new int[dataGridView1.ColumnCount];

            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    var cellValue = dataGridView1.Rows[0].Cells[i].Value;

                    if (cellValue != null && int.TryParse(cellValue.ToString(), out int value))
                    {
                        numbers[i] = value;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка: Ячейка {i + 1} пуста или содержит некорректные данные!",
                                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Используем AddRange вместо присваивания
                testObject.attributes.AddRange(numbers);

                // Определяем класс объекта
                int resultClass = perceptron.FindClass(testObject);
                MessageBox.Show($"Объект относится к {resultClass} классу",
                                "Результат классификации", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода тестовой сборки: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
namespace Laba4
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.listBoxFunctions = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_Classes = new System.Windows.Forms.TextBox();
            this.textBox_Objects = new System.Windows.Forms.TextBox();
            this.textBox_Attributes = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(180, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 65);
            this.button1.TabIndex = 0;
            this.button1.Text = "Создать \r\nобучающую выборку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(688, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Тестовая выборка:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Кол-во классов:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Кол-во Объектов в классе:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(422, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "Кол-во признаков объекта:";
            // 
            // listBox
            // 
            this.listBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 25;
            this.listBox.Location = new System.Drawing.Point(16, 80);
            this.listBox.Name = "listBox";
            this.listBox.ScrollAlwaysVisible = true;
            this.listBox.Size = new System.Drawing.Size(311, 354);
            this.listBox.TabIndex = 10;
            // 
            // listBoxFunctions
            // 
            this.listBoxFunctions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxFunctions.FormattingEnabled = true;
            this.listBoxFunctions.ItemHeight = 25;
            this.listBoxFunctions.Location = new System.Drawing.Point(397, 80);
            this.listBoxFunctions.Name = "listBoxFunctions";
            this.listBoxFunctions.ScrollAlwaysVisible = true;
            this.listBoxFunctions.Size = new System.Drawing.Size(476, 354);
            this.listBoxFunctions.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(477, 473);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 65);
            this.button2.TabIndex = 12;
            this.button2.Text = "Подобрать класс";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_Classes
            // 
            this.textBox_Classes.Location = new System.Drawing.Point(38, 34);
            this.textBox_Classes.Name = "textBox_Classes";
            this.textBox_Classes.Size = new System.Drawing.Size(73, 29);
            this.textBox_Classes.TabIndex = 13;
            this.textBox_Classes.Text = "3";
            this.textBox_Classes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Classes_KeyPress);
            // 
            // textBox_Objects
            // 
            this.textBox_Objects.Location = new System.Drawing.Point(254, 34);
            this.textBox_Objects.Name = "textBox_Objects";
            this.textBox_Objects.Size = new System.Drawing.Size(73, 29);
            this.textBox_Objects.TabIndex = 14;
            this.textBox_Objects.Text = "2";
            this.textBox_Objects.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Classes_KeyPress);
            // 
            // textBox_Attributes
            // 
            this.textBox_Attributes.Location = new System.Drawing.Point(503, 34);
            this.textBox_Attributes.Name = "textBox_Attributes";
            this.textBox_Attributes.Size = new System.Drawing.Size(73, 29);
            this.textBox_Attributes.TabIndex = 15;
            this.textBox_Attributes.Text = "2";
            this.textBox_Attributes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Classes_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(666, 34);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(222, 39);
            this.dataGridView1.TabIndex = 16;
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Classes_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(924, 565);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox_Attributes);
            this.Controls.Add(this.textBox_Objects);
            this.Controls.Add(this.textBox_Classes);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBoxFunctions);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab4";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ListBox listBoxFunctions;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_Classes;
        private System.Windows.Forms.TextBox textBox_Objects;
        private System.Windows.Forms.TextBox textBox_Attributes;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}


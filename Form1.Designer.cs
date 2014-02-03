namespace Search_minimum_way
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
            this.B_Delete = new System.Windows.Forms.Button();
            this.B_Finish = new System.Windows.Forms.Button();
            this.B_Start = new System.Windows.Forms.Button();
            this.В_Wall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.B_Go = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // B_Delete
            // 
            this.B_Delete.BackColor = System.Drawing.Color.MediumOrchid;
            this.B_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Delete.ForeColor = System.Drawing.Color.SandyBrown;
            this.B_Delete.Image = global::Search_minimum_way.Properties.Resources.Empty_Cells;
            this.B_Delete.Location = new System.Drawing.Point(506, 236);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(77, 42);
            this.B_Delete.TabIndex = 3;
            this.B_Delete.Text = "Стереть";
            this.B_Delete.UseVisualStyleBackColor = false;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // B_Finish
            // 
            this.B_Finish.BackColor = System.Drawing.Color.MediumOrchid;
            this.B_Finish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Finish.ForeColor = System.Drawing.Color.SandyBrown;
            this.B_Finish.Image = global::Search_minimum_way.Properties.Resources.Finish_col;
            this.B_Finish.Location = new System.Drawing.Point(506, 171);
            this.B_Finish.Name = "B_Finish";
            this.B_Finish.Size = new System.Drawing.Size(77, 42);
            this.B_Finish.TabIndex = 2;
            this.B_Finish.Text = "Финиш";
            this.B_Finish.UseVisualStyleBackColor = false;
            this.B_Finish.Click += new System.EventHandler(this.B_Finish_Click);
            // 
            // B_Start
            // 
            this.B_Start.BackColor = System.Drawing.Color.RoyalBlue;
            this.B_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Start.ForeColor = System.Drawing.Color.SandyBrown;
            this.B_Start.Image = global::Search_minimum_way.Properties.Resources.Start_col;
            this.B_Start.Location = new System.Drawing.Point(506, 106);
            this.B_Start.Name = "B_Start";
            this.B_Start.Size = new System.Drawing.Size(77, 42);
            this.B_Start.TabIndex = 1;
            this.B_Start.Text = "Старт";
            this.B_Start.UseVisualStyleBackColor = false;
            this.B_Start.Click += new System.EventHandler(this.B_Start_Click);
            // 
            // В_Wall
            // 
            this.В_Wall.BackColor = System.Drawing.SystemColors.HotTrack;
            this.В_Wall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.В_Wall.ForeColor = System.Drawing.Color.SandyBrown;
            this.В_Wall.Image = global::Search_minimum_way.Properties.Resources.Wall;
            this.В_Wall.Location = new System.Drawing.Point(506, 46);
            this.В_Wall.Name = "В_Wall";
            this.В_Wall.Size = new System.Drawing.Size(77, 42);
            this.В_Wall.TabIndex = 0;
            this.В_Wall.Text = "Стена";
            this.В_Wall.UseVisualStyleBackColor = false;
            this.В_Wall.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(548, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // B_Go
            // 
            this.B_Go.Location = new System.Drawing.Point(485, 316);
            this.B_Go.Name = "B_Go";
            this.B_Go.Size = new System.Drawing.Size(116, 38);
            this.B_Go.TabIndex = 6;
            this.B_Go.Text = "Проложить путь";
            this.B_Go.UseVisualStyleBackColor = true;
            this.B_Go.Click += new System.EventHandler(this.B_Go_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(843, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 492);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.B_Go);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.B_Finish);
            this.Controls.Add(this.B_Start);
            this.Controls.Add(this.В_Wall);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button В_Wall;
        private System.Windows.Forms.Button B_Start;
        private System.Windows.Forms.Button B_Finish;
        private System.Windows.Forms.Button B_Delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_Go;
        private System.Windows.Forms.Label label3;


    }
}


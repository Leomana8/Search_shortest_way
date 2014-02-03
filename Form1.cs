using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Search_minimum_way
{
    public partial class Form1 : Form
    {

        Graphics Draw_Srfce; //создаём графическую поверхность поля
        Way Field_Cells;
        int Width_Cell;
        int Height_Cell;
        //Size s_Cell = new Size (Width_Cell, Height_Cell);
        int u = 0;
        // загрузка картинок 
        Image im_em_cel = Image.FromFile(@"Icons\Empty_Cells.PNG");
        Image im_wall = Image.FromFile(@"Icons\Wall.PNG");
        Image im_start = Image.FromFile(@"Icons\Start.PNG");
        Image im_finish = Image.FromFile(@"Icons\Finish.PNG");
        Image im_trail = Image.FromFile(@"Icons\Trail.PNG");
        /////////////////

        bool f_start = false; // установлен ли старт
        bool f_finish = false;// установлен ли финиш
        bool f_pove_way = false; // проложен ли путь
        List <List<int>> ArrWay; // массив пути

        public Form1()
        {
            InitializeComponent();
            Width_Cell = 40;
            Height_Cell = 40;
            Field_Cells = new Way(10, 10, Width_Cell, Height_Cell, 5, 5);
                    
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            u++;           
            Draw_Srfce = e.Graphics;
                Print_Field( e);
                if (f_pove_way) Print_Way(e);
        }

        private void Print_Field( PaintEventArgs e)
        {
            Image tmp = im_em_cel;
            Graphics g = e.Graphics;
            for (int i = 0; i < Field_Cells.GetLength(0); i++)
                for (int j = 0; j < Field_Cells.GetLength(1); j++)
                {
                    if (Field_Cells[i, j].Obj == Type_obj.FreeWay) tmp = im_em_cel;
                    if (Field_Cells[i, j].Obj == Type_obj.Wall) tmp = im_wall;
                    if (Field_Cells[i, j].Obj == Type_obj.Start) { tmp = im_start; label2.Text = Convert.ToString(u); }
                    if (Field_Cells[i, j].Obj == Type_obj.Finish) tmp = im_finish;
                    g.DrawImage(tmp, Field_Cells[i, j].Location);
                }
        }
        private void Print_Way( PaintEventArgs e)
        {                 
            Graphics g = e.Graphics;

            for (int i = 1; i < ArrWay.Count - 1; i++)
                {
                    g.DrawImage(Rotate_Im(i), Field_Cells[ArrWay[i][0], ArrWay[i][1]].Location);
                    System.Threading.Thread.Sleep(100);
                }
            g.Dispose();
            f_pove_way = false;
        }
        // Поворот следов в зависимости от следующей клетки
        private Image Rotate_Im(int ind)
        {
            Image tmp = (Image)im_trail.Clone(); 
            if(ArrWay[ind + 1][0] > ArrWay[ind][0])
                tmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (ArrWay[ind + 1][1] > ArrWay[ind][1])
                tmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            if (ArrWay[ind + 1][0] < ArrWay[ind][0])
                tmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return tmp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            В_Wall.Enabled = false;
            if(!f_finish) B_Finish.Enabled = true;
            if (!f_start) B_Start.Enabled = true;
            B_Delete.Enabled = true;
        }

        private void B_Start_Click(object sender, EventArgs e)
        {
            В_Wall.Enabled = true;
            if (!f_finish) B_Finish.Enabled = true;
            B_Start.Enabled = false;
            B_Delete.Enabled = true;
        }

        private void B_Finish_Click(object sender, EventArgs e)
        {
            В_Wall.Enabled = true;
            B_Finish.Enabled = false;
            if (!f_start) B_Start.Enabled = true;
            B_Delete.Enabled = true;
        }

        private void B_Delete_Click(object sender, EventArgs e)
        {
            В_Wall.Enabled = true;
            if (!f_finish) B_Finish.Enabled = true;
            if (!f_start) B_Start.Enabled = true;
            B_Delete.Enabled = false;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (e.Location.X - Width_Cell) / Width_Cell;
            int y = (e.Location.Y - Height_Cell) / Height_Cell;
            label1.Text = Convert.ToString(x) + " " + Convert.ToString(y);
            if ((x > 0 && x < Field_Cells.GetLength(0) - 1) && (y > 0 && y < Field_Cells.GetLength(1) - 1))
            {

                if (В_Wall.Enabled == false)
                {
                    if (Field_Cells[x, y].Obj == Type_obj.Start)
                    {
                        f_start = false;
                        B_Start.Enabled = true;
                    }
                    if (Field_Cells[x, y].Obj == Type_obj.Finish)
                    {
                        f_finish = false;
                        B_Finish.Enabled = true;
                    }
                    Field_Cells[x, y].Obj = Type_obj.Wall;
                }
                else if (B_Start.Enabled == false && !f_start)
                {
                    if (Field_Cells[x, y].Obj == Type_obj.Finish)
                    {
                        f_finish = false;
                        B_Finish.Enabled = true;
                    }
                    Field_Cells[x, y].Obj = Type_obj.Start;
                    f_start = true;

                }
                else if (B_Finish.Enabled == false && !f_finish)
                {
                    if (Field_Cells[x, y].Obj == Type_obj.Start)
                    {
                        f_start = false;
                        B_Start.Enabled = true;
                    }
                    Field_Cells[x, y].Obj = Type_obj.Finish;
                    f_finish = true;
                }
                else if (B_Delete.Enabled == false)
                {
                    if (Field_Cells[x, y].Obj == Type_obj.Start)
                    {
                        f_start = false;
                        B_Start.Enabled = true;
                    }
                    if (Field_Cells[x, y].Obj == Type_obj.Finish)
                    {
                        f_finish = false;
                        B_Finish.Enabled = true;
                    }
                    Field_Cells[x, y].Obj = Type_obj.FreeWay;                   
                }
                Invalidate();

            }
        }

       
        

        private void B_Go_Click(object sender, EventArgs e)
        {
            if (f_finish && f_start)
            {
                ArrWay = Field_Cells.PaveWay();
                if(ArrWay != null)
                    f_pove_way = true;
                else
                    MessageBox.Show("Нет пути от Старта до Финиша");
            }
            else MessageBox.Show("Не установлен Старт или Финиш");
            Invalidate();
        }
    }

        
    }


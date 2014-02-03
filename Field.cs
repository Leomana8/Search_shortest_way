using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Search_minimum_way
{
    enum Type_obj { Finish, Start = 253, FreeWay, Wall };
    class Cell
    {
        
        Rectangle location;
        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }
        Type_obj obj; // стена, проход, старт , финиш
        public Type_obj Obj
        {
            get { return obj; }
            set { obj = value; }
        }

       
    };
    
    class Field 
    {

        protected int Num_Cells_V; // количество клеток по вертикали
        protected int Num_Cells_H; // количество клеток по горизонатли

        protected Size s_Cell; // размер одной клетки
        protected Cell[,] field_Cells; // поле
      
        
	    public Cell this[int h, int v] 
	    {
		    get {
                if (h < 0 || h > Num_Cells_H ||
                 v < 0 || v > Num_Cells_V)
                    throw new IndexOutOfRangeException("Отсуствует элемент с заданными индексами:["+h+","+v+"]" );
                else return field_Cells[h, v];                
                }
            set
            {
                if (h < 0 || h > Num_Cells_H ||
                    v < 0 || v > Num_Cells_V)
                    throw new IndexOutOfRangeException("Отсуствует элемент с заданными индексами:[" + h + "," + v + "]");
                else field_Cells[h, v] = value;         
            }
	    }

        public Field() {}
        /// <summary>
        /// Конструктор, заполняет поле пустыми клетками и стенов вокруг поля
        /// </summary>
        /// <param name="n"> количество клеток по вертикали </param>
        /// <param name="m"> количество клеток по горизонатли</param>
        /// <param name="w"> ширина клетки</param>
        /// <param name="h"> высота клетки</param>
        /// <param name="x"> координата x точки начала поля </param>
        /// <param name="y"> координата y точки начала поля</param>
        public Field(int n, int m, int w, int h, int x, int y)
        {
            Num_Cells_H = n;
            Num_Cells_V = m;
            
            field_Cells = new Cell[Num_Cells_H, Num_Cells_V];            
            s_Cell = new Size(w, h);

            Point Cell_Point = new Point(x, y); // точка начала клетки
            for (int i = 0; i < field_Cells.GetLength(0); i++)
            {
                
                for (int j = 0; j < field_Cells.GetLength(1); j++)
                {
                    field_Cells[i,j] = new Cell();
                    Cell_Point.X = (i + 1) * s_Cell.Width;
                    Cell_Point.Y = (j + 1) * s_Cell.Height;
                    field_Cells[i, j].Location = new Rectangle(Cell_Point, s_Cell);
                    field_Cells[i, j].Obj = Type_obj.FreeWay;
                    if (j == 0 || i == 0 || j == field_Cells.GetLength(1) - 1 || i == field_Cells.GetLength(0) - 1)
                        field_Cells[i, j].Obj = Type_obj.Wall;
                }
            }
         }
        // получить размерность поля
        public int GetLength(int i)
        {
            if (i == 0) return Num_Cells_H;
            else return Num_Cells_V;
        }
  
    }

    class Way : Field
    {
        public Way(int n, int m, int w, int h, int x, int y) : base(n, m, w, h, x, y) {}
        public List<List<int>> PaveWay()
        {
            int[,] workarr = new int[Num_Cells_H, Num_Cells_V];
            int k;
            int x = 0;
            int y = 0;
            int max_k = Num_Cells_H * Num_Cells_V; // максимальное число итераций
            int i, j;
            int twice;
            // заполняем рабочий массив
            for (i = 0; i < Num_Cells_H; i++)
            {
                for (j = 0; j < Num_Cells_V; j++)
                {
                    if (field_Cells[i, j].Obj == Type_obj.Start)
                    {
                        x = i;
                        y = j;
                    }
                    workarr[i, j] = (int)field_Cells[i, j].Obj;
                }
            }

            bool f_way = false; // есть ли путь
            bool f_way_iter = true; // путь на данной итерации
            // Распространение волны 
            k = 0;
            // начинаем поиск.. сначала ищем финиш
            while (!f_way && f_way_iter && k < max_k)
            {
                f_way_iter = false; // считаем что на данной итерации нет пути
                for (i = 1; i < Num_Cells_H - 1; i++)
                {
                    for (j = 1; j < Num_Cells_V - 1; j++)
                    {
                        if (workarr[i, j] == k)
                        {
                            twice = 0;
                            f_way_iter = true; // на данной итерации есть путь
                            do
                            {
                                for (int p = -1; p < 2; p = p + 2)
                                {
                                    if (workarr[i + p * (-twice + 1), j + p * twice] == (int)Type_obj.Start) // дошли до старта => выходим из поиска
                                    {
                                        twice = 2;
                                        i = Num_Cells_H;
                                        j = Num_Cells_V;
                                        f_way = true; // путь полностью найден
                                        break;
                                    }
                                    if (workarr[i + p * (-twice + 1), j + p * twice] == (int)Type_obj.FreeWay)
                                        workarr[i + p * (-twice + 1), j + p * twice] = k + 1;
                                }
                                twice++;
                            }
                            while (twice < 2);

                        }
                    }
                }
                k++;
            }

            if (!f_way)
                return null; // Нет пути
            // заполняем массив коордианатами
            List<List<int>> ArrWay = new List<List<int>>();
            ArrWay.Add(new List<int>());
            int r = 0;
            //стартовая координата
            ArrWay[r].Add(x);
            ArrWay[r].Add(y);
            i = x;
            j = y;

            int min = workarr[i, j];
            int i_m = i;
            int j_m = j;
            while (workarr[i, j] != (int)Type_obj.Finish) // прокладываем маршрут пока не дойдем до финиша
            {
                twice = 0;
                // поиск минимального элемента из [i+1,j]; [i-1,j]; [i,j+1]; [i,j-1]
                do
                {
                    for (int p = -1; p < 2; p = p + 2)
                    {
                        if (workarr[i + p * (-twice + 1), j + p * twice] < min)
                        {
                            min = workarr[i + p * (-twice + 1), j + p * twice];
                            i_m = i + p * (-twice + 1);
                            j_m = j + p * twice;
                        }
                    }
                    twice++;
                }
                while (twice <= 1);
                i = i_m;
                j = j_m;
                r++;
                ArrWay.Add(new List<int>());// добавляем новую строку под координату
                ArrWay[r].Add(i);
                ArrWay[r].Add(j);
            }
            return ArrWay;
        }

    }
}

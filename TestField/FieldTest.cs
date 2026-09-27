using Microsoft.VisualStudio.TestTools.UnitTesting;
using Search_minimum_way;
using System.Drawing;

namespace TestField
{
    /// <summary>
    /// Модульные тесты класса Field и метода PathFinder.FindPath.
    /// Доступ к internal-типам основного проекта — через InternalsVisibleTo.
    /// </summary>
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void Constructor_SetsDimensions()
        {
            Field f = new Field(5, 4, 10, 20);
            Assert.AreEqual(5, f.GetLength(0));
            Assert.AreEqual(4, f.GetLength(1));
        }

        [TestMethod]
        public void Constructor_BorderIsWall_InteriorIsFreeWay()
        {
            Field f = new Field(5, 5, 1, 1);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    CellType expected = (i == 0 || j == 0 || i == 4 || j == 4)
                        ? CellType.Wall
                        : CellType.FreeWay;
                    Assert.AreEqual(expected, f[i, j].Type, "клетка [" + i + "," + j + "]");
                }
            }
        }

        [TestMethod]
        public void Indexer_SetGetCell()
        {
            Field f = new Field(5, 5, 1, 1);
            f[2, 2].Type = CellType.Start;
            Assert.AreEqual(CellType.Start, f[2, 2].Type);
        }

        [TestMethod]
        public void Indexer_ThrowsWhenCellOutsideField()
        {
            Field f = new Field(5, 5, 1, 1);
            // индекс за последней допустимой границей
            Assert.ThrowsExactly<IndexOutOfRangeException>(() => _ = f[5, 0]);
        }

        [TestMethod]
        public void FindPath_FindsShortestPath()
        {
            Field f = new Field(10, 10, 40, 40);
            f[1, 1].Type = CellType.Start;
            f[3, 3].Type = CellType.Finish;

            var path = PathFinder.FindPath(f);
            Assert.IsNotNull(path, "путь должен быть найден");

            // маршрут начинается на старте и заканчивается на финише
            Assert.AreEqual(new Point(1, 1), path[0]);
            Assert.AreEqual(new Point(3, 3), path[path.Count - 1]);

            // соседние клетки маршрута стоят вплотную (без прыжков по диагонали)
            for (int i = 1; i < path.Count; i++)
            {
                int step = Math.Abs(path[i].X - path[i - 1].X)
                         + Math.Abs(path[i].Y - path[i - 1].Y);
                Assert.AreEqual(1, step, "шаг " + i);
            }

            // кратчайшее расстояние от (1,1) до (3,3) — 4 шага, т.е. 5 клеток
            Assert.AreEqual(5, path.Count);
        }

        [TestMethod]
        public void FindPath_ReturnsNullWhenNoPath()
        {
            Field f = new Field(10, 10, 40, 40);
            f[1, 1].Type = CellType.Start;
            f[3, 3].Type = CellType.Finish;
            // закрываем единственный выход от старта (остальные границы — уже стены)
            f[2, 1].Type = CellType.Wall;
            f[1, 2].Type = CellType.Wall;

            Assert.IsNull(PathFinder.FindPath(f), "пути быть не должно");
        }

        [TestMethod]
        public void FindPath_StartAndFinishAtSameCell_ReturnsNull()
        {
            Field f = new Field(10, 10, 40, 40);
            f[5, 5].Type = CellType.Start;
            // финиш не установлен

            Assert.IsNull(PathFinder.FindPath(f), "пути быть не должно без финиша");
        }

        [TestMethod]
        public void FindPath_WithObstacles_FindsShortestPath()
        {
            Field f = new Field(10, 10, 40, 40);
            f[1, 1].Type = CellType.Start;
            f[8, 8].Type = CellType.Finish;

            // создаём препятствия в виде стены посередине
            for (int i = 3; i <= 7; i++)
            {
                f[i, 5].Type = CellType.Wall;
            }

            var path = PathFinder.FindPath(f);
            Assert.IsNotNull(path, "путь должен быть найден вокруг препятствия");
            Assert.AreEqual(new Point(1, 1), path[0]);
            Assert.AreEqual(new Point(8, 8), path[path.Count - 1]);

            // проверяем что все клетки пути не являются стенами
            foreach (var point in path)
            {
                Assert.AreNotEqual(CellType.Wall, f[point.X, point.Y].Type, 
                    $"путь не должен проходить через стену на [{point.X},{point.Y}]");
            }
        }

        [TestMethod]
        public void FindPath_ComplexMaze_FindsPath()
        {
            Field f = new Field(10, 10, 40, 40);
            f[1, 1].Type = CellType.Start;
            f[8, 8].Type = CellType.Finish;

            // создаём сложный лабиринт
            for (int i = 2; i < 8; i++)
            {
                f[i, 2].Type = CellType.Wall;
                f[i, 4].Type = CellType.Wall;
                f[i, 6].Type = CellType.Wall;
            }
            // оставляем проходы
            f[3, 2].Type = CellType.FreeWay;
            f[6, 4].Type = CellType.FreeWay;
            f[2, 6].Type = CellType.FreeWay;

            var path = PathFinder.FindPath(f);
            Assert.IsNotNull(path, "путь должен быть найден в лабиринте");
        }

        [TestMethod]
        public void FindPath_StraightLine_NoObstacles()
        {
            Field f = new Field(10, 10, 40, 40);
            f[1, 5].Type = CellType.Start;
            f[8, 5].Type = CellType.Finish;

            var path = PathFinder.FindPath(f);
            Assert.IsNotNull(path);
            Assert.AreEqual(8, path.Count); // 8 клеток по прямой
            Assert.AreEqual(new Point(1, 5), path[0]);
            Assert.AreEqual(new Point(8, 5), path[path.Count - 1]);
        }

        [TestMethod]
        public void FindPath_StartAdjacentToFinish()
        {
            Field f = new Field(10, 10, 40, 40);
            f[5, 5].Type = CellType.Start;
            f[5, 6].Type = CellType.Finish;

            var path = PathFinder.FindPath(f);
            Assert.IsNotNull(path);
            Assert.AreEqual(2, path.Count); // всего 2 клетки
            Assert.AreEqual(1, path.Count - 1); // 1 шаг
        }
    }
}

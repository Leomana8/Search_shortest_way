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
    }
}

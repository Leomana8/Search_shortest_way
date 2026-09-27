using Microsoft.VisualStudio.TestTools.UnitTesting;
using Search_minimum_way;
using System;

namespace TestField
{
    /// <summary>
    /// Модульные тесты класса Field и метода Way.PaveWay.
    /// Доступ к internal-типам основного проекта — через InternalsVisibleTo.
    /// </summary>
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void Constructor_SetsDimensions()
        {
            Field f = new Field(5, 4, 10, 20, 0, 0);
            Assert.AreEqual(5, f.GetLength(0));
            Assert.AreEqual(4, f.GetLength(1));
        }

        [TestMethod]
        public void Constructor_BorderIsWall_InteriorIsFreeWay()
        {
            Field f = new Field(5, 5, 1, 1, 0, 0);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Type_obj expected = (i == 0 || j == 0 || i == 4 || j == 4)
                        ? Type_obj.Wall
                        : Type_obj.FreeWay;
                    Assert.AreEqual(expected, f[i, j].Obj, "клетка [" + i + "," + j + "]");
                }
            }
        }

        [TestMethod]
        public void Indexer_SetGetCell()
        {
            Field f = new Field(5, 5, 1, 1, 0, 0);
            f[2, 2].Obj = Type_obj.Start;
            Assert.AreEqual(Type_obj.Start, f[2, 2].Obj);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Indexer_ThrowsWhenCellOutsideField()
        {
            Field f = new Field(5, 5, 1, 1, 0, 0);
            var tmp = f[5, 0]; // индекс за последней допустимой границей
        }

        [TestMethod]
        public void PaveWay_FindsShortestPath()
        {
            Way w = new Way(10, 10, 40, 40, 0, 0);
            w[1, 1].Obj = Type_obj.Start;
            w[3, 3].Obj = Type_obj.Finish;

            var path = w.PaveWay();
            Assert.IsNotNull(path, "путь должен быть найден");

            // маршрут начинается на старте и заканчивается на финише
            CollectionAssert.AreEqual(new[] { 1, 1 }, path[0]);
            CollectionAssert.AreEqual(new[] { 3, 3 }, path[path.Count - 1]);

            // соседние клетки маршрута стоят вплотную (без прыжков по диагонали)
            for (int i = 1; i < path.Count; i++)
            {
                int step = Math.Abs(path[i][0] - path[i - 1][0])
                         + Math.Abs(path[i][1] - path[i - 1][1]);
                Assert.AreEqual(1, step, "шаг " + i);
            }

            // кратчайшее расстояние от (1,1) до (3,3) — 4 шага, т.е. 5 клеток
            Assert.AreEqual(5, path.Count);
        }

        [TestMethod]
        public void PaveWay_ReturnsNullWhenNoPath()
        {
            Way w = new Way(10, 10, 40, 40, 0, 0);
            w[1, 1].Obj = Type_obj.Start;
            w[3, 3].Obj = Type_obj.Finish;
            // закрываем единственный выход от старта (остальные границы — уже стены)
            w[2, 1].Obj = Type_obj.Wall;
            w[1, 2].Obj = Type_obj.Wall;

            Assert.IsNull(w.PaveWay(), "пути быть не должно");
        }
    }
}

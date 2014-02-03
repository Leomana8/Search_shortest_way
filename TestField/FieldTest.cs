using Search_minimum_way;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestField
{
    
    
    /// <summary>
    ///Это класс теста для FieldTest, в котором должны
    ///находиться все модульные тесты FieldTest
    ///</summary>
    [TestClass()]
    public class FieldTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Тест для Конструктор Field
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Search minimum way.exe")]
        public void FieldConstructorTest()
        {
            int n = 5; // TODO: инициализация подходящего значения
            int m = 5; // TODO: инициализация подходящего значения
            int w = 1; // TODO: инициализация подходящего значения
            int h = 1; // TODO: инициализация подходящего значения
            int x = 1; // TODO: инициализация подходящего значения
            int y = 1; // TODO: инициализация подходящего значения
            Field_Accessor target = new Field_Accessor(n, m, w, h, x, y);
            Assert.Inconclusive("TODO: реализуйте код для проверки целевого объекта");
        }

        /// <summary>
        ///Тест для Конструктор Field
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Search minimum way.exe")]
        public void FieldConstructorTest1()
        {
            Field_Accessor target = new Field_Accessor();
            Assert.Inconclusive("TODO: реализуйте код для проверки целевого объекта");
        }

        /// <summary>
        ///Тест для Item
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Search minimum way.exe")]
        public void ItemTest()
        {
            Field_Accessor target = new Field_Accessor(5,5, 1,1, 1,1); // TODO: инициализация подходящего значения
            int h = 2; // TODO: инициализация подходящего значения
            int v = 2; // TODO: инициализация подходящего значения
            int expected = 1; // TODO: инициализация подходящего значения
            int actual;
            
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Проверьте правильность этого метода теста.");
        }
    }
}

namespace Search_minimum_way;

/// <summary>Тип клетки поля.</summary>
internal enum CellType
{
    FreeWay,
    Wall,
    Start,
    Finish,
}

/// <summary>Клетка поля.</summary>
internal sealed class Cell
{
    /// <summary>Область отрисовки клетки.</summary>
    public Rectangle Location { get; set; }

    /// <summary>Что находится в клетке: стена, проход, старт или финиш.</summary>
    public CellType Type { get; set; }
}

/// <summary>
/// Поле из клеток. Крайние клетки по периметру — всегда стены.
/// </summary>
internal class Field
{
    private readonly Cell[,] _cells;

    /// <summary>
    /// Создаёт поле, заполненное пустыми клетками и окружённое стеной.
    /// </summary>
    /// <param name="horizontalCells">Количество клеток по горизонтали.</param>
    /// <param name="verticalCells">Количество клеток по вертикали.</param>
    /// <param name="cellWidth">Ширина клетки в пикселях.</param>
    /// <param name="cellHeight">Высота клетки в пикселях.</param>
    public Field(int horizontalCells, int verticalCells, int cellWidth, int cellHeight)
    {
        HorizontalCells = horizontalCells;
        VerticalCells = verticalCells;
        CellSize = new Size(cellWidth, cellHeight);
        _cells = new Cell[horizontalCells, verticalCells];

        for (int i = 0; i < horizontalCells; i++)
        {
            for (int j = 0; j < verticalCells; j++)
            {
                bool isBorder = i == 0 || j == 0
                             || i == horizontalCells - 1 || j == verticalCells - 1;
                _cells[i, j] = new Cell
                {
                    // поле отрисовывается с отступом в одну клетку от края формы
                    Location = new Rectangle((i + 1) * cellWidth, (j + 1) * cellHeight, cellWidth, cellHeight),
                    Type = isBorder ? CellType.Wall : CellType.FreeWay,
                };
            }
        }
    }

    /// <summary>Количество клеток по горизонтали.</summary>
    public int HorizontalCells { get; }

    /// <summary>Количество клеток по вертикали.</summary>
    public int VerticalCells { get; }

    /// <summary>Размер одной клетки в пикселях.</summary>
    public Size CellSize { get; }

    public Cell this[int h, int v]
    {
        get
        {
            ValidateIndices(h, v);
            return _cells[h, v];
        }
        set
        {
            ValidateIndices(h, v);
            _cells[h, v] = value;
        }
    }

    /// <summary>Размерность поля: 0 — по горизонтали, 1 — по вертикали.</summary>
    public int GetLength(int dimension) => dimension == 0 ? HorizontalCells : VerticalCells;

    private void ValidateIndices(int h, int v)
    {
        if (h < 0 || h >= HorizontalCells || v < 0 || v >= VerticalCells)
            throw new IndexOutOfRangeException($"Отсутствует элемент с заданными индексами: [{h},{v}]");
    }
}

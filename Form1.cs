namespace Search_minimum_way;

public partial class Form1 : Form
{
    /// <summary>Инструмент, выбранный кнопками панели.</summary>
    private enum Tool { None, Wall, Start, Finish, Erase }

    private const int CellSizePx = 40;

    private readonly Field _field = new(10, 10, CellSizePx, CellSizePx);

    private readonly Image _imgEmpty = Properties.Resources.Empty_Cells;
    private readonly Image _imgWall = Properties.Resources.Wall;
    private readonly Image _imgStart = Properties.Resources.Start;
    private readonly Image _imgFinish = Properties.Resources.Finish;

    // след, повёрнутый по направлению движения (исходная картинка смотрит вверх)
    private readonly Image _trailUp = Properties.Resources.Trail;
    private readonly Image _trailRight;
    private readonly Image _trailDown;
    private readonly Image _trailLeft;

    private Tool _tool = Tool.None;
    private bool _startPlaced;
    private bool _finishPlaced;

    private IReadOnlyList<Point>? _path; // найденный путь
    private int _pathProgress;           // сколько клеток пути уже показано (анимация)

    public Form1()
    {
        InitializeComponent();
        _trailRight = Rotated(_trailUp, RotateFlipType.Rotate90FlipNone);
        _trailDown = Rotated(_trailUp, RotateFlipType.Rotate180FlipNone);
        _trailLeft = Rotated(_trailUp, RotateFlipType.Rotate270FlipNone);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawField(e.Graphics);
        DrawPath(e.Graphics);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        // клоны следов принадлежат форме, остальные картинки — общие ресурсы
        _trailRight.Dispose();
        _trailDown.Dispose();
        _trailLeft.Dispose();
        base.OnFormClosed(e);
    }

    private void DrawField(Graphics g)
    {
        for (int x = 0; x < _field.GetLength(0); x++)
        {
            for (int y = 0; y < _field.GetLength(1); y++)
            {
                Image image = _field[x, y].Type switch
                {
                    CellType.Wall => _imgWall,
                    CellType.Start => _imgStart,
                    CellType.Finish => _imgFinish,
                    _ => _imgEmpty,
                };
                g.DrawImage(image, _field[x, y].Location);
            }
        }
    }

    private void DrawPath(Graphics g)
    {
        if (_path is null)
            return;
        for (int i = 1; i <= _pathProgress && i < _path.Count - 1; i++)
        {
            Image trail = TrailTowards(_path[i], _path[i + 1]);
            g.DrawImage(trail, _field[_path[i].X, _path[i].Y].Location);
        }
    }

    private Image TrailTowards(Point from, Point to)
    {
        if (to.X > from.X) return _trailRight;
        if (to.X < from.X) return _trailLeft;
        if (to.Y > from.Y) return _trailDown;
        return _trailUp;
    }

    private static Image Rotated(Image source, RotateFlipType rotation)
    {
        var copy = (Image)source.Clone();
        copy.RotateFlip(rotation);
        return copy;
    }

    private void B_Wall_Click(object sender, EventArgs e) => SetTool(Tool.Wall);

    private void B_Start_Click(object sender, EventArgs e) => SetTool(Tool.Start);

    private void B_Finish_Click(object sender, EventArgs e) => SetTool(Tool.Finish);

    private void B_Delete_Click(object sender, EventArgs e) => SetTool(Tool.Erase);

    private void SetTool(Tool tool)
    {
        _tool = tool;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        B_Wall.Enabled = _tool != Tool.Wall;
        B_Start.Enabled = _tool != Tool.Start && !_startPlaced;
        B_Finish.Enabled = _tool != Tool.Finish && !_finishPlaced;
        B_Delete.Enabled = _tool != Tool.Erase;
    }

    private void Form1_MouseClick(object sender, MouseEventArgs e)
    {
        int x = (e.Location.X - CellSizePx) / CellSizePx;
        int y = (e.Location.Y - CellSizePx) / CellSizePx;
        CoordinatesLabel.Text = $"{x} {y}";

        // клики по стене-рамке и за пределами поля игнорируем
        if (x <= 0 || x >= _field.GetLength(0) - 1 || y <= 0 || y >= _field.GetLength(1) - 1)
            return;

        Cell cell = _field[x, y];
        switch (_tool)
        {
            case Tool.Wall:
                SetCellType(cell, CellType.Wall);
                break;
            case Tool.Start when !_startPlaced:
                SetCellType(cell, CellType.Start);
                break;
            case Tool.Finish when !_finishPlaced:
                SetCellType(cell, CellType.Finish);
                break;
            case Tool.Erase:
                SetCellType(cell, CellType.FreeWay);
                break;
            default:
                return; // инструмент не выбран — ничего не делаем
        }

        _path = null; // старый путь больше не актуален
        UpdateButtons();
        Invalidate();
    }

    private void SetCellType(Cell cell, CellType type)
    {
        // если клетка раньше была стартом или финишем — сбрасываем флаги
        if (cell.Type == CellType.Start) _startPlaced = false;
        if (cell.Type == CellType.Finish) _finishPlaced = false;

        cell.Type = type;

        if (type == CellType.Start) _startPlaced = true;
        if (type == CellType.Finish) _finishPlaced = true;
    }

    private async void B_Go_Click(object sender, EventArgs e)
    {
        if (!_startPlaced || !_finishPlaced)
        {
            MessageBox.Show("Не установлен Старт или Финиш");
            return;
        }

        IReadOnlyList<Point>? path = PathFinder.FindPath(_field);
        if (path is null)
        {
            MessageBox.Show("Нет пути от Старта до Финиша");
            return;
        }

        _path = path;
        B_Go.Enabled = false;
        try
        {
            // анимация прокладки пути без блокировки UI-потока
            for (_pathProgress = 1; _pathProgress < path.Count - 1; _pathProgress++)
            {
                Invalidate();
                await Task.Delay(100);
            }
        }
        finally
        {
            B_Go.Enabled = true;
        }
    }
}

namespace Search_minimum_way;

/// <summary>
/// Поиск кратчайшего пути от старта к финишу волновым алгоритмом
/// (обход в ширину, BFS) с последующим восстановлением маршрута.
/// </summary>
internal static class PathFinder
{
    private const int Unvisited = -1;
    private const int WallMark = -2;

    /// <summary>
    /// Ищет кратчайший путь от старта к финишу.
    /// </summary>
    /// <returns>
    /// Координаты клеток пути, начиная со старта и заканчивая финишем,
    /// или <c>null</c>, если путь не существует либо старт/финиш не установлены.
    /// </returns>
    public static IReadOnlyList<Point>? FindPath(Field field)
    {
        int width = field.GetLength(0);
        int height = field.GetLength(1);

        var distance = new int[width, height];
        Point start = default;
        Point finish = default;
        bool hasStart = false;
        bool hasFinish = false;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                distance[x, y] = field[x, y].Type == CellType.Wall ? WallMark : Unvisited;
                switch (field[x, y].Type)
                {
                    case CellType.Start:
                        start = new Point(x, y);
                        hasStart = true;
                        break;
                    case CellType.Finish:
                        finish = new Point(x, y);
                        hasFinish = true;
                        break;
                }
            }
        }

        if (!hasStart || !hasFinish)
            return null;

        // Распространение волны от финиша: distance[x, y] — длина пути до финиша.
        distance[finish.X, finish.Y] = 0;
        var queue = new Queue<Point>();
        queue.Enqueue(finish);
        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();
            foreach (Point neighbor in Neighbors(current, width, height))
            {
                if (distance[neighbor.X, neighbor.Y] != Unvisited)
                    continue;
                distance[neighbor.X, neighbor.Y] = distance[current.X, current.Y] + 1;
                queue.Enqueue(neighbor);
            }
        }

        if (distance[start.X, start.Y] == Unvisited)
            return null; // пути нет

        // Восстановление маршрута: от старта идём в сторону уменьшения distance.
        var path = new List<Point> { start };
        Point step = start;
        while (distance[step.X, step.Y] != 0)
        {
            foreach (Point neighbor in Neighbors(step, width, height))
            {
                if (distance[neighbor.X, neighbor.Y] == distance[step.X, step.Y] - 1)
                {
                    step = neighbor;
                    break;
                }
            }
            path.Add(step);
        }
        return path;
    }

    private static IEnumerable<Point> Neighbors(Point p, int width, int height)
    {
        if (p.X > 0) yield return new Point(p.X - 1, p.Y);
        if (p.X < width - 1) yield return new Point(p.X + 1, p.Y);
        if (p.Y > 0) yield return new Point(p.X, p.Y - 1);
        if (p.Y < height - 1) yield return new Point(p.X, p.Y + 1);
    }
}


namespace Interactive_Maze_Solver;

public class MazeSolver
{
    private int[,] maze;
    private int rows, cols;
    private Node start;
    private Node goal;
    public List<Node> Path { get; private set; }

    public MazeSolver(int[,] maze, Node start, Node goal)
    {
        this.maze = maze;
        this.start = start;
        this.goal = goal;
        rows = maze.GetLength(0);
        cols = maze.GetLength(1);
        Path = new List<Node>();
    }

    public bool SolveDFS()
    {
        Stack<Node> stack = new Stack<Node>();
        bool[,] visited = new bool[rows, cols];

        stack.Push(start);


        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            Path.Add(current);

            if (current.Row == goal.Row && current.Col == goal.Col)
                return true;

            visited[current.Row, current.Col] = true;


            foreach (var neighbor in GetNeighbors(current))
            {
                if (!visited[neighbor.Row, neighbor.Col])
                    stack.Push(neighbor);
            }
        }
        return false;
    }
    public bool SolveBFS()
    {
        Queue<Node> queue = new Queue<Node>();
        bool[,] visited = new bool[rows, cols];


        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            Path.Add(current);

            if (current.Row == goal.Row && current.Col == goal.Col)
                return true;

            visited[current.Row, current.Col] = true;


            foreach (var neighbor in GetNeighbors(current))
            {
                if (!visited[neighbor.Row, neighbor.Col])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor.Row, neighbor.Col] = true;
                }
            }
        }
        return false;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        int[] dRow = { -1, 1, 0, 0 };
        int[] dCol = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newRow = node.Row + dRow[i];
            int newCol = node.Col + dCol[i];

            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && maze[newRow, newCol] == 0)
            {
                neighbors.Add(new Node { Row = newRow, Col = newCol });
            }
        }

        return neighbors;
    }
}

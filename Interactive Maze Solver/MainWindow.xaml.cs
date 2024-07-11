using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Interactive_Maze_Solver;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int cellSize = 60;
    private int[,] maze;
    private Node start;
    private Node goal;
    private List<UIElement> pathElements = new List<UIElement>();
    private UIElement startImage;
    private UIElement animatedImage;
    private bool isCustomMazeMode = false;

    public MainWindow()
    {
        InitializeComponent();
        LoadMaze(0);
    }

    private void LoadMaze(int index)
    {
        // Define your predefined mazes here
        var mazes = new List<int[,]>
{
    new int[,]
    {
        { 0, 1, 0, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 1, 0 },
        { 0, 1, 1, 0, 0 },
        { 0, 0, 0, 1, 0 }
    },
    new int[,]
    {
        { 0, 1, 0, 1, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 0 },
        { 0, 0, 0, 1, 0 }
    },
    new int[,]
    {
        { 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 0 },
        { 0, 0, 0, 1, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 1, 0, 0, 0 }
    },
    new int[,]
    {
        { 0, 0, 0, 1, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 1, 0, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 0, 0, 0 }
    },
    new int[,]
    {
        { 0, 0, 1, 0, 0 },
        { 1, 0, 1, 0, 1 },
        { 0, 0, 0, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 0, 0, 0 }
    },
    new int[,]
    {
        { 0, 1, 1, 0, 0 },
        { 0, 1, 0, 0, 0 },
        { 0, 0, 0, 1, 0 },
        { 1, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0 }
    },
    new int[,]
    {
        { 0, 0, 0, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 1, 0 }
    }
};
        // Set the maze, start, and goal positions for each predefined maze
        maze = mazes[index];
        start = new Node { Row = 0, Col = 0 }; // Adjust as needed
        goal = new Node { Row = 4, Col = 4 }; // Adjust as needed
        DrawMaze();
    }

    private void DrawMaze()
    {
        MazeCanvas.Children.Clear();
        pathElements.Clear();
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Rectangle rect = new Rectangle
                {
                    Width = cellSize,
                    Height = cellSize,
                    Stroke = Brushes.Black,
                    Fill = maze[row, col] == 1 ? Brushes.Black : Brushes.White
                };
                Canvas.SetLeft(rect, col * cellSize);
                Canvas.SetTop(rect, row * cellSize);
                MazeCanvas.Children.Add(rect);
            }
        }
        DrawSpecialNode(start, "boy.png", true);
        DrawSpecialNode(goal, "flag.png");
    }

    private void DrawSpecialNode(Node node, string imagePath, bool isStart = false)
    {
        Image image = new Image
        {
            Width = cellSize,
            Height = cellSize,
            Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/{imagePath}"))
        };

        Canvas.SetLeft(image, node.Col * cellSize);
        Canvas.SetTop(image, node.Row * cellSize);
        MazeCanvas.Children.Add(image);

        if (isStart)
        {
            startImage = image;
        }
    }

    private void SolveButton_Click(object sender, RoutedEventArgs e)
    {
        if (animatedImage is not null)
        {
            MazeCanvas.Children.Remove(animatedImage);
            animatedImage = null;
        }

        if (startImage is not null)
        {
            MazeCanvas.Children.Remove(startImage);
            startImage = null;
        }
        PathCostTextBlock.Text = "";
        /*  TimeTakenTextBlock.Text = "";*/

        DrawSpecialNode(start, "boy.png", true);
        DrawSpecialNode(goal, "flag.png");

        MazeSolver solver = new MazeSolver(maze, start, goal);
        bool pathFound = false;
        var watch = System.Diagnostics.Stopwatch.StartNew();

        if (AlgorithmSelector.SelectedIndex == 0) // DFS
        {
            pathFound = solver.SolveDFS();
        }
        else if (AlgorithmSelector.SelectedIndex == 1) // BFS
        {
            pathFound = solver.SolveBFS();
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;

        if (pathFound)
        {
            MessageBox.Show("Path Found");
            PathCostTextBlock.Text = solver.Path.Count.ToString();
            /* TimeTakenTextBlock.Text = $"{elapsedMs} ms";*/
            AnimatePath(solver.Path);
        }
        else
        {
            PathCostTextBlock.Text = "N/A";
            /*TimeTakenTextBlock.Text = "N/A";*/
            MessageBox.Show("No Path Found");
        }
    }
    private void AnimatePath(List<Node> path)
    {
        if (startImage is not null)
            MazeCanvas.Children.Remove(startImage);

        Image boyImage = new Image
        {
            Width = cellSize,
            Height = cellSize,
            Source = new BitmapImage(new Uri("pack://application:,,,/Resources/boy.png"))
        };
        Canvas.SetLeft(boyImage, start.Col * cellSize);
        Canvas.SetTop(boyImage, start.Row * cellSize);
        MazeCanvas.Children.Add(boyImage);
        animatedImage = boyImage;

        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(200); // Adjust the interval for desired speed
        int index = 0;

        timer.Tick += (s, e) =>
        {
            if (index < path.Count)
            {
                var node = path[index];
                Canvas.SetLeft(boyImage, node.Col * cellSize);
                Canvas.SetTop(boyImage, node.Row * cellSize);
                index++;
            }
            else
            {
                timer.Stop();
            }
        };
        timer.Start();
    }

    private void CustomMazeToggle_Checked(object sender, RoutedEventArgs e)
    {
        isCustomMazeMode = true;
        MazeSelector.IsEnabled = false; // Disable predefined maze selection
        ResetMaze(); // Clear the canvas for custom maze creation
    }

    private void CustomMazeToggle_Unchecked(object sender, RoutedEventArgs e)
    {
        isCustomMazeMode = false;
        MazeSelector.IsEnabled = true; // Enable predefined maze selection
        LoadMaze(MazeSelector.SelectedIndex); // Load the selected predefined maze
    }
    private void MazeCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (isCustomMazeMode)
        {
            Point clickedPoint = e.GetPosition(MazeCanvas);
            int col = (int)(clickedPoint.X / cellSize);
            int row = (int)(clickedPoint.Y / cellSize);

            if (row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1))
            {
                maze[row, col] = maze[row, col] == 1 ? 0 : 1;
                DrawMaze();
            }
        }
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        if (isCustomMazeMode)
        {
            ResetMaze();
            return;
        }
        foreach (var element in pathElements)
        {
            MazeCanvas.Children.Remove(element);
        }
        pathElements.Clear();

        if (animatedImage is not null)
        {
            MazeCanvas.Children.Remove(animatedImage);
            animatedImage = null;
        }

        if (startImage is not null)
        {
            MazeCanvas.Children.Remove(startImage);
            startImage = null;
        }

        DrawMaze();
    }
    private void ResetMaze()
    {
        for (int row = 0; row < maze.GetLength(0); row++)
        {
            for (int col = 0; col < maze.GetLength(1); col++)
            {
                maze[row, col] = 0;
            }
        }
        DrawMaze();
    }


    private void MazeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int selectedIndex = MazeSelector.SelectedIndex;
        LoadMaze(selectedIndex);
    }
}

public class Node
{
    public int Row { get; set; }
    public int Col { get; set; }

    public int Cost { get; set; } = 1;
}


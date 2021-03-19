using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A_Star_pathfinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int gridWidth = 20, gridHeight = 30;
        public static Node[,] buttonGrid = new Node[gridWidth, gridHeight];
        public static Node start, end;
        public List<Node> path;

        public MainWindow()
        {
            InitializeComponent();
            Init();
            SetupGrid();
        }

        public void Init()
        {
            VisualGrid.Height = mainWindow.Height / gridHeight * gridWidth;
            VisualGrid.Width = mainWindow.Height;
            for (int i = 0; i < gridWidth; i++)
            {
                VisualGrid.RowDefinitions.Add(new RowDefinition() { Name = "Row" + i });
            }

            for (int j = 0; j < gridHeight; j++)
            {
                VisualGrid.ColumnDefinitions.Add(new ColumnDefinition() {Name = "Col" + j});
            }
            VisualGrid.RowDefinitions.Add(new RowDefinition() { Name = "ButtonRow" });
        }

        public void SetupGrid()
        {
            for (int j = 0; j < gridHeight; j++)
            {
                for (int i = 0; i < gridWidth; i++)
                {
                    Button button = new Button();
                    VisualGrid.Children.Add(button);
                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    buttonGrid[i, j] = new Node(i, j, button);
                    if (i == gridWidth-1 || i == 0 || j == gridHeight-1 || j == 0)
                    {
                        buttonGrid[i, j].SetTraversible(false);
                    }
                }
            }
            start = buttonGrid[8, 12];
            end = buttonGrid[1, 1];
            start.MakeStartPoint();
            end.MakeEndPoint();

            Button startButton = new Button() { Content = "Find path" };
            startButton.Click += StartButton_Click;
            Grid.SetRow(startButton, gridHeight + 1);
            Grid.SetColumn(startButton, 0);
            VisualGrid.Children.Add(startButton);

            Button resetButton = new Button() { Content = "Reset" };
            resetButton.Click += ResetButton_Click;
            Grid.SetRow(resetButton, gridHeight + 1);
            Grid.SetColumn(resetButton, 1);
            VisualGrid.Children.Add(resetButton);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            //VisualGrid.Children.Clear();
            //SetupGrid();
            foreach (Node n in buttonGrid)
            {
                if (n.IsTraversible())
                {
                    Button button = new Button();
                    VisualGrid.Children.Add(button);
                    Grid.SetColumn(button, n.Col);
                    Grid.SetRow(button, n.Row);
                    buttonGrid[n.Row, n.Col] = new Node(n.Row, n.Col, button);
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            path = Pathfinding.FindPath(start, end);
            foreach (Node n in path)
            {
                n.button.Background = Brushes.BlueViolet;
            }
        }
    }
}

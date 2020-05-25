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
        public static int gridWidth = 10, gridHeight = 15;
        public static Node[,] buttonGrid = new Node[gridWidth, gridHeight];
        public static Node start, end;

        public MainWindow()
        {
            InitializeComponent();
            VisualGrid.Height = mainWindow.Height / gridHeight * gridWidth;
            VisualGrid.Width = mainWindow.Height / gridHeight * gridHeight;
            for (int i = 0; i < gridWidth; i++)
            {
                VisualGrid.RowDefinitions.Add(new RowDefinition() { Name = "Row" + i });
            }
            for (int j = 0; j < gridHeight; j++)
            {
                VisualGrid.ColumnDefinitions.Add(new ColumnDefinition() { Name = "Col" + j });
                for (int i = 0; i < gridWidth; i++)
                {
                    Button button = new Button();
                    VisualGrid.Children.Add(button);
                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    buttonGrid[i, j] = new Node(i, j, button);
                    if (i == 5 && j == 8)
                    {
                        int targetRow = 0;
                        int targetCol = 0;
                        Trace.WriteLine("Distance from " + i + ", " + j + " to " + targetRow + ", " + targetCol + " is " + buttonGrid[i, j].DistanceToNode(buttonGrid[targetRow, targetCol]) + ".");
                    }
                }
            }
            start = buttonGrid[8, 12];
            end = buttonGrid[1, 1];
            start.MakeStartPoint();
            end.MakeEndPoint();
        }
    }
}

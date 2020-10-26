using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace A_Star_pathfinding
{
    public class Node
    {
        public int GCost; //Distance from starting node
        public int HCost; //Distance from End node
        public Node parent;
        public readonly Button button;
        private const int DistanceDiagonal = 14;
        private const int DistanceStraight = 10;
        public int Row;
        public int Col;
        private int FCost;
        private bool traversible = true;

        public Node(int row, int col, Button button)
        {
            this.Row = row;
            this.Col = col;
            this.button = button;
            button.PreviewMouseDown += Button_MouseDown;    //Has to be preview, because the Click event eats up the left mouse click
            button.Background = default;
        }

        public void MakeStartPoint()
        {
            button.Background = Brushes.Cyan;
        }

        public void MakeEndPoint()
        {
            button.Background = Brushes.Cyan;
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (traversible)
                {
                    button.Background = Brushes.Black;
                } else
                {
                    button.Background = default;
                }
                traversible = !traversible;
            }
        }

        public int DistanceToNode(Node node)
        {
            if (this == node)
            {
                //button.Background = Brushes.DarkBlue;
                return 0;
            }
            int rowDir = node.Row - Row;
            int colDir = node.Col - Col;
            int realDistance;
            if (node.Row == Row || node.Col == Col)
            {
                realDistance = DistanceStraight;
            }
            else
            {
                realDistance = DistanceDiagonal;
            }
            return MainWindow.buttonGrid[Row + Math.Sign(rowDir), Col + Math.Sign(colDir)].DistanceToNode(node) + realDistance;
        }

        public Stack<Node> GetNeighbours()
        {
            Stack<Node> result = new Stack<Node>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (
                        (Row + i > 0) 
                        && (Col + j > 0) 
                        && (Row + i < MainWindow.gridWidth) 
                        && (Col + j < MainWindow.gridHeight) 
                        && (i != 0 || j != 0)
                        ) result.Push(MainWindow.buttonGrid[(Row + i), (Col + j)]);
                }
            }
            return result;
        }

        public bool IsTraversible()
        {
            return traversible;
        }

        public int GetFCost()
        {
            this.FCost = this.HCost + this.GCost;
            return this.FCost;
        }
    }
}

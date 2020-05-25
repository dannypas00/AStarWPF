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
        private static int distanceDiagonal = 14;
        private static int distanceStraight = 10;
        private readonly int row;
        private readonly int col;
        private readonly Button button;
        private int GCost; //Distance from starting node
        private int HCost; //Distance from End node
        private int FCost; //GCost + HCost
        private bool closed = false;
        private bool traversible = true;

        public Node(int row, int col, Button button)
        {
            this.row = row;
            this.col = col;
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
            if (!closed && e.ChangedButton == MouseButton.Left)
            {
                button.Background = Brushes.Red;
                closed = true;
            } else if (e.ChangedButton == MouseButton.Right)
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
            int rowDir = node.GetRow() - row;
            int colDir = node.GetCol() - col;
            return MainWindow.buttonGrid[row + Math.Sign(rowDir), col + Math.Sign(colDir)].DistanceToNode(node) + ((rowDir != 0 && rowDir != 0) ? distanceDiagonal : distanceStraight);
        }

        public int GetCol()
        {
            return col;
        }

        public int GetRow()
        {
            return row;
        }

        public bool IsTraversible()
        {
            return traversible;
        }
    }
}

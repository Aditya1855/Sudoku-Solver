
//using System;

//namespace SudokuSolverRazor.Models
//{
//    public class MiniSolver : ISudokuSolver
//    {
//        private int size;
//        private int boxRows, boxCols;
//        private string[,] board;

//        public MiniSolver(int size)
//        {
//            this.size = size;
//            board = new string[size, size];

//            if (size == 4)
//            {
//                boxRows = boxCols = 2;
//            }
//            else if (size == 6)
//            {
//                boxRows = 2; boxCols = 3;
//            }
//            else
//            {
//                throw new ArgumentException("Only 4x4 and 6x6 Mini Sudoku are supported.");
//            }
//        }

//        public bool Solve(string[,] input)
//        {
//            Array.Copy(input, board, input.Length);
//            return SolveRecursive(0, 0);
//        }

//        public string[,] GetBoard() => board;

//        private bool SolveRecursive(int row, int col)
//        {
//            if (row == size) return true;
//            if (col == size) return SolveRecursive(row + 1, 0);
//            if (!string.IsNullOrEmpty(board[row, col])) return SolveRecursive(row, col + 1);

//            for (int num = 1; num <= size; num++)
//            {
//                var val = num.ToString();
//                if (IsValid(row, col, val))
//                {
//                    board[row, col] = val;
//                    if (SolveRecursive(row, col + 1)) return true;
//                    board[row, col] = "";
//                }
//            }
//            return false;
//        }

//        private bool IsValid(int row, int col, string val)
//        {
//            for (int i = 0; i < size; i++)
//            {
//                if (board[row, i] == val || board[i, col] == val)
//                    return false;
//            }

//            int startRow = row / boxRows * boxRows;
//            int startCol = col / boxCols * boxCols;
//            for (int i = 0; i < boxRows; i++)
//            {
//                for (int j = 0; j < boxCols; j++)
//                {
//                    if (board[startRow + i, startCol + j] == val)
//                        return false;
//                }
//            }
//            return true;
//        }
//    }
//}

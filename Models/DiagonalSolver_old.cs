
//using System;

//namespace SudokuSolverRazor.Models
//{
//    public class DiagonalSolver : ISudokuSolver
//    {
//        private string[,] board = new string[9, 9];

//        public bool Solve(string[,] input)
//        {
//            Array.Copy(input, board, input.Length);
//            return SolveRecursive(0, 0);
//        }

//        public string[,] GetBoard() => board;

//        private bool SolveRecursive(int row, int col)
//        {
//            if (row == 9) return true;
//            if (col == 9) return SolveRecursive(row + 1, 0);
//            if (!string.IsNullOrEmpty(board[row, col])) return SolveRecursive(row, col + 1);

//            for (int num = 1; num <= 9; num++)
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
//            for (int i = 0; i < 9; i++)
//            {
//                if (board[row, i] == val || board[i, col] == val)
//                    return false;

//                int boxRow = 3 * (row / 3) + i / 3;
//                int boxCol = 3 * (col / 3) + i % 3;
//                if (board[boxRow, boxCol] == val)
//                    return false;
//            }

//            if (row == col)
//            {
//                for (int i = 0; i < 9; i++)
//                    if (board[i, i] == val) return false;
//            }

//            if (row + col == 8)
//            {
//                for (int i = 0; i < 9; i++)
//                    if (board[i, 8 - i] == val) return false;
//            }

//            return true;
//        }
//    }
//}








//namespace SudokuSolverRazor.Models
//{
//    public class DiagonalSolver : ClassicSolver
//    {
//        // Optional: Override validation logic here in the future if needed
//    }
//}

namespace SudokuSolverRazor.Models
{
    public interface ISudokuSolver
    {
        bool Solve(string[,] input);
        int[,] GetBoard();
    }

    public class ClassicSolver : ISudokuSolver
    {
        protected int[,] board = new int[9, 9];
        protected virtual bool ValidateInitialBoard()
        {
            // Row and Column validation
            for (int i = 0; i < 9; i++)
            {
                var rowSet = new HashSet<int>();
                var colSet = new HashSet<int>();

                for (int j = 0; j < 9; j++)
                {
                    int r = board[i, j];
                    if (r != 0)
                    {
                        if (rowSet.Contains(r)) return false;
                        rowSet.Add(r);
                    }

                    int c = board[j, i];
                    if (c != 0)
                    {
                        if (colSet.Contains(c)) return false;
                        colSet.Add(c);
                    }
                }
            }

            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxCol = 0; boxCol < 3; boxCol++)
                {
                    var boxSet = new HashSet<int>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            int val = board[3 * boxRow + i, 3 * boxCol + j];
                            if (val != 0)
                            {
                                if (boxSet.Contains(val)) return false;
                                boxSet.Add(val);
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool Solve(string[,] input)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    board[i, j] = int.TryParse(input[i, j], out int value) ? value : 0;

            if (!ValidateInitialBoard())
                return false;

            return SolveInternal();
        }


        public int[,] GetBoard() => board;

        private bool SolveInternal()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsValid(row, col, num))
                            {
                                board[row, col] = num;
                                if (SolveInternal()) return true;
                                board[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        protected virtual bool IsValid(int row, int col, int num)
        {
            for (int i = 0; i < 9; i++)
                if (board[row, i] == num || board[i, col] == num)
                    return false;

            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[startRow + i, startCol + j] == num)
                        return false;

            return true;
        }
    }

    public class MiniSolver : ISudokuSolver
    {
        private int[,] board;
        private int size;
        private int blockSize;

        public MiniSolver(int size)
        {
            this.size = size;
            board = new int[size, size];
            blockSize = size == 4 ? 2 : (size == 6 ? 2 : 3);
        }

        public bool Solve(string[,] input)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = int.TryParse(input[i, j], out int value) ? value : 0;

            return SolveInternal();
        }

        public int[,] GetBoard() => board;

        private bool SolveInternal()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board[row, col] == 0)
                    {
                        for (int num = 1; num <= size; num++)
                        {
                            if (IsValid(row, col, num))
                            {
                                board[row, col] = num;
                                if (SolveInternal()) return true;
                                board[row, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsValid(int row, int col, int num)
        {
            for (int i = 0; i < size; i++)
                if (board[row, i] == num || board[i, col] == num)
                    return false;

            int startRow = row / blockSize * blockSize;
            int startCol = col / blockSize * blockSize;
            for (int i = 0; i < blockSize; i++)
                for (int j = 0; j < blockSize; j++)
                    if (board[startRow + i, startCol + j] == num)
                        return false;

            return true;
        }
    }

    public class DiagonalSolver : ClassicSolver
    {

        protected override bool ValidateInitialBoard()
        {
            if (!base.ValidateInitialBoard())
                return false;

            var mainDiag = new HashSet<int>();
            var antiDiag = new HashSet<int>();

            for (int i = 0; i < 9; i++)
            {
                int a = board[i, i];
                if (a != 0)
                {
                    if (mainDiag.Contains(a)) return false;
                    mainDiag.Add(a);
                }

                int b = board[i, 8 - i];
                if (b != 0)
                {
                    if (antiDiag.Contains(b)) return false;
                    antiDiag.Add(b);
                }
            }

            return true;
        }

        protected override bool IsValid(int row, int col, int num)
        {
            if (!base.IsValid(row, col, num))
                return false;

            if (row == col)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i != row && GetBoard()[i, i] == num)
                        return false;
                }
            }

            if (row + col == 8)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i != row && GetBoard()[i, 8 - i] == num)
                        return false;
                }
            }

            return true;
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SudokuSolverRazor.Models;
using System.Text.RegularExpressions;

public class IndexModel : PageModel
{
    [BindProperty]
    public string SelectedType { get; set; } = "Classic";

    [BindProperty]
    public List<List<string>> Puzzle { get; set; } = new();

    public List<List<string>> InputPuzzle
    {
        get
        {
            int size = GetSize();
            if (Puzzle == null || Puzzle.Count != size || Puzzle.Any(row => row.Count != size))
            {
                Puzzle = CreateEmptyPuzzle();
            }
            return Puzzle;
        }
    }

    public int[,]? SolvedPuzzle { get; set; }

    public bool ShowUnsolvableMessage { get; set; } = false;

    public void OnGet()
    {
        Puzzle = CreateEmptyPuzzle();
    }
    private bool IsInputValid(string[,] board, int size)
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (!string.IsNullOrWhiteSpace(board[i, j]) &&
                    !Regex.IsMatch(board[i, j], $"^[1-{size}]$"))
                    return false;
        return true;
    }

    public IActionResult OnPostSolve()
    {
        int size = GetSize();
        Puzzle = Puzzle?.Count == size ? Puzzle : CreateEmptyPuzzle();

        string[,] board = ParseBoardFromForm(size);

        if (!IsInputValid(board, size))
        {
            ShowUnsolvableMessage = true;
            ViewData["Error"] = $"Invalid input! Please enter only digits from 1 to {size} in the puzzle.";
            return Page();
        }

        ISudokuSolver solver = SelectedType switch
        {
            "Mini4x4" => new MiniSolver(4),
            "Mini6x6" => new MiniSolver(6),
            "Diagonal" => new DiagonalSolver(),
            _ => new ClassicSolver()
        };

        if (solver.Solve(board))
            SolvedPuzzle = solver.GetBoard();
        else
            ShowUnsolvableMessage = true;

        return Page();
    }

    public IActionResult OnPostClear()
    {
        Puzzle = CreateEmptyPuzzle();
        SolvedPuzzle = null;
        ShowUnsolvableMessage = false;
        return Page();
    }

    private List<List<string>> CreateEmptyPuzzle()
    {
        int size = GetSize();
        var puzzle = new List<List<string>>();
        for (int i = 0; i < size; i++)
        {
            var row = new List<string>();
            for (int j = 0; j < size; j++)
                row.Add("");
            puzzle.Add(row);
        }
        return puzzle;
    }

    private int GetSize()
    {
        return SelectedType switch
        {
            "Mini4x4" => 4,
            "Mini6x6" => 6,
            _ => 9
        };
    }

    private string[,] ParseBoardFromForm(int size)
    {
        string[,] board = new string[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                string key = $"Puzzle[{i}][{j}]";
                if (Request.Form.ContainsKey(key))
                    board[i, j] = Request.Form[key];
                else
                    board[i, j] = "";
            }
        }

        return board;
    }
}

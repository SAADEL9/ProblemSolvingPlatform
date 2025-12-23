using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class Probleme
{
    public int ProbId { get; set; }

    public string Title { get; set; } = null!;

    public string Descr { get; set; } = null!;

    public string? Difficulte { get; set; }

    // New fields for function templates and test cases
    public string? FunctionTemplate { get; set; }

    public string? TestCases { get; set; }

    public string? Language { get; set; } = "python";

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}

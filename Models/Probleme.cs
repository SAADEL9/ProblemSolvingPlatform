using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class Probleme
{
    public int ProbId { get; set; }

    public string Title { get; set; } = null!;

    public string Descr { get; set; } = null!;

    public string? Difficulte { get; set; }
}

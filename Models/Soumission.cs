using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class Soumission
{
    public int SoumissionId { get; set; }

    public int UserId { get; set; }

    public string Code { get; set; } = null!;

    public string Probleme { get; set; } = null!;

    public string Langage { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

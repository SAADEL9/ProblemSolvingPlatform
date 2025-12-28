using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class Soumission
{
    public int SoumissionId { get; set; }

    public int UserId { get; set; }

    public int ProbId { get; set; }

    public string Code { get; set; } = null!;

    public string Probleme { get; set; } = null!;

    public string Langage { get; set; } = null!;

    // New fields for submission tracking
    public bool? IsPassed { get; set; }

    public int? TestsPassed { get; set; }

    public int? TestsTotal { get; set; }

    public int? PointsEarned { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual User User { get; set; } = null!;
}

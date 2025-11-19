using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class Commentaire
{
    public int CommentaireId { get; set; }

    public int UserId { get; set; }

    public string Probleme { get; set; } = null!;

    public string Contenu { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public virtual User User { get; set; } = null!;
}

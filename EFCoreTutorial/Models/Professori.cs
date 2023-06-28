using System;
using System.Collections.Generic;

namespace EFCoreTutorial.Models;

public partial class Professori
{
    public long Id { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public virtual ICollection<Corsi> Corsos { get; set; } = new List<Corsi>();
}

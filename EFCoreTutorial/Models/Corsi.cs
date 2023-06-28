using System;
using System.Collections.Generic;

namespace EFCoreTutorial.Models;

public partial class Corsi
{
    public long Id { get; set; }

    public string? Nome { get; set; }

    public string? Descrizione { get; set; }

    public virtual ICollection<Professori> Professores { get; set; } = new List<Professori>();

    public virtual ICollection<Studenti> Studentes { get; set; } = new List<Studenti>();
}

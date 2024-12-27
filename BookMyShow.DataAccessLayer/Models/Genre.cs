﻿using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}

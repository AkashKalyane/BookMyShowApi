using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieName { get; set; } = null!;

    public int GenreId { get; set; }

    public string Grade { get; set; } = null!;

    public bool IsAdult { get; set; }

    public string ReleaseYear { get; set; } = null!;

    public int DirectorId { get; set; }

    public int MainActorMale { get; set; }

    public int MainActorFemale { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? ChangedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual Director Director { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Actor MainActorFemaleNavigation { get; set; } = null!;

    public virtual Actor MainActorMaleNavigation { get; set; } = null!;
}

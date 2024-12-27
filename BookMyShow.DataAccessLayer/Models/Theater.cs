using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class Theater
{
    public int TheaterId { get; set; }

    public string TheaterName { get; set; } = null!;

    public bool IsMultiScreen { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual ICollection<TheaterScreen> TheaterScreens { get; set; } = new List<TheaterScreen>();
}

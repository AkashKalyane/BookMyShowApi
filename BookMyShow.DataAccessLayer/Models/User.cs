using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class User
{
    public int UserId { get; set; }
   
    public string? UserName { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? CreateOn { get; set; }

    public string? Salt { get; set; }

    public virtual ICollection<Actor> ActorChangedByNavigations { get; set; } = new List<Actor>();

    public virtual ICollection<Actor> ActorCreatedByNavigations { get; set; } = new List<Actor>();

    public virtual ICollection<Actor> ActorDeletedByNavigations { get; set; } = new List<Actor>();

    public virtual ICollection<Booking> BookingChangedByNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingCreatedByNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingDeletedByNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Director> DirectorChangedByNavigations { get; set; } = new List<Director>();

    public virtual ICollection<Director> DirectorCreatedByNavigations { get; set; } = new List<Director>();

    public virtual ICollection<Director> DirectorDeletedByNavigations { get; set; } = new List<Director>();

    public virtual ICollection<Genre> GenreChangedByNavigations { get; set; } = new List<Genre>();

    public virtual ICollection<Genre> GenreCreatedByNavigations { get; set; } = new List<Genre>();

    public virtual ICollection<Genre> GenreDeletedByNavigations { get; set; } = new List<Genre>();

    public virtual ICollection<Movie> MovieChangedByNavigations { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MovieCreatedByNavigations { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MovieDeletedByNavigations { get; set; } = new List<Movie>();

    public virtual ICollection<Slot> SlotChangedByNavigations { get; set; } = new List<Slot>();

    public virtual ICollection<Slot> SlotCreatedByNavigations { get; set; } = new List<Slot>();

    public virtual ICollection<Slot> SlotDeletedByNavigations { get; set; } = new List<Slot>();

    public virtual ICollection<Theater> TheaterChangedByNavigations { get; set; } = new List<Theater>();

    public virtual ICollection<Theater> TheaterCreatedByNavigations { get; set; } = new List<Theater>();

    public virtual ICollection<Theater> TheaterDeletedByNavigations { get; set; } = new List<Theater>();

    public virtual ICollection<TheaterScreen> TheaterScreenChangedByNavigations { get; set; } = new List<TheaterScreen>();

    public virtual ICollection<TheaterScreen> TheaterScreenCreatedByNavigations { get; set; } = new List<TheaterScreen>();

    public virtual ICollection<TheaterScreen> TheaterScreenDeletedByNavigations { get; set; } = new List<TheaterScreen>();

    public virtual ICollection<UserContact> UserContactChangedByNavigations { get; set; } = new List<UserContact>();

    public virtual ICollection<UserContact> UserContactCreatedByNavigations { get; set; } = new List<UserContact>();

    public virtual ICollection<UserContact> UserContactDeletedByNavigations { get; set; } = new List<UserContact>();

    public virtual ICollection<UserContact> UserContactUsers { get; set; } = new List<UserContact>();

    public virtual ICollection<UserDetail> UserDetailChangedByNavigations { get; set; } = new List<UserDetail>();

    public virtual ICollection<UserDetail> UserDetailCreatedByNavigations { get; set; } = new List<UserDetail>();

    public virtual ICollection<UserDetail> UserDetailDeletedByNavigations { get; set; } = new List<UserDetail>();

    public virtual ICollection<UserDetail> UserDetailUsers { get; set; } = new List<UserDetail>();
}

using Microsoft.EntityFrameworkCore;

namespace EarthlyRemedies.Models
{
  public class EarthlyRemediesContext : DbContext
  {
    public EarthlyRemediesContext(DbContextOptions<EarthlyRemediesContext> options)
        : base(options)
    {
    }

    public DbSet<Remedy> Remedies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Remedy>()
      .HasData(
      new Remedy { RemedyId = 10, Name = "Mucus Melter", Details = "L-Acetyl Cysteine, taken daily, will shorten duration of respiratory ailments.", Ailment = "chest congestion", Category = "respiratory", Ingredients = "L-Acetyl Cysteine", UserId = 1 },
      new Remedy { RemedyId = 11, Name = "Dandruff Buster", Details = "Castor oil rubbed into the scalp, every night before bed, for one week.", Ailment = "dandruff", Category = "skin", Ingredients = "castor oil", UserId = 2 },
      new Remedy { RemedyId = 2, Name = "Sore Muscle Balm", Details = "Mix 2 drops eucalyptus oil, 1 drop peppermint oil, and 1 drop tea tree oil into 1 oz neutral oil or balm.  Rub on sore muscles before sleep.", Ailment = "muscle soreness", Category = "musculoskeletal", Ingredients = "tea tree oil, peppermint, eucalyptus", UserId = 2 },
      new Remedy { RemedyId = 3, Name = "Dry Eye Relief", Details = "Mega doses of Omega 3s for LIFE!!!!", Ailment = "dry eyes", Category = "eyes", Ingredients = "Omega 3", UserId = 1 },
      new Remedy { RemedyId = 4, Name = "Fungus Foiler", Details = "Soak the affected area in apple cider vinegar twice a day until cured", Ailment = "foot fungus", Category = "skin", Ingredients = "apple cider vinegar", UserId = 2 },
      new Remedy { RemedyId = 5, Name = "Polish Tummy Cure", Details = "One shot of vodka, followed by one shot of fresh ginger juice, daily until symptoms resolve.", Ailment = "digestive illness", Category = "GI", Ingredients = "ginger, vodka", UserId = 1 },
      new Remedy { RemedyId = 6, Name = "Sunburn salve", Details = "apply earl grey tea to the affected area 5 times a day until burn goes away", Ailment = "sunburn", Category = "skin", Ingredients = "tea", UserId = 2 },
      new Remedy { RemedyId = 7, Name = "Heartburn Helper", Details = "Heat 4 oz water to body temperature.  Add 1 oz fresh lemon juice. Drink slowly.", Ailment = "heartburn", Category = "GI", Ingredients = "lemon", UserId = 1 },
      new Remedy { RemedyId = 8, Name = "Nighttime Salve", Details = "Mix 2 drops eucalyptus oil, 1 drop peppermint oil, and 1 drop tea tree oil into 1 oz neutral oil or balm.  Rub on throat, chest and upper back before sleep.", Ailment = "cough", Category = "respiratory", Ingredients = "tea tree oil, peppermint, eucalyptus", UserId = 2 },
      new Remedy { RemedyId = 9, Name = "Eye obsruction removal", Details = "If something is in your eye, pull your upper eyelid over your lower one, in an attempt to scrape out the obstruction", Ailment = "eye obstruction", Category = "eyes", Ingredients = "", UserId = 1 }
      );

      builder.Entity<Rating>()
      .HasData(
        new Rating { RatingId = 1, Stars = 5, RemedyId = 2, UserId = 2, Comments = "Almost killed me!" },
        new Rating { RatingId = 2, Stars = 3, RemedyId = 2, UserId = 1, Comments = "Not sure it did anything." },
        new Rating { RatingId = 3, Stars = 1, RemedyId = 2, UserId = 3, Comments = "Best ever!!!" }
      );

      builder.Entity<User>()
      .HasData(
        new User { Id = 1, FirstName = "Julia", LastName = "Test", Username = "JuliaTest", Password = "test" },
        new User { Id = 2, FirstName = "Jason", LastName = "Test", Username = "JasonTest", Password = "test" },
        new User { Id = 3, FirstName = "DJ", LastName = "Test", Username = "DJTest", Password = "test" }
      );

    }


  }
}
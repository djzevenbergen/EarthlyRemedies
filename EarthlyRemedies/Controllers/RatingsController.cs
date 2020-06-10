
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EarthlyRemedies.Models;
using Microsoft.EntityFrameworkCore;


namespace EarthlyRemedies.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RatingsController : ControllerBase
  {
    private EarthlyRemediesContext _db;

    public RatingsController(EarthlyRemediesContext db)
    {
      _db = db;
    }

    // POST api/Remedies
    [HttpPost]
    public void Post([FromBody] Rating rating)
    {
      _db.Ratings.Add(rating);
      _db.SaveChanges();
    }

    // GET api/Remedies
    [HttpGet]
    public ActionResult<IEnumerable<Rating>> Get(int remedyId, int ratingId, int userId)
    {
      var query = _db.Ratings.AsQueryable();

      if (remedyId != 0)
      {
        query = query.Where(entry => entry.RemedyId == remedyId);
      }

      if (ratingId != 0)
      {
        query = query.Where(entry => entry.RatingId == ratingId);
      }

      if (userId != 0)
      {
        query = query.Where(entry => entry.UserId == userId);
      }
      return query.ToList();
    }

  }
}
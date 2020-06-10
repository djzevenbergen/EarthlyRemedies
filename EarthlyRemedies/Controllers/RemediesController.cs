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
  public class RemediesController : ControllerBase
  {
    private EarthlyRemediesContext _db;

    public RemediesController(EarthlyRemediesContext db)
    {
      _db = db;
    }

    // GET api/Remedies
    // [HttpGet]
    // public ActionResult<IEnumerable<Remedy>> Get()
    // {
    //   return _db.Remedies.ToList();
    // }

    // POST api/Remedies
    [HttpPost]
    public void Post([FromBody] Remedy remedy)
    {
      _db.Remedies.Add(remedy);
      _db.SaveChanges();
    }

    // GET api/remedies/#
    [HttpGet("{id}")]
    public ActionResult<Remedy> Get(int id)
    {
      return _db.Remedies.FirstOrDefault(entry => entry.RemedyId == id);
    }

    //PUT api/remedies/userId/remedyId
    [HttpPut("{userId}/{id}")]
    public void Put(int userId, int id, [FromBody] Remedy remedy)
    {
      remedy.RemedyId = id;
      if (remedy.UserId == userId)
      {
        _db.Entry(remedy).State = EntityState.Modified;
        _db.SaveChanges();
      }
    }

    //http://localhost:5000/api/remedies/1/9
    [HttpDelete("{userId}/{id}")]
    public void Delete(int id, int userId)
    {
      var remedyToDelete = _db.Remedies.FirstOrDefault(entry => entry.RemedyId == id);
      if (remedyToDelete.UserId == userId)
      {
        _db.Remedies.Remove(remedyToDelete);
        _db.SaveChanges();
      }
    }

    // GET api/Remedies
    [HttpGet]
    // public ActionResult<IEnumerable<Remedy>> Get(string name, string details, string ailment, string category, string ingredients, int userId)
    public ActionResult<Dictionary<string, object>> Get(string name, string details, string ailment, string category, string ingredients, int userId)
    {
      var query = _db.Remedies.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      if (details != null)
      {
        query = query.Where(entry => entry.Details.Contains(details));
      }

      if (ingredients != null)
      {
        query = query.Where(entry => entry.Ingredients.Contains(ingredients));
      }

      if (ailment != null)
      {
        query = query.Where(entry => entry.Ailment == ailment);
      }

      if (category != null)
      {
        query = query.Where(entry => entry.Category == category);
      }

      if (userId != 0)
      {
        query = query.Where(entry => entry.UserId == userId);
      }
      Dictionary<string, object> response = new Dictionary<string, object>();
      response.Add("categories", EnvironmentVariables.Categories);
      response.Add("remedies", query);
      return response;
    }
  }
}

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EarthlyRemedies.Models
{
  public static class EnvironmentVariables
  {
    public static List<string> Categories = new List<string> { "GI", "respiratory", "skin", "eyes", "hair", "musculoskeletal", "endocrine", "chakra", "pineal", "lymphatic", "cardiovascular", "digestive", "nervous", "reproductive" };

  }
}

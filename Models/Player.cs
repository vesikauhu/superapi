using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gameapi.Models
{
  public class Player
  {
    [BsonId]
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public double Kills = 0;

    public double Deaths = 0;
    public double Matches = 0;

    public double PickUps = 0;
    public double Accuracy = 100;

    public double MatchRatio = 0.00;
    public double DeathRatio = 0.00;
  }

  public class NewPlayer
  {
    [Required]
    public string Name { get; set; }
  }

  public class ModifiedPlayer
  {
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Matches { get; set; }
    public int PickUps { get; set; }
    public double Accuracy { get; set; }
  }
}
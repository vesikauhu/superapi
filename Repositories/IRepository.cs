using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gameapi.Models;

namespace gameapi.Repositories
{
  //All logic related to data-access happens on the classes that implement this
  public interface IRepository
  {
    Task<Player> CreatePlayer(Player player); 
    Task<Player> GetPlayer(Guid playerId); 
    Task<List<Player>> GetAllPlayers(); 
    Task<Player> GetByName(string name);

    Task<Player> UpdatePlayer(Player player); 
    Task<Player> DeletePlayer(Guid playerId);

    Task<List<Player>> Top10MatchRatio();
    Task<List<Player>> Top10DeathRatio();
    Task<List<Player>> Top10Accuracy();
    Task<List<Player>> Top10Pickups();
  }
}
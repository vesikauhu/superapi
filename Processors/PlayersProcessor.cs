using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Repositories;

namespace gameapi.Processors
{
  // Business logic (logic for verifying and updating the data) happens here
  public class PlayersProcessor
  {
    private readonly IRepository _repository;
    public PlayersProcessor(IRepository repository)
    {
      _repository = repository;
    }

    public Task<List<Player>> GetAll()
    {
      return _repository.GetAllPlayers();
    }

    public Task<Player> Get(Guid id)
    {
        return _repository.GetPlayer(id);
    }


    public Task<Player> GetByName(string name)
    {
      return _repository.GetByName(name);
    }

    public Task<List<Player>> Top10MatchRatio()
    {
      return _repository.Top10MatchRatio();
    }

    public Task<List<Player>> Top10DeathRatio()
    {
      return _repository.Top10DeathRatio();
    }

    public Task<List<Player>> Top10Accuracy()
    {
      return _repository.Top10Accuracy();
    }

    public Task<List<Player>> Top10Pickups()
    {
      return _repository.Top10Pickups();
    }

    public Task<Player> Create(NewPlayer newPlayer)
    {
      Player player = new Player()
      {
          Id = Guid.NewGuid(),
          Name = newPlayer.Name
      };
      return _repository.CreatePlayer(player);
    }

    public Task<Player> Delete(Guid id)
    {
        return _repository.DeletePlayer(id);
    }

    public async Task<Player> Update(Guid id, ModifiedPlayer modifiedPlayer)
    {
        Player player = await _repository.GetPlayer(id);
        player.Kills = modifiedPlayer.Kills;
        player.Deaths = modifiedPlayer.Deaths;
        player.Matches = modifiedPlayer.Matches;
        player.PickUps = modifiedPlayer.PickUps;
        player.Accuracy = modifiedPlayer.Accuracy;
        player.MatchRatio = player.Kills / player.Matches;
        player.DeathRatio = player.Kills / player.Deaths;
        await _repository.UpdatePlayer(player);
        return player;
    }
  }
}
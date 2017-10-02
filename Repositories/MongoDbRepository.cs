using System;
using System.Collections.Generic;
using gameapi.Exceptions;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.mongodb;
using MongoDB.Driver;

namespace gameapi.Repositories
{
  //Gets from and updates data to MongoDb 
  public class MongoDbRepository : IRepository
  {
    private IMongoCollection<Player> _collection;
    public MongoDbRepository(MongoDBClient client)
    {
      //Getting the database with name "game"
      IMongoDatabase database = client.GetDatabase("game");

      //Getting collection with name "players"
      _collection = database.GetCollection<Player>("players");
    }

    

    public async Task<Player> CreatePlayer(Player player)
    {
      await _collection.InsertOneAsync(player);
      return player;
    }


    public async Task<Player> DeletePlayer(Guid playerId)
    {
        Player player = await GetPlayer(playerId);
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        await _collection.DeleteOneAsync(filter);
        return player;
    }

    public async Task<List<Player>> GetAllPlayers()
    {
      List<Player> players = new List<Player>();
      var filter = FilterDefinition<Player>.Empty;
      using (IAsyncCursor<Player> cursor = await _collection.FindAsync(filter))
      {
        while (await cursor.MoveNextAsync())
          {
            IEnumerable<Player> batch = cursor.Current;
            foreach (Player p in batch)
            {
              players.Add(p);
            }
          }
      }
      return players;
    }

    public async Task<Player> GetPlayer(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        var cursor = await _collection.FindAsync(filter);
        bool playerFound = await cursor.AnyAsync();
        if (playerFound)
        {
          cursor = await _collection.FindAsync(filter);
          return await cursor.FirstAsync();
        }
        else
        {
          throw new NotFoundException();
        }
    }

    public async Task<Player> GetByName (string name)
    {
      var filter = Builders<Player>.Filter.Eq("Name", name);
      var cursor = await _collection.FindAsync(filter);
      bool playerFound = await cursor.AnyAsync();
      if (playerFound)
      {
        cursor = await _collection.FindAsync(filter);
        var player = await cursor.FirstAsync();
        return player;
      }
      else
      {
        throw new NotFoundException();
      }
    }


    public async Task<Player> UpdatePlayer(Player player)
    {
      var filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
      await _collection.ReplaceOneAsync(filter, player);
      return player;
    }

    public async Task<List<Player>> Top10MatchRatio()
    {
      SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending("MatchRatio");
      FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Matches", 10);
      IFindFluent<Player, Player> cursor = _collection.Find(filter).Sort(sortDef).Limit(10);
      List<Player> players = await cursor.ToListAsync();
      return players;
    }

    public async Task<List<Player>> Top10DeathRatio()
    {
      SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending("DeathRatio");
      FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Kills", 50);
      IFindFluent<Player, Player> cursor = _collection.Find(filter).Sort(sortDef).Limit(10);
      List<Player> players = await cursor.ToListAsync();
      return players;
    }

    public async Task<List<Player>> Top10Accuracy()
    {
      SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending("Accuracy");
      FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Matches", 10);
      IFindFluent<Player, Player> cursor = _collection.Find(filter).Sort(sortDef).Limit(10);
      List<Player> players = await cursor.ToListAsync();
      return players;
    }

    public async Task<List<Player>> Top10Pickups()
    {
      SortDefinition<Player> sortDef = Builders<Player>.Sort.Descending("PickUps");
      FilterDefinition<Player> filter = Builders<Player>.Filter.Empty;
      IFindFluent<Player, Player> cursor = _collection.Find(filter).Sort(sortDef).Limit(10);
      List<Player> players = await cursor.ToListAsync();
      return players;
    }
  }
}
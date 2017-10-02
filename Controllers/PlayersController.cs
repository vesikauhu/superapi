using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using gameapi.Exceptions;
using gameapi.Models;
using gameapi.ModelValidation;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{

    [Route("api/players")]
    public class PlayersController : Controller
    {
        private PlayersProcessor _processor;
        public PlayersController(PlayersProcessor processor)
        {
            _processor = processor;
        }
        
        [HttpGet]
        public Task<List<Player>> GetAll()
        {
            return _processor.GetAll();
        }

        [HttpGet("{id:guid}")]
        public Task<Player> Get(Guid id)
        {
            return _processor.Get(id);
        }
        

        [HttpGet("{name}")]
        public Task<Player> GetByName(string name)
        {
            return _processor.GetByName(name);
        }

        [HttpGet("top10matches")]
        public Task<List<Player>> Top10MatchRatio() 
        {
            return _processor.Top10MatchRatio();
        }

        [HttpGet("top10deaths")]
        public Task<List<Player>> Top10DeathRatio() 
        {
            return _processor.Top10DeathRatio();
        }

        [HttpGet("top10accuracy")]
        public Task<List<Player>> Top10Accuracy() 
        {
            return _processor.Top10Accuracy();
        }

        [HttpGet("top10pickups")]
        public Task<List<Player>> Top10PickUps() 
        {
            return _processor.Top10Pickups();
        }

        [HttpPost]
        [ValidateModel]
        public Task<Player> Create([FromBody]NewPlayer player)
        {
            return _processor.Create(player);
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)
        {
            return _processor.Delete(id);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public Task<Player> Update(Guid id, [FromBody]ModifiedPlayer player)
        {
            return _processor.Update(id, player);
        }
    }
}
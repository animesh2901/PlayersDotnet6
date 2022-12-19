using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PlayersDotnet6.Models;

namespace PlayersDotnet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext _playerContext;

        public PlayerController(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player>>> Get()
        {
            return Ok(await _playerContext.Players.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await _playerContext.Players.FindAsync(id);
            if (player == null)
                return BadRequest("Player Not Found");
            await Task.Run(() => player);
            return Ok(player);
        }


        [HttpPost]
        public async Task<ActionResult<List<Player>>> AddPlayer(Player player)
        {
            _playerContext.Players.Add(player);
            await _playerContext.SaveChangesAsync();
            return Ok(await _playerContext.Players.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<Player>>> UpdatePlayer(Player request)
        {
            var player = await _playerContext.Players.FindAsync(request.Id);
            if (player == null)
                return BadRequest("Player Not Found");

            player.FirstName = request.FirstName;
            player.LastName = request.LastName; 
            player.JerseyNumber = request.JerseyNumber;
            player.Team = request.Team;

            await _playerContext.SaveChangesAsync();
            return Ok(await _playerContext.Players.ToListAsync());
        }


        [HttpDelete ("{id}")]
        public async Task<ActionResult<Player>> Delete(int id)
        {
            var player = await _playerContext.Players.FindAsync(id);
            if (player == null)
                return BadRequest("Player Not Found");

            _playerContext.Players.Remove(player);
            await _playerContext.SaveChangesAsync();
            return Ok(await _playerContext.Players.ToListAsync());
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slotgame.Data;  // 引用 AppDbContext
using slotgame.Models; // 引用 GameList

namespace slotgame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GameList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameList>>> GetGames()
        {
            var games = await _context.GameList.ToListAsync();
            return Ok(games);
        }

        // POST: api/GameList
        [HttpPost]
        public async Task<ActionResult<GameList>> AddGame(GameList game)
        {
            try
            {
                _context.GameList.Add(game);
                await _context.SaveChangesAsync();
                return Ok(new { message = "✅ Insert success", data = game });
            }
            catch (DbUpdateException dbEx)
            {
                // 如果是資料庫層錯誤，印出更完整的資訊
                var inner = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                return BadRequest(new { message = "❌ Database error", error = inner });
            }
            catch (Exception ex)
            {
                // 一般例外
                return BadRequest(new { message = "❌ General error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameList game)
        {
            if (id != game.GameID)
            {
                return BadRequest();
            }
            _context.Entry(game).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.GameList.Any(e => e.GameID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.GameList.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            _context.GameList.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

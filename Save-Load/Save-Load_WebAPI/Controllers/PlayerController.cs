using Save_Load_WebAPI.Model;   
using Microsoft.AspNetCore.Mvc;

namespace Save_Load_WebAPI.Controllers
{
    public class PlayerController : ControllerBase
    {
        // Criando um Player Default
        public static List<Player> players = new()
        {
            new Player { Id = "1", Vida = 100, QuantidadeItens = 0, PosicaoX = 0, PosicaoY = 0, PosicaoZ = 0},
        };

        // Pega todos os players da lista acima
        [HttpGet]
        [Route("api/Player")]
        public IActionResult GetPlayers()
        {
            return Ok(players);
        }

        // Pega um único player por seu ID
        [HttpGet]
        [Route("api/Player/{id}")]
        public IActionResult GetPlayerById(string id)
        {
            var player = players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // Adiciona um player novo a lista de players
        [HttpPost]
        [Route("api/Player")]
        public IActionResult AddPlayer([FromBody] Player novoPlayer)
        {
            players.Add(novoPlayer);
            return Ok(novoPlayer);
        }

        // Deleta um player existente baseado em seu ID
        [HttpDelete]
        [Route("api/Player/{id}")]
        public IActionResult DeletePlayer(string id)
        {
            var player = players.FirstOrDefault(a => a.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            players.Remove(player);
            return Ok();
        }

        // Atualiza um player existente baseando-se no ID e do corpo da requisição
        [HttpPut]
        [Route("api/Player/{id}")]
        public IActionResult UpdatePlayer(string id, [FromBody] Player playerAtualizado)
        {
            var player = players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            player.Vida = playerAtualizado.Vida;
            player.QuantidadeItens = playerAtualizado.QuantidadeItens;
            player.PosicaoX = playerAtualizado.PosicaoX;
            player.PosicaoY = playerAtualizado.PosicaoY;
            player.PosicaoZ = playerAtualizado.PosicaoZ;
            return Ok(player);
        }
    }
}

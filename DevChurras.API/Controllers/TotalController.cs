using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("")]
    public class TotalController : ControllerBase
    {
        [HttpPost]
        [Route("totais")]
        public async Task<ActionResult<Total>> Post(
            [FromServices] DataContext context,
            [FromBody] Total model)
        {
            int valorPorPessoa = 20;
            int valorPorPessoaQueNaoBebe = 10;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var participantes = await context.Participantes.ToListAsync();

            foreach (var participante in participantes)
            {
                if (participante.ConsomeBebidaAlcoolica)
                    model.TotalArrecadado += valorPorPessoa;
                else
                    model.TotalArrecadado += valorPorPessoaQueNaoBebe;
            }

            var convidados = await context.Convidados.ToListAsync();

            foreach (var convidado in convidados)
            {
                if (convidado.ConsomeBebidaAlcoolica)
                    model.TotalArrecadado += valorPorPessoa;
                else
                    model.TotalArrecadado += valorPorPessoaQueNaoBebe;
            }

            return Ok(model);
        }
    }
}
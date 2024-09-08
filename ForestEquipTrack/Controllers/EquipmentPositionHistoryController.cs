using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ForestEquipTrack.Api.Controllers
{
    [ApiController]
    [Route("api/historico-posicao")]
    public class EquipmentPositionHistoryController : Controller
    {
        private readonly IEquipmentPositionHistoryS equipmentPositionHistoryS;
        private readonly IMapper mapper;

        public EquipmentPositionHistoryController(
            IEquipmentPositionHistoryS _equipmentPositionHistoryS,
            IMapper _mapper)
        {
            equipmentPositionHistoryS = _equipmentPositionHistoryS;
            mapper = _mapper;
        }

        /// <summary>
        /// Cria um histórico de posições de estados.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST 
        ///     {
        ///        "IdEquipment" : "2516...",
        ///        "Date" : "12/05/2008",
        ///        "Lat": "String",
        ///        "Lon": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="500">Se o item não for criado</response> 
        [HttpPost("novo")]
        public async Task<IActionResult> PostPH([FromForm] EquipmentPositionHistoryIM entityDTO)
        {
            try
            {
                var create = await equipmentPositionHistoryS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdPH), new { id = create.EquipmentPositionId }, create);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operação não concluida, Erro ao criar historico {ex.Message}");
            }
        }

        ///<summary>
        /// Buscar todos os itens.
        /// </summary>
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("todos-historicos")]
        public async Task<ActionResult<IEnumerable<EquipmentPositionHistoryVM>>> FindAllPH()
        {
            try
            {
                var clientAll = await equipmentPositionHistoryS.FindAllAsync();

                return Ok(clientAll);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"histórico não encontrados, Erro na operação {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("historicoPosicao/{id}")]
        public async Task<IActionResult> GetByIdPH([FromForm] Guid id)
        {
            try
            {
                var equipment = await equipmentPositionHistoryS.GetByIdAsync(id);

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Histórico não encontrado, Erro na operação {ex.Message}");
            }
        }

        /// <summary>
        /// Atualizar um item.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT 
        ///     {
        ///        "IdEquipmentPosition": "9034...",
        /// 
        ///        "IdEquipment" : "2516...",
        ///        "Date" : "12/05/2008",
        ///        "Lat": "String",
        ///        "Lon": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut("atualizar")]
        public async Task<IActionResult> PutPH([FromForm] Guid id, [FromForm] EquipmentPositionHistoryIM entityDTO)
        {
            try
            {
                await equipmentPositionHistoryS.UpdateAsync(id, entityDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletar o item pelo ID.
        /// </summary>
        ///
        /// <response code="400">Se o item não for deletado</response> 
        [HttpDelete("remover")]
        public async Task<IActionResult> DeletePH([FromForm] Guid id)
        {
            try
            {
                await equipmentPositionHistoryS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

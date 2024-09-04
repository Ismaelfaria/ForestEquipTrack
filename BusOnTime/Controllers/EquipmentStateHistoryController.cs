using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentStateHistoryController : Controller
    {
        private readonly IEquipmentStateHistoryS equipmentStateHistoryS;
        private readonly IMapper mapper;

        public EquipmentStateHistoryController(
            IEquipmentStateHistoryS _equipmentStateHistoryS,
            IMapper _mapper)
        {
            equipmentStateHistoryS = _equipmentStateHistoryS;
            mapper = _mapper;
        }

        /// <summary>
        /// Cria um histórico de registro de estados.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST 
        ///     {
        ///        "IdState" : "2516...",
        ///        "IdEquipment" : "8973...",
        ///        "Color": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="500">Se o item não for criado</response> 
        [HttpPost]
        public async Task<IActionResult> PostSH([FromForm] EquipmentStateHistoryIM entityDTO)
        {
            try
            {
                var create = await equipmentStateHistoryS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdSH), new { id = create.EquipmentStateId }, create);
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentStateHistoryVM>>> FindAllSH()
        {
            try
            {
                var clientAll = await equipmentStateHistoryS.FindAllAsync();

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
        [HttpGet("historicoEstado/{id}")]
        public async Task<IActionResult> GetByIdSH([FromForm] Guid id)
        {
            try
            {
                var equipment = await equipmentStateHistoryS.GetByIdAsync(id);

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
        ///        "EquipmentStateId": "2516...",
        /// 
        ///        "IdState" : "2516...",
        ///        "IdEquipment" : "8973...",
        ///        "Color": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut]
        public async Task<IActionResult> PutSH([FromForm] Guid id, [FromForm] EquipmentStateHistoryIM entityDTO)
        {
            try
            {
                await equipmentStateHistoryS.UpdateAsync(id, entityDTO);

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
        [HttpDelete]
        public async Task<IActionResult> DeleteSH([FromForm] Guid id)
        {
            try
            {
                await equipmentStateHistoryS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

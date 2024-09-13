using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ForestEquipTrack.Domain.Entities;

namespace ForestEquipTrack.Api.Controllers
{
    [ApiController]
    [Route("api/estado-historico")]
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
        [HttpPost("novo")]
        public async Task<IActionResult> PostSH([FromForm] EquipmentStateHistoryIM entityDTO)
        {
            try
            {
                var create = await equipmentStateHistoryS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdSH), new { id = create.EquipmentStatehistoryId }, create);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Message = "Solicitação inválida, informe todos os campos válidos.", Errors = errors });
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
        /// <response code="500">Erro na operação</response> 
        [HttpGet("todos-historicos-estados")]
        public async Task<ActionResult<IEnumerable<EquipmentStateHistoryVM>>> FindAllSH()
        {
            try
            {
                var equipmentStateHistoryAll = await equipmentStateHistoryS.FindAllAsync();

                if (equipmentStateHistoryAll == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentStateHistoryAll);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro na operação: {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        /// <response code="500">Erro na operação</response>
        [HttpGet("historico-estado/{id}")]
        public async Task<IActionResult> GetByIdSH([FromForm] Guid id)
        {
            try
            {
                var equipmentStateHistory = await equipmentStateHistoryS.GetByIdAsync(id);

                if (equipmentStateHistory == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentStateHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Histórico não encontrado, Erro na operação {ex.Message}");
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
        /// <response code="500">Erro na operação</response>
        [HttpPut("atualizar")]
        public async Task<IActionResult> PutSH([FromForm] Guid id, [FromForm] EquipmentStateHistoryIM entityDTO)
        {
            try
            {
                await equipmentStateHistoryS.UpdateAsync(id, entityDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletar o item pelo ID.
        /// </summary>
        ///
        /// <response code="500">Erro na operação</response> 
        [HttpDelete("remover")]
        public async Task<IActionResult> DeleteSH([FromForm] Guid id)
        {
            try
            {
                await equipmentStateHistoryS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }
    }
}

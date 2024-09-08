using AutoMapper;
<<<<<<< HEAD:ForestEquipTrack/Controllers/EquipmentPositionHistoryController.cs
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
=======
>>>>>>> 8b0414dc44985badb6962e4e8c6480cfba5f1092:BusOnTime/Controllers/EquipmentPositionHistoryController.cs
using FluentValidation;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
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
        /// <response code="500">Se o item não for encontrado</response> 
        [HttpGet("todos-historicos")]
        public async Task<ActionResult<IEnumerable<EquipmentPositionHistoryVM>>> FindAllPH()
        {
            try
            {
                var equipmentPositionAll = await equipmentPositionHistoryS.FindAllAsync();

                if (equipmentPositionAll == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentPositionAll);
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
        /// <response code="500">Se o item não for encontrado</response> 
        [HttpGet("historicoPosicao/{id}")]
        public async Task<IActionResult> GetByIdPH([FromForm] Guid id)
        {
            try
            {
                var equipmentPosition = await equipmentPositionHistoryS.GetByIdAsync(id);

                if (equipmentPosition == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro na operação: {ex.Message}");
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
        /// <response code="500">Se o item não for atualizado</response> 
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
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletar o item pelo ID.
        /// </summary>
        ///
        /// <response code="500">Se o item não for deletado</response> 
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
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }
    }
}

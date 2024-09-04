using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
    [ApiController]
    [Route("api/valor-equipamento")]
    public class EquipmentModelStateHourlyEarningController : Controller
    {
        private readonly IEquipmentModelStateHourlyEarningS equipmentModelStateHourlyEarningS;
        private readonly IMapper mapper;

        public EquipmentModelStateHourlyEarningController(
            IEquipmentModelStateHourlyEarningS _equipmentModelStateHourlyEarningS,
            IMapper _mapper)
        {
            equipmentModelStateHourlyEarningS = _equipmentModelStateHourlyEarningS;
            mapper = _mapper;
        }

        /// <summary>
        /// Cria um registro de valor por estado dos equipamentos.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST 
        ///     {
        ///        "IdModel" : "2516...",
        ///        "IdState" : "9289...",
        ///        "Value": "Decimal"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="500">Se o item não for criado</response> 
        [HttpPost("novo")]
        public async Task<IActionResult> PostEMS([FromForm] EquipmentModelStateHourlyEarningsIM entityDTO)
        {
            try
            {
                var create = await equipmentModelStateHourlyEarningS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdEMS), new { id = create.EquipmentModelStateHourlyEarningsId }, create);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operação não concluida, Erro ao criar registro de valor por estado do equipamento {ex.Message}");
            }
        }

        ///<summary>
        /// Buscar todos os itens.
        /// </summary>
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("todos-valores")]
        public async Task<ActionResult<IEnumerable<EquipmentModelStateHourlyEarningsVM>>> FindAllEMS()
        {
            try
            {
                var clientAll = await equipmentModelStateHourlyEarningS.FindAllAsync();

                return Ok(clientAll);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Registros não encontrados, Erro na operação {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("valor/{id}")]
        public async Task<IActionResult> GetByIdEMS([FromForm] Guid id)
        {
            try
            {
                var equipment = await equipmentModelStateHourlyEarningS.GetByIdAsync(id);

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Modelo não encontrado, Erro na operação {ex.Message}");
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
        ///        "EquipmentModelStateHourlyEarningsId" : "9087...",
        /// 
        ///        "IdModel" : "2516...",
        ///        "IdState" : "9289...",
        ///        "Value": "Decimal"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut("atualizar")]
        public async Task<IActionResult> PutEMS([FromForm] Guid id, [FromForm] EquipmentModelStateHourlyEarningsIM entityDTO)
        {
            try
            {
               await equipmentModelStateHourlyEarningS.UpdateAsync(id, entityDTO);

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
        public async Task<IActionResult> DeleteEMS([FromForm] Guid id)
        {
            try
            {
               await equipmentModelStateHourlyEarningS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

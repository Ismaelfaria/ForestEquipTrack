using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ForestEquipTrack.Api.Controllers
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
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Message = "Solicitação inválida, informe todos os campos válidos.", Errors = errors });
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
        ///  <response code="500">Se ocorrer algum erro</response>
        [HttpGet("todos-valores")]
        public async Task<ActionResult<IEnumerable<EquipmentModelStateHourlyEarningsVM>>> FindAllEMS()
        {
            try
            {
                var equipmentModelStateHourlyAll = await equipmentModelStateHourlyEarningS.FindAllAsync();

                if (equipmentModelStateHourlyAll == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentModelStateHourlyAll);
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
        ///  <response code="500">Se ocorrer algum erro</response>
        [HttpGet("valor/{id}")]
        public async Task<IActionResult> GetByIdEMS([FromForm] Guid id)
        {
            try
            {
                var equipmentModelStateHourly = await equipmentModelStateHourlyEarningS.GetByIdAsync(id);

                if (equipmentModelStateHourly == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentModelStateHourly);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Modelo não encontrado, Erro na operação {ex.Message}");
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
        ///  <response code="500">Se ocorrer algum erro</response>
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
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletar o item pelo ID.
        /// </summary>
        ///
        ///  <response code="500">Se ocorrer algum erro</response>
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
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }
    }
}

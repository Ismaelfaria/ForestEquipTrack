using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ForestEquipTrack.Api.Controllers
{
    [ApiController]
    [Route("api/estado")]
    public class EquipmentStateController : Controller
    {
        private readonly IEquipmentStateS equipmentStateS;
        private readonly IMapper mapper;

        public EquipmentStateController(
            IEquipmentStateS _equipmentStateS,
            IMapper _mapper)
        {
            equipmentStateS = _equipmentStateS;
            mapper = _mapper;
        }

        /// <summary>
        /// Cria um registro de Estado do Equipamento.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST 
        ///     {
        ///        "Name" : "String",
        ///        "Color": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="500">Se o item não for criado</response> 
        [HttpPost("novo")]
        public async Task<IActionResult> PostS([FromForm] EquipmentStateIM entityDTO)
        {
            try
            {
                var create = await equipmentStateS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdEM), new { id = create.EquipmentStateId }, create);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Message = "Solicitação inválida, informe todos os campos válidos.", Errors = errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operação não concluida, Erro ao criar estado de equipamento {ex.Message}");
            }
        }

        ///<summary>
        /// Buscar todos os itens.
        /// </summary>
        /// <response code="404">Se o item não for encontrado</response> 
        /// <response code="500">Erro na operação</response> 
        [HttpGet("todos-estados")]
        public async Task<ActionResult<IEnumerable<EquipmentStateVM>>> FindAllS()
        {
            try
            {
                var equipmentStateAll = await equipmentStateS.FindAllAsync();

                if (equipmentStateAll == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentStateAll);
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
        [HttpGet("equipamento-estado/{id}")]
        public async Task<IActionResult> GetByIdEM(Guid id)
        {
            try
            {
                var equipmentState = await equipmentStateS.GetByIdAsync(id);

                if (equipmentState == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentState);
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
        ///        "IdState": "2516...",
        /// 
        ///        "Name" : "String",
        ///        "Color": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="500">Erro na operação</response> 
        [HttpPut("atualizar")]
        public async Task<IActionResult> PutEM([FromForm] Guid id, [FromForm] EquipmentStateIM entityDTO)
        {
            try
            {
                await equipmentStateS.UpdateAsync(id, entityDTO);

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
        public async Task<IActionResult> DeleteEM([FromForm] Guid id)
        {
            try
            {
                await equipmentStateS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }
    }
}

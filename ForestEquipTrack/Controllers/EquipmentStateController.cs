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
                return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
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
        [HttpGet("todos-estados")]
        public async Task<ActionResult<IEnumerable<EquipmentStateVM>>> FindAllS()
        {
            try
            {
                var clientAll = await equipmentStateS.FindAllAsync();

                return Ok(clientAll);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Estado dos equipamentos não encontrados, Erro na operação {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("equipamento-estado/{id}")]
        public async Task<IActionResult> GetByIdEM(Guid id)
        {
            try
            {
                var equipment = await equipmentStateS.GetByIdAsync(id);

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Estado não encontrado, Erro na operação {ex.Message}");
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
        /// <response code="400">Se o item não for atualizado</response> 
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
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletar o item pelo ID.
        /// </summary>
        ///
        /// <response code="400">Se o item não for deletado</response> 
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
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

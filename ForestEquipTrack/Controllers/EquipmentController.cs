using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ForestEquipTrack.Api.Controllers
{
    [ApiController]
    [Route("api/equipamento")]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentS equipmentS;
        private readonly IMapper mapper;

        public EquipmentController(
            IEquipmentS _equipmentS,
            IMapper _mapper)
        {
            equipmentS = _equipmentS;
            mapper = _mapper;
        }

        /// <summary>
        /// Cria um registro de Equipamento.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST 
        ///     {
        ///        "IdModel" : "2516...",
        ///        "nome": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="500">Se o item não for criado</response> 
        [HttpPost("novo")]
        public async Task<IActionResult> PostE([FromForm] EquipmentIM entityDTO)
        {
            try
            {
                var create = await equipmentS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdE), new { id = create.EquipmentId }, create);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operação não concluida, Erro ao criar equipamento {ex.Message}");
            }
        }

        ///<summary>
        /// Buscar todos os itens.
        /// </summary>
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("todos-equipamentos")]
        public async Task<ActionResult<IEnumerable<EquipmentVM>>> FindAllE()
        {
            try
            {
                var clientAll = await equipmentS.FindAllAsync();

                return Ok(clientAll);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Modelo não encontrado, Erro na operação: {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("equipamento/{id}")]
        public async Task<IActionResult> GetByIdE([FromForm] Guid id)
        {
            try
            {
                var equipment = await equipmentS.GetByIdAsync(id);

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Equipamento não encontrado, Erro na operação {ex.Message}");
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
        ///        "id": "2516...",
        /// 
        ///        "IdModel" : "2516...",
        ///        "nome": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut("atualizar")]
        public async Task<IActionResult> PutE([FromForm] Guid id, [FromForm] EquipmentIM entityDTO)
        {
            try
            {
                await equipmentS.UpdateAsync(id, entityDTO);

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
        [HttpDelete("Remover")]
        public async Task<IActionResult> DeleteE(Guid id)
        {
            try
            {
                await equipmentS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

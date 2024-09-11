using AutoMapper;
using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.DTOs.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using ForestEquipTrack.Domain.Entities;

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
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Message = "Solicitação inválida, informe todos os campos válidos.", Errors = errors });
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
                var equipmentAll = await equipmentS.FindAllAsync();

                if (equipmentAll == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentAll);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Message = "Solicitação inválida, informe todos os campos válidos.", Errors = errors });
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
        [HttpGet("equipamento/{id}")]
        public async Task<IActionResult> GetByIdE([FromForm] Guid id)
        {
            try
            {
                var equipment = await equipmentS.GetByIdAsync(id);

                if (equipment == null)
                {
                    return StatusCode(404, $"Usuario não encontrados");
                }

                return Ok(equipment);
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
                return StatusCode(500, $"Request Error: {ex.Message}");
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
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }
    }
}

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
    [Route("api/modelo")]
    public class EquipmentModelController : Controller
    {
        private readonly IEquipmentModelS equipmentModelS;
        private readonly IMapper mapper;

        public EquipmentModelController(
            IEquipmentModelS _equipmentModelS,
            IMapper _mapper)
        {
            equipmentModelS = _equipmentModelS;
            mapper = _mapper;
        }

        /// <summary>
        /// Cria um registro de Modelo do Equipamento.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST 
        ///     {
        ///        "IdEquipment" : "2516...",
        ///        "name": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Retorna o novo item criado</response>
        /// <response code="500">Se o item não for criado</response> 
        [HttpPost("Novo")]
        public async Task<IActionResult> PostEM([FromForm] EquipmentModelIM entityDTO)
        {
            try
            {
                var create = await equipmentModelS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdEM), new { id = create.EquipmentModelId }, create);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { Message = "Solicitação inválida, informe todos os campos válidos.", Errors = errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operação não concluida:{ex.Message}");
            }
        }

        ///<summary>
        /// Buscar todos os itens.
        /// </summary>
        /// <response code="404">Se o item não for encontrado</response> 
        /// <response code="500">Se ocorrer algum erro</response> 
        [HttpGet("todos-modelos")]
        public async Task<ActionResult<IEnumerable<EquipmentModelVM>>> FindAllEM()
        {
            try
            {
                var equipmentModelAll = await equipmentModelS.FindAllAsync();

                if (equipmentModelAll == null)
                {
                    return StatusCode(404, $"Usuarios não encontrados");
                }

                return Ok(equipmentModelAll);
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
        /// <response code="500">Se ocorrer algum erro</response> 
        [HttpGet("modelo/{id}")]
        public IActionResult GetByIdEM([FromForm] Guid id)
        {
            try
            {
                var equipmentModel = equipmentModelS.GetByIdAsync(id);

                if (equipmentModel == null)
                {
                    return StatusCode(404, $"Usuario não encontrados");
                }

                return Ok(equipmentModel);
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
        ///        "idModel": "2516...",
        /// 
        ///        "IdEquipment" : "2516...",
        ///        "name": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="500">Se o item não for atualizado</response> 
        [HttpPut("atualizar")]
        public IActionResult PutEM([FromForm] Guid id, [FromForm] EquipmentModelIM entityDTO)
        {
            try
            {
                equipmentModelS.UpdateAsync(id, entityDTO);

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
        public IActionResult DeleteEM([FromForm] Guid id)
        {
            try
            {
                equipmentModelS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Request Error: {ex.Message}");
            }
        }
    }
}

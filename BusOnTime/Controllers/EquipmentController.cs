using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
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
        [HttpPost]
        public IActionResult PostE([FromForm] EquipmentIM entityDTO)
        {
            try
            {
                var create = equipmentS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdE), new { id = create.Result.EquipmentId }, create);
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
        [HttpGet]
        public IActionResult FindAllE()
        {
            try
            {
                var clientAll = equipmentS.FindAllAsync();

                return Ok(clientAll);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Modelo não encontrados, Erro na operação {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("equipamento/{id}")]
        public IActionResult GetByIdE(Guid id)
        {
            try
            {
                var equipment = equipmentS.GetByIdAsync(id);

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
        [HttpPut]
        public IActionResult PutE([FromForm] Guid id, EquipmentIM entityDTO)
        {
            try
            {
                equipmentS.UpdateAsync(id, entityDTO);

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
        public IActionResult DeleteE(Guid id)
        {
            try
            {
                equipmentS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

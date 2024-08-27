using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
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
        [HttpPost]
        public IActionResult PostSH([FromForm] EquipmentStateHistoryIM entityDTO)
        {
            try
            {
                var create = equipmentStateHistoryS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdSH), new { id = create.Result.EquipmentStateId }, create);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
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
        [HttpGet]
        public IActionResult FindAllSH()
        {
            try
            {
                var clientAll = equipmentStateHistoryS.FindAllAsync();

                return Ok(clientAll);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"histórico não encontrados, Erro na operação {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar o item pelo id.
        /// </summary>
        ///
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("historicoEstado/{id}")]
        public IActionResult GetByIdSH(Guid id)
        {
            try
            {
                var equipment = equipmentStateHistoryS.GetByIdAsync(id);

                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Histórico não encontrado, Erro na operação {ex.Message}");
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
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut]
        public IActionResult PutSH([FromForm] Guid id, EquipmentStateHistoryIM entityDTO)
        {
            try
            {
                equipmentStateHistoryS.UpdateAsync(id, entityDTO);

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
        public IActionResult DeleteSH(Guid id)
        {
            try
            {
                equipmentStateHistoryS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

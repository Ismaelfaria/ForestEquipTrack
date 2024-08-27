using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
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
        [HttpPost]
        public IActionResult PostPH([FromForm] EquipmentPositionHistoryIM entityDTO)
        {
            try
            {
                var create = equipmentPositionHistoryS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdPH), new { id = create.Result.EquipmentPositionId }, create);
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
        public IActionResult FindAllPH()
        {
            try
            {
                var clientAll = equipmentPositionHistoryS.FindAllAsync();

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
        [HttpGet("historicoPosicao/{id}")]
        public IActionResult GetByIdPH(Guid id)
        {
            try
            {
                var equipment = equipmentPositionHistoryS.GetByIdAsync(id);

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
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut]
        public IActionResult PutPH([FromForm] Guid id, EquipmentPositionHistoryIM entityDTO)
        {
            try
            {
                equipmentPositionHistoryS.UpdateAsync(id, entityDTO);

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
        public IActionResult DeletePH(Guid id)
        {
            try
            {
                equipmentPositionHistoryS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

using AutoMapper;
using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Mapping.DTOs.InputModel;
using BusOnTime.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusOnTime.Web.Controllers
{
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
        [HttpPost]
        public IActionResult PostEM([FromForm] EquipmentModelIM entityDTO)
        {
            try
            {
                var create = equipmentModelS.CreateAsync(entityDTO);

                return CreatedAtAction(nameof(GetByIdEM), new { id = create.Result.ModelId }, create);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Operação não concluida, Erro ao criar modelo de equipamento {ex.Message}");
            }
        }

        ///<summary>
        /// Buscar todos os itens.
        /// </summary>
        /// <response code="404">Se o item não for encontrado</response> 
        [HttpGet("buscar-todos-clientes")]
        public IActionResult FindAllEM()
        {
            try
            {
                var clientAll = equipmentModelS.FindAllAsync();

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
        [HttpGet("equipamentoModelo/{id}")]
        public IActionResult GetByIdEM(Guid id)
        {
            try
            {
                var equipment = equipmentModelS.GetByIdAsync(id);

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
        ///        "idModel": "2516...",
        /// 
        ///        "IdEquipment" : "2516...",
        ///        "name": "String"
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item atualizado</returns>
        /// <response code="201">Retorna o novo item atualizado</response>
        /// <response code="400">Se o item não for atualizado</response> 
        [HttpPut]
        public IActionResult PutEM([FromForm] Guid id, EquipmentModelIM entityDTO)
        {
            try
            {
                equipmentModelS.UpdateAsync(id, entityDTO);

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
        public IActionResult DeleteEM(Guid id)
        {
            try
            {
                equipmentModelS.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Request Error: {ex.Message}");
            }
        }
    }
}

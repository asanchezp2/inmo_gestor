using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Application.Interfaces;
using RealEstateAPI.Domain.Enums;

namespace RealEstateAPI.Controllers;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly ILogger<PropertiesController> _logger;

    public PropertiesController(IPropertyService propertyService, ILogger<PropertiesController> logger)
    {
        _propertyService = propertyService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<PropertyResponseDTO>> Create([FromBody] PropertyCreateDTO dto)
    {
        try
        {
            var result = await _propertyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.PropertyId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property");
            return StatusCode(500, new { error = "An error occurred while creating the property" });
        }
    }

    [HttpGet]
    public async Task<ActionResult<PropertyListResponseDTO>> GetAll(
        [FromQuery] PropertyType? type,
        [FromQuery] PropertyStatus? status,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] Zone? zone,
        [FromQuery] decimal? minArea,
        [FromQuery] decimal? maxArea,
        [FromQuery] int? advisorId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var result = await _propertyService.GetAllAsync(
                type, status, minPrice, maxPrice, zone, minArea, maxArea, advisorId, page, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties");
            return StatusCode(500, new { error = "An error occurred while retrieving properties" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyResponseDTO>> GetById(string id)
    {
        try
        {
            var result = await _propertyService.GetByIdAsync(id);
            
            if (result == null)
            {
                return NotFound(new { error = $"Property with ID {id} not found" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving property {PropertyId}", id);
            return StatusCode(500, new { error = "An error occurred while retrieving the property" });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyResponseDTO>> Update(string id, [FromBody] PropertyUpdateDTO dto)
    {
        try
        {
            var result = await _propertyService.UpdateAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating property {PropertyId}", id);
            return StatusCode(500, new { error = "An error occurred while updating the property" });
        }
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<PropertyResponseDTO>> UpdateStatus(string id, [FromBody] PropertyStatusUpdateDTO dto)
    {
        try
        {
            var result = await _propertyService.UpdateStatusAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating property status {PropertyId}", id);
            return StatusCode(500, new { error = "An error occurred while updating the property status" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _propertyService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting property {PropertyId}", id);
            return StatusCode(500, new { error = "An error occurred while deleting the property" });
        }
    }
}

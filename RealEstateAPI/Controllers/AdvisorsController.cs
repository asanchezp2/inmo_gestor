using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Application.DTOs;
using RealEstateAPI.Application.Interfaces;

namespace RealEstateAPI.Controllers;

[ApiController]
[Route("api/advisors")]
public class AdvisorsController : ControllerBase
{
    private readonly IAdvisorService _advisorService;
    private readonly ILogger<AdvisorsController> _logger;

    public AdvisorsController(IAdvisorService advisorService, ILogger<AdvisorsController> logger)
    {
        _advisorService = advisorService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<AdvisorResponseDTO>> Create([FromBody] AdvisorCreateDTO dto)
    {
        try
        {
            var result = await _advisorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.AdvisorId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating advisor");
            return StatusCode(500, new { error = "An error occurred while creating the advisor" });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdvisorResponseDTO>>> GetAll()
    {
        try
        {
            var result = await _advisorService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving advisors");
            return StatusCode(500, new { error = "An error occurred while retrieving advisors" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdvisorResponseDTO>> GetById(int id)
    {
        try
        {
            var result = await _advisorService.GetByIdAsync(id);
            
            if (result == null)
            {
                return NotFound(new { error = $"Advisor with ID {id} not found" });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving advisor {AdvisorId}", id);
            return StatusCode(500, new { error = "An error occurred while retrieving the advisor" });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AdvisorResponseDTO>> Update(int id, [FromBody] AdvisorUpdateDTO dto)
    {
        try
        {
            var result = await _advisorService.UpdateAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating advisor {AdvisorId}", id);
            return StatusCode(500, new { error = "An error occurred while updating the advisor" });
        }
    }

    [HttpGet("{id}/properties")]
    public async Task<ActionResult<IEnumerable<PropertyResponseDTO>>> GetProperties(int id)
    {
        try
        {
            var result = await _advisorService.GetPropertiesByAdvisorIdAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving properties for advisor {AdvisorId}", id);
            return StatusCode(500, new { error = "An error occurred while retrieving advisor properties" });
        }
    }
}

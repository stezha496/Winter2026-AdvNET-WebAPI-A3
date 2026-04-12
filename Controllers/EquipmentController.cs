using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentController(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEquipment()
    {
        List<Equipment> equipment = await _equipmentRepository.GetAllEquipment();
        return Ok(equipment);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEquipmentById(int id)
    {
        Equipment? equipment = await _equipmentRepository.GetEquipmentById(id);

        if (equipment == null)
            return NotFound();

        return Ok(equipment);
    }

    [Authorize(Roles = "ITAdmin")]
    [HttpPost]
    public async Task<IActionResult> AddEquipment([FromBody] Equipment equipment)
    {
        await _equipmentRepository.AddEquipment(equipment);
        return Ok(equipment);
    }

    [Authorize(Roles = "ITAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipment(int id, [FromBody] Equipment newEquipment)
    {
        await _equipmentRepository.UpdateEquipment(id, newEquipment);
        return Ok(newEquipment);
    }

    [Authorize(Roles = "ITAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipmentById(int id)
    {
        await _equipmentRepository.DeleteEquipmentById(id);
        return Ok();
    }
}
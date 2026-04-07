using _991745453_IT_ASSET_API.Models;
using _991745453_IT_ASSET_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _991745453_IT_ASSET_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    // Inject repository
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentController(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    [HttpGet]
    public List<Equipment> GetAllEquipment() {
        return _equipmentRepository.GetAllEquipment().Result;
    }

    [HttpGet("{id}")]
    public Equipment GetEquipmentById(int userId)
    {
        return _equipmentRepository.GetEquipmentById(userId).Result;
    }

    [HttpPost]
    public Task Post([FromBody] Equipment equipment) => _equipmentRepository.AddEquipment(equipment);

    [HttpPost("{id}")]
    public void UpdateEquipment(int equipmentId, Equipment newEquipment)
    {
        _equipmentRepository.UpdateEquipment(equipmentId, newEquipment);
    }

    [HttpDelete("{id}")]
    public void DeleteEquipmentById(int equipmentId)
    {
        _equipmentRepository.DeleteEquipmentById(equipmentId);
    }
}

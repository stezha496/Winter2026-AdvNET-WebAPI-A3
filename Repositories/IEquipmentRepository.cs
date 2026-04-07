using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface IEquipmentRepository
{
    Task<List<Equipment>> GetAllEquipment();
    Task<Equipment?> GetEquipmentById(int id);
    Task AddEquipment(Equipment equipment);

    Task UpdateEquipment(int equipmentId, Equipment equipment);
    Task DeleteEquipmentById(int id);
}

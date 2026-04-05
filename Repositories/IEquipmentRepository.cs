using _991745453_IT_ASSET_API.Models;

namespace _991745453_IT_ASSET_API.Repositories;

public interface IEquipmentRepository
{
    List<Equipment> GetAllEquipment();
    Equipment GetEquipmentById(int id);
    Equipment AddEquipment(Equipment equipment);

    void UpdateEquipment(int equipmentId, Equipment equipment);
    void DeleteEquipmentById(int id);
}

using _991745453_IT_ASSET_API.Data;
using _991745453_IT_ASSET_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _991745453_IT_ASSET_API.Repositories;

public class EquipmentRepository(AppDbContext context) : IEquipmentRepository
{
    public async Task AddEquipment(Equipment equipment)
    {
        await context.Equipment.AddAsync(equipment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteEquipmentById(int id)
    {
        await context.Equipment
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<Equipment>> GetAllEquipment()
    {
        return await context.Equipment.ToListAsync();
    }

    public async Task<Equipment?> GetEquipmentById(int id)
    {
        return await context.Equipment.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateEquipment(int id, Equipment newEquipment)
    {
        Equipment? existing = await context.Equipment.FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null) return;

        existing.AssetTag = newEquipment.AssetTag;
        existing.DeviceName = newEquipment.DeviceName;
        existing.IsAvailable = newEquipment.IsAvailable;

        await context.SaveChangesAsync();
    }
}

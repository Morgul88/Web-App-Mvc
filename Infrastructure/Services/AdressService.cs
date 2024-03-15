using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class AdressService(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<AddressEntities> GetAddressAsync(string UserId)
    {
        var adressEntity = await _context.Adresses.FirstOrDefaultAsync(x => x.ApplicationUserId == UserId);

        return adressEntity!;
    }

    public async Task<bool> CreateAdressAsync(AddressEntities entity)
    {
        _context.Adresses.Add(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAdressAsync(AddressEntities entity)
    {
        var existing = await _context.Adresses.FirstOrDefaultAsync(x => x.ApplicationUserId == entity.ApplicationUserId);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        
        return false;
    }
}

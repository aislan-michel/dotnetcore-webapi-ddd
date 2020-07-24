using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
     public class BaseRepository<Table> : IRepository<Table> where Table : BaseEntity
     {
          protected readonly MyContext _context;
          private DbSet<Table> _dataSet;

          public BaseRepository(MyContext context)
          {
               _context = context;
               _dataSet = _context.Set<Table>();
          }

          public async Task<bool> DeleteAsync(long id)
          {
               try
               {
                    var result = await _dataSet.SingleOrDefaultAsync(table => table.Id.Equals(id));

                    if(result == null)
                    {
                         return false;
                    }

                    _dataSet.Remove(result);
                    await _context.SaveChangesAsync();

                    return true;
               }
               catch(Exception exception)
               {
                    throw exception;
               }
          }

          public async Task<Table> InsertAsync(Table entity)
          {
               try
               {
                    if(entity.Id < 0)
                    {
                         return null;
                    }

                    entity.CreateAt = DateTime.UtcNow;

                    await _dataSet.AddAsync(entity);
                    await _context.SaveChangesAsync();
               }
               catch (Exception exception)
               {
                    throw exception;
               }

               return entity;
          }

          public async Task<bool> ExistAsync(long id)
          {
               return await _dataSet.AnyAsync(table => table.Id.Equals(id));
          }

          public async Task<Table> SelectAsync(long id)
          {
               try
               {
                    return await _dataSet.SingleOrDefaultAsync(table => table.Id.Equals(id));
               }
               catch (Exception exception)
               {
                    throw exception;
               }
          }

          public async Task<IEnumerable<Table>> SelectAsync()
          {
               try
               {
                    return await _dataSet.ToListAsync();
               }
               catch (Exception exception)
               {
                    throw exception;
               }
          }

          public async Task<Table> UpdateAsync(Table entity)
          {
               try
               {
                    var result = await _dataSet.SingleOrDefaultAsync(table => table.Id.Equals(entity.Id));

                    if(result == null)
                    {
                         return null;
                    }

                    entity.UpdateAt = DateTime.UtcNow;
                    entity.CreateAt = result.CreateAt;

                    _context.Entry(result).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
               }
               catch (Exception exception)
               {
                    throw exception;
               }

               return entity;
          }
     }
}

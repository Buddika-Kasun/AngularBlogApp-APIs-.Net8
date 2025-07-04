﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Presistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories
{
    public class GenaricRepository<TEntity>(BlogDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        internal readonly DbSet<TEntity> DbSet = context.Set<TEntity>();
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await DbSet.AddAsync(entity);
            return entry.Entity;
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            var entry = DbSet.Update(entity);
            return entry.Entity;
        }
    }
}

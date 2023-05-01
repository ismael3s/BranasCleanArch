﻿using Application.Order.Repositories;

namespace Infra.Data.EF.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CommitAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}

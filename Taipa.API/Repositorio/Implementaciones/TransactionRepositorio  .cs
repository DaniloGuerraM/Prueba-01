using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Taipa.API.Repositorio.Interface;
using Taipa.App.Repositorio.Contexto;

namespace Taipa.API.Repositorio.Implementaciones
{
    public class TransactionRepositorio : ITransactionRepositorio
    {
        private readonly AppContexto _context;
        public TransactionRepositorio(AppContexto context)
        {
            _context = context;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
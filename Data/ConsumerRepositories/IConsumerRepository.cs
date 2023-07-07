using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ConsumerRepositories
{
    public interface IConsumerRepository : IRepositoryBase<Consumer>
    {
    }
}
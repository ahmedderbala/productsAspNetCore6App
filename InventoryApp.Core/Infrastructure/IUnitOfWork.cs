using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        /// <summary>   Commits this object. </summary>
        void Commit();

        Task<int> CommitAsync();
    }
}

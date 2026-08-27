using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMS.Framework.Data.Services
{
    public abstract class BaseDataService : IDataService
    {
        public IUnitOfWork UnitOfWork;

        protected BaseDataService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                UnitOfWork.Dispose();
            }
            _disposed = true;
        }
    }
}

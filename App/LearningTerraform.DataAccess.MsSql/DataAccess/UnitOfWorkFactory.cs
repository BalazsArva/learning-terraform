using System;
using LearningTerraform.BusinessLogic.DataAccess.Abstract;

namespace LearningTerraform.DataAccess.MsSql.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly Func<DataContext> creator;

        public UnitOfWorkFactory(Func<DataContext> creator)
        {
            this.creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(creator());
        }
    }
}

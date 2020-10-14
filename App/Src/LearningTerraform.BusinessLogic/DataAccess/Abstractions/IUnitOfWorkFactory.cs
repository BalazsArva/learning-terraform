namespace LearningTerraform.BusinessLogic.DataAccess.Abstractions
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}

namespace LearningTerraform.BusinessLogic.DataAccess.Abstract
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}

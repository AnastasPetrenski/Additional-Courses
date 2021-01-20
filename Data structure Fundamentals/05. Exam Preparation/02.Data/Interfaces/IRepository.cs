namespace _02.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository
    {
        int Size { get; }

        void Add(IEntity entity);

        IEntity GetById(int id);

        List<IEntity> GetByParentId(int parentId);

        List<IEntity> GetAll();

        IRepository Copy();

        List<IEntity> GetAllByType(string type);

        List<IEntity> GetAllByStatus(string status);

        IEntity PeekMostRecent();

        IEntity DequeueMostRecent();
    }
}

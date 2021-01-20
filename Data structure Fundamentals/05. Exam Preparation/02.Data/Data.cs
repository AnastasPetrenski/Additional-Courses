namespace _02.Data
{
    using _02.Data.Interfaces;
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> _entities;

        public Data()
        {
            this._entities = new OrderedBag<IEntity>();
        }

        public Data(Data copy)
        {
            this._entities = copy._entities;
        }

        public int Size => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Add(entity);

            var parentNode = this.GetById((int)entity.ParentId);

            if (parentNode != null)
            {
                parentNode.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {
            Data copy = (Data)this.MemberwiseClone();

            return new Data(copy); 
        }

        public IEntity DequeueMostRecent()
        {
            ValidateNotNull();

            return this._entities.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._entities);
        }

        public List<IEntity> GetAllByType(string type)
        {
            List<IEntity> entities = new List<IEntity>();

            CheckTypeValidation(type);

            for (int i = 0; i < this.Size; i++)
            {
                if (this._entities[i].GetType().Name.Equals(type))
                {
                    entities.Add(this._entities[i]);
                }
            }

            return entities;
        }

        private void CheckTypeValidation(string type)
        {
            bool isValid = false;

            if (type.Equals(typeof(Invoice).Name))
            {
                isValid = true;
            }
            else if (type.Equals(typeof(StoreClient).Name))
            {
                isValid = true;
            }
            else if (type.Equals(typeof(User).Name))
            {
                isValid = true;
            }

            if (!isValid)
            {
                throw new InvalidOperationException($"Invalid type: {type}");
            }

        }

        public List<IEntity> GetAllByStatus(string status)
        {
            List<IEntity> entities = new List<IEntity>();

            Enum.TryParse(status, out BaseEntityStatus result);

            for (int i = 0; i < this.Size; i++)
            {
                if (this._entities[i].Status.Equals(result))
                {
                    entities.Add(this._entities[i]);
                }
            }

            return entities;
        }

        public IEntity GetById(int id)
        {
            if (id < 0 || id > this.Size)
            {
                return null;
            }

            //TODO Check for bugs
            return this._entities[this.Size - 1 - id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parent = this.GetById(parentId);

            if (parent == null)
            {
                return new List<IEntity>();
            }

            return parent.Children;
        }

        public IEntity PeekMostRecent()
        {
            ValidateNotNull();

            return this._entities.GetFirst();
        }

        private void ValidateNotNull()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }
    }
}

namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Loader : IBuffer
    {
        private List<IEntity> _entities;

        public Loader()
        {
            this._entities = new List<IEntity>();
        }

        public int EntitiesCount => this._entities.Count;

        // O(1) amortisized coz of resizing
        public void Add(IEntity entity)
        {
            this._entities.Add(entity);
        }

        // O(n)
        public void Clear()
        {
            this._entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this._entities.Contains(entity);
        }


        public IEntity Extract(int id)
        {
            //ValidateIndex(id);

            var element = GetEntityById(id);

            if (element == null)
            {
                return null;
            }

            this._entities.Remove(element);
            return element;
        }

        private IEntity GetEntityById(int id)
        {
            IEntity element = null;

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                if (this._entities[i].Id == id)
                {
                    element = this._entities[i];
                    //this._entities.Remove(element);
                    break;
                }
            }

            return element;
        }

        public IEntity Find(IEntity entity)
        {
            return GetEntityById(entity.Id);
        }
       
        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            foreach (var item in this._entities)
            {
                yield return item;
            }
        }

        public void RemoveSold()
        {
            //this._entities = this._entities.Where(e => e.Status != BaseEntityStatus.Sold).ToList();

            this._entities.RemoveAll(e => e.Status == BaseEntityStatus.Sold);

            //var result = new List<IEntity>();
            //foreach (var item in this._entities)
            //{
            //    if (item.Status != BaseEntityStatus.Sold)
            //    {
            //        result.Add(item);
            //    }
            //}

            //this._entities = result;
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int index = GetIndexElement(oldEntity);

            this._entities[index] = newEntity;
        }


        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            //return this._entities.Where(e => (int)e.Status >= (int)lowerBound &&
            //                                 (int)e.Status <= (int)upperBound).ToList();
            int lower = (int)lowerBound;
            int upper = (int)upperBound;

            var result = new List<IEntity>(this.EntitiesCount);
            foreach (var item in this._entities)
            {
                if (lower <= (int)item.Status && (int)item.Status <= upper)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            var firstIndex = GetIndexElement(first);
            var seconfIndex = GetIndexElement(second);

            this._entities[firstIndex] = second;
            this._entities[seconfIndex] = first;
        }

        public IEntity[] ToArray()
        {
            return this._entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            Action<IEntity> changeStatus = (x) => ChangeStatus(x, oldStatus, newStatus); 
            //{
            //    if (x.Status == oldStatus)
            //    {
            //        x.Status = newStatus;
            //    }
            //}; 
            this._entities.ForEach(changeStatus);
        }

        private void ChangeStatus(IEntity x, BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            if (x.Status == oldStatus)
            {
                x.Status = newStatus;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateIndex(int id)
        {
            if (id < 0 || id > this._entities.Count)
            {
                throw new ArgumentException();
            }
        }

        private int GetIndexElement(IEntity oldEntity)
        {
            var index = this._entities.IndexOf(oldEntity);

            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            return index;
        }
    }
}

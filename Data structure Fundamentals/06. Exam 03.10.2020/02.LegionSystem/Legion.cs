namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedBag<IEnemy> legion;

        public Legion()
        {
            this.legion = new OrderedBag<IEnemy>();
        }

        public int Size => this.legion.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.legion.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            if (!this.legion.Contains(enemy))
            {
                this.legion.Add(enemy);
            }
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            IEnemy unit = default;
            foreach (var item in this.legion)
            {
                if (item.AttackSpeed == speed)
                {
                    unit = item;
                    break;
                }
            }

            return unit;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> fasters = new List<IEnemy>();
            for (int i = 0; i < this.Size; i++)
            {
                if (this.legion[i].AttackSpeed > speed)
                {
                    fasters.Add(this.legion[i]);
                }
            }

            return fasters;
        }

        public IEnemy GetFastest()
        {
            ValidateNotEmpty();
            return this.legion[this.Size - 1];
        }

        public IEnemy[] GetOrderedByHealth()
        {
            IEnemy[] orderedByHealth = new IEnemy[this.Size];
            orderedByHealth = this.legion.OrderByDescending(x => x.Health).ToArray();

            //orderedByHealth[0] = this.legion[0];
            //int result;
            //OrderByHealt order = new OrderByHealt();

            //for (int i = 1; i < this.Size - 1; i++)
            //{   
            //    result = order.Compare(orderedByHealth[0], this.legion[i + 1]);
            //    if (result > 0)
            //    {
            //        var temp = orderedByHealth[0];
            //        orderedByHealth[0] = this.legion[i];
            //        orderedByHealth[i] = temp;
            //    }
            //    else
            //    {
            //        orderedByHealth[i] = this.legion[i];
            //    }
            //}

            //result = order.Compare(orderedByHealth[0], this.legion[this.Size - 1]);
            //if (result > 1)
            //{
            //    var temp = orderedByHealth[0];
            //    orderedByHealth[0] = this.legion[this.Size - 1];
            //    orderedByHealth[this.Size - 1] = temp;
            //}
            //else
            //{
            //    orderedByHealth[this.Size - 1] = this.legion[this.Size - 1];
            //}

            return orderedByHealth;
        }

        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> slowers = new List<IEnemy>();
            for (int i = 0; i < this.Size; i++)
            {
                if (this.legion[i].AttackSpeed < speed)
                {
                    slowers.Add(this.legion[i]);
                }
            }

            return slowers;
        }

        public IEnemy GetSlowest()
        {
            ValidateNotEmpty();

            return this.legion[0];
        }

        public void ShootFastest()
        {
            ValidateNotEmpty();
            this.legion.RemoveLast();
        }

        public void ShootSlowest()
        {
            ValidateNotEmpty();
            this.legion.RemoveFirst();
        }

        private void ValidateNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }

        private class OrderByHealt : IComparer<IEnemy>
        {
            public int Compare(IEnemy x, IEnemy y)
            {
                if (x.Health < y.Health)
                {
                    return 1;
                }
                else if (x.Health > y.Health)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}

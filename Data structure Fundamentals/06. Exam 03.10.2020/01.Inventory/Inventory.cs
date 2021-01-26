namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        // O(1) amortized
        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        // O(1)
        public void Clear()
        {
            this.weapons.Clear();
        }

        // O(n)
        public bool Contains(IWeapon weapon)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Equals(weapon))
                {
                    return true;
                }
            }

            return false;
        }

        // O(n)
        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                //Equal(category) : ==
                if (this.weapons[i].Category == category)
                {
                    this.weapons[i].Ammunition = 0;
                }
            }
        }

        // O(n)
        public bool Fire(IWeapon weapon, int ammunition)
        {
            var current = GetById(weapon.Id);
            CheckWeaponExist(current);

            if (ammunition > current.Ammunition)
            {
                return false;
            }

            current.Ammunition -= ammunition;
            return true;
        }

        // O(n)
        public IWeapon GetById(int id)
        {
            IWeapon weapon = default;

            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id.Equals(id))
                {
                    weapon = this.weapons[i];
                    break;
                }
            }

            return weapon;
        }

        // O(n)
        public int Refill(IWeapon weapon, int ammunition)
        {
            var current = GetById(weapon.Id);
            CheckWeaponExist(current);

            if (current.Ammunition + ammunition > current.MaxCapacity)
            {
                current.Ammunition = current.MaxCapacity;
            }
            else
            {
                current.Ammunition += ammunition;
            }

            return current.Ammunition;
        }

        // O(n ^ 2)
        public IWeapon RemoveById(int id)
        {
            var current = GetById(id);
            CheckWeaponExist(current);
            // O(n)
            this.weapons.RemoveAt(id);

            return current;
        }

        // O(n ^ 2)
        public int RemoveHeavy()
        {
            int count = 0;
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Category == Category.Heavy)
                {
                    var toRemove = this.weapons[i];
                    // O(n)
                    this.weapons.Remove(toRemove);
                    i--;
                    count++;
                }
            }

            return count;
        }

        // O(n)
        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this.weapons);
        }

        // O(n)
        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> list = new List<IWeapon>();

            foreach (var weapon in this.weapons)
            {
                if ((int)weapon.Category >= (int)lower && (int)weapon.Category <= (int)upper)
                {
                    list.Add(weapon);
                }
            }

            return list;
        }

        // O(n ^ 2)
        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        { 
            int firstIndex = GetIndexElement(firstWeapon);
            int secondIndex = GetIndexElement(secondWeapon);
            if (firstWeapon.Category.CompareTo(secondWeapon.Category) == 0)
            {
                this.weapons[firstIndex] = secondWeapon;
                this.weapons[secondIndex] = firstWeapon;
            }
        }


        private int GetIndexElement(IWeapon firstWeapon)
        {
            int index = this.weapons.IndexOf(firstWeapon);

            if (index == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            return index;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                yield return this.weapons[i];
            }
        }

        private void CheckWeaponExist(IWeapon weapon)
        {
            if (weapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}

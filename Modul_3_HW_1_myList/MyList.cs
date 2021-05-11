using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_3_HW_1_myList
{
    public class MyList<T> : IReadOnlyCollection<T>
    {
        private const int DefaultCount = 4;
        private const int RateFactor = 2;

        private int _capasity;
        private T[] _array;

        public MyList(int capasity)
        {
            _capasity = capasity;
            _array = new T[_capasity];
        }

        public MyList()
            : this(DefaultCount)
        {
        }

        public int Capacity
        {
            get
            {
                return _capasity;
            }
            set
            {
                if (!ValidateSetCapasity(value))
                {
                    return;
                }

                _capasity = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (Count == Capacity)
            {
                var isSuccess = IncreaseArray();
                if (!isSuccess)
                {
                    return;
                }
            }

            AddItem(item);
        }

        public void AddRange(IReadOnlyCollection<T> array)
        {
            var capasity = Count + array.Count;

            if (capasity >= Capacity)
            {
                var isSuccsess = IncreaseArray(capasity);
                if (!isSuccsess)
                {
                    return;
                }
            }

            foreach (T item in array)
            {
                AddItem(item);
            }
        }

        public bool RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                return false;
            }

            var lastIndex = Count - 1;

            for (var i = 0; i < lastIndex; i++)
            {
                _array[i] = _array[i + 1];
            }

            _array[lastIndex] = default(T);
            Count--;
            return true;
        }

        public bool Remove(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_array[i].Equals(item))
                {
                    var isSuccess = RemoveAt(i);

                    if (!isSuccess)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(_array, comparer);
        }

        public IEnumerator GetEnumerator()
        {
            return GetGenericEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetGenericEnumerator();
        }

        private bool IncreaseArray(int? capacity = null)
        {
            var tempArray = _array;
            if (capacity == null)
            {
                _capasity = _capasity * RateFactor;
            }
            else
            {
                var isValidCapacity = ValidateSetCapasity(capacity.Value);
                if (!isValidCapacity)
                {
                    return false;
                }

                _capasity = capacity.Value;
            }

            _array = new T[Capacity];

            for (var i = 0; i < tempArray.Length; i++)
            {
                _array[i] = tempArray[i];
            }

            return true;
        }

        private bool ValidateSetCapasity(int capacity)
        {
            return capacity > Count;
        }

        private void AddItem(T item)
        {
            _array[Count] = item;
            Count++;
        }

        private IEnumerator<T> GetGenericEnumerator()
        {
            foreach (var item in _array)
            {
                if (!item.Equals(default(T)))
                {
                    yield return item;
                }
            }
        }
    }
}

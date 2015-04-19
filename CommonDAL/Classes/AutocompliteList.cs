using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonDAL.Classes
{
    public class AutocompliteList<T> : IEnumerable
    {
        private class SortComparer : IComparer<T>
        {
            private Func<T, string> KeyFunc { get; set; }

            public SortComparer(Func<T, string> keyFunc)
            {
                KeyFunc = keyFunc;
            }

            public int Compare(T x, T y)
            {
                return String.Compare(KeyFunc(x), KeyFunc(y), StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private List<T> _list;
        private Func<T, string> _keyFunc;
        private SortComparer _sortComparer;

        public AutocompliteList(IEnumerable<T> initialCollection, Func<T, string> keyFunc)
        {
            _keyFunc = keyFunc;
            _sortComparer = new SortComparer(keyFunc);
            _list = new List<T>(initialCollection);

            _list.Sort(_sortComparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            int indexToInsert = _list.BinarySearch(0, _list.Count, item, _sortComparer);

            //If there is no element with such key
            if (indexToInsert < 0)
            {
                _list.Insert(~indexToInsert, item);
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _list[index]; }
        }

        public List<T> GetAutocompliteList(string prefix, int count)
        {
            var result = new List<T>(count);
            var index = FindFirstAutocompliteIndex(prefix);

            //Get first "count" elements 
            for (;index < _list.Count && index < count;index++)
            {
                result.Add(_list[index]);
            }

            return result;
        }

        private int FindFirstAutocompliteIndex(string prefix)
        {
            int startIndex = 0;
            int endIndex = _list.Count - 1;

            while (startIndex <= endIndex)
            {
                int middlePosition = (startIndex + endIndex) / 2;

                T middleValue = this[middlePosition];
                var middleKey = _keyFunc(middleValue);

                //Compare
                if (IsPrefixOf(prefix, middleKey))
                {
                    return middlePosition;
                }

                int compareResult = String.Compare(prefix, middleKey, StringComparison.InvariantCultureIgnoreCase);

                switch (compareResult)
                {
                    case -1:
                        endIndex = middlePosition - 1;
                        break;
                    case 0:
                        return middlePosition;
                    case 1:
                        startIndex = middlePosition + 1;
                        break;
                }
            }

            return -1;
        }

        private bool IsPrefixOf(string prefix, string item)
        {
            if (prefix.Length > item.Length)
                return false;

            return !prefix.Where((t, i) => t != item[i]).Any();
        }
    }
}
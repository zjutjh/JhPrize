using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JhPrize.Core
{
    public static class Extensions
    {
        public static void UpdateCollection<T>(ICollection<T> collection, IEnumerable<T> other, EqualityComparer<T> equalityComparer) where T:IUpdatable<T>
        {
            List<T> otherCopy = other.ToList();
            List<T> sourceCopy = collection.ToList();
            foreach (var item in sourceCopy)
            {
                var take = other.TakeWhile((a) => equalityComparer.Equals(a, item));
                if (take.Any())
                {
                    item.Update(take.First());
                    foreach (var item2 in take)
                    {
                        otherCopy.Remove(item2);
                    }
                } else
                {
                    collection.Remove(item);
                }
            }
            foreach (var item in otherCopy)
            {
                collection.Add(item);
            }
        }

        
    }
}

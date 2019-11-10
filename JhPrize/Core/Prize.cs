using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhPrize.Core
{
    public class Prize : INotifyPropertyChanged, IUpdatable<Prize>
    {
        public class Comparer : EqualityComparer<Prize>
        {
            public override bool Equals(Prize x, Prize y)
            {
                return x.Title == y.Title;
            }

            public override int GetHashCode(Prize obj)
            {
                return obj.Title.GetHashCode();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string title;
        private string detail;
        private int acceptCount =10000;
        private int count =1000;
        private int capacity =1002131;

        public Prize()
        {
        }

        public Prize(string title, string detail, int acceptCount, int count, int capacity)
        {
            this.Title = title;
            this.Detail = detail;
            this.AcceptCount = acceptCount;
            this.Count = count;
            this.Capacity = capacity;
        }

        public string Title
        {
            get => title; set
            {
                title = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public string Detail
        {
            get => detail; set
            {
                detail = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Detail)));
            }
        }

        public int AcceptCount
        {
            get => acceptCount; set
            {
                acceptCount = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AcceptCount)));
            }
        }

        public int Count
        {
            get => count; set
            {
                count = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }

        public int Capacity
        {
            get => capacity; set
            {
                capacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Capacity)));
            }
        }

        public void Update(Prize other)
        {
            this.Title = other.Title;
            this.Detail = other.Detail;
            this.AcceptCount = other.AcceptCount;
            this.Count = other.Count;
            this.Capacity = other.Capacity;
        }
    }
}

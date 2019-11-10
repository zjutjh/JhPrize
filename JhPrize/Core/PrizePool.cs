using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JhPrize.Core
{
    public class PrizePool : INotifyPropertyChanged, IUpdatable<PrizePool>
    {
        public class Comparer : EqualityComparer<PrizePool>
        {
            public override bool Equals(PrizePool x, PrizePool y)
            {
                return x.Title == y.Title;
            }

            public override int GetHashCode(PrizePool obj)
            {
                return obj.Title.GetHashCode();
            }
        }

        private string title;
        private ObservableCollection<Prize> prizes = new ObservableCollection<Prize>();
        private int acceptCount;
        private int count;
        private int capacity;

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



        public PrizePool()
        {
            this.Prizes.CollectionChanged += Prizes_CollectionChanged;
        }

        private void Prizes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.AcceptCount = (from item in Prizes select item.AcceptCount).Sum();
            this.Count = (from item in Prizes select item.Count).Sum();
            this.Capacity = (from item in Prizes select item.Capacity).Sum();
        }

        public void Update(PrizePool other)
        {
            this.Title = other.title;
            Extensions.UpdateCollection<Prize>(Prizes, other.Prizes,new Prize.Comparer());
        }

        public PrizePool(string title, params Prize[] prizes) : this()
        {
            this.Title = title;
            foreach (var item in prizes)
            {
                this.Prizes.Add(item);
            }
        }

        public string Title
        {
            get => title; set
            {
                title = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        public ObservableCollection<Prize> Prizes
        {
            get => prizes; set
            {
                prizes = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prizes)));


            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

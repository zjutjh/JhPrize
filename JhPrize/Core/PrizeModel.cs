using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhPrize.Core
{
    public class PrizeModel
    {
        public int id;
        public string title;
        public string captain;
        public string content;
        public int capacity;
        public int count;
        public int accept_count;

        public Prize ToPrize()
        {
            return new Prize(
                this.captain, 
                this.content,
                this.accept_count,
                this.count,
                this.capacity
            );
        }
    }
}

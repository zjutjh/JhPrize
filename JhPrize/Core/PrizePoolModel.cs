using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhPrize.Core
{
    public class PrizePoolModel
    {
        public string title;
        public PrizeModel[] data;

        public PrizePool ToPrizePool()
        {
            return new PrizePool(
                this.title,
                this.data.Select((a) => a.ToPrize()).ToArray()
            );
        }
    }
}

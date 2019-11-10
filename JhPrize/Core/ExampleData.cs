using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JhPrize.Core
{
    public static class ExampleData
    {
        public static PrizePool[] GeneratePrizePools()
        {
            return new PrizePool[] {
                new PrizePool("完成毅行",
                    new Prize("一等奖", "星星*10\n漆橙轰趴五折券*1", 0, 0, 1),
                    new Prize("二等奖", "星星*8\n健身卡一个月*1", 1, 4, 12),
                    new Prize("三等奖", "星星*7\n漆橙轰趴七折券*5", 2, 3, 8)
                    ),
                new PrizePool("未完成毅行",
                    new Prize("一等奖", "星星*5\nPokeman Go光碟*1",0,0,2),
                    new Prize("二等奖", "星星*2\n京东100元购物券*1",2,3,10),
                    new Prize("三等奖", "星星*1\n宝可梦Surface Go背部贴膜*1",0,8,40)
                    ) 
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFund.Model
{
    public class Share
    {
        public string ShareId { get; set; }
        public string Symbol { get; set; }
        public decimal SharePrice { get; set; }

        public Share()
        {

        }

    }
}

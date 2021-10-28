using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedMohammedRabieAbdElwhab.BL.Classes
{
    public class EmailActionTempModel
    {
        public string Tilte { get; set; }
        public string bodyMsg { get; set; }
        public string btnlinkValue { get; set; }
        public Uri callbackUrl { get; set; }
    }
}

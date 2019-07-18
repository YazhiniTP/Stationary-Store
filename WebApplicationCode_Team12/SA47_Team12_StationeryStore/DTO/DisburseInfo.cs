using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA47_Team12_StationeryStore
{
    public class DisburseInfo
    {
        public int DisbursementID { get; set; }
        public string DepartmentDes { get; set; }
        public string ItemID { get; set; }
        public string ItemDes { get; set; }
        public int? DisbursedQty { get; set; }
    }
}

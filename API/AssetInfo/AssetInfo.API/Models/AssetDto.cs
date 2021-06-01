using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Models
{
    public class AssetDto
    {
        public string MachineType { get; set; }
        public string AssetName { get; set; }
        public string AssetSeriesNo { get; set; }
    }
}

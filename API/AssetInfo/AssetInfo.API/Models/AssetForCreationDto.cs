using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Models
{
    public class AssetForCreationDto
    {
        [Required]
        public string MachineType { get; set; }
        [Required]
        public string AssetName { get; set; }
        [Required]
        public string AssetSeriesNo { get; set; }
    }
}

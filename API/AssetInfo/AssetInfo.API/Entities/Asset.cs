using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Entities
{
    /// <summary>
    /// An asset with id, machine type, asset name and asset series field
    /// </summary>
    public class Asset : IEquatable<Asset>
    {
        /// <summary>
        /// The id of the asset
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The machine type uses given asset and series no of that asset 
        /// </summary>
        //[Required]
        public string MachineType { get; set; }

        /// <summary>
        /// The name of asset
        /// </summary>
        [Required]
        public string AssetName { get; set; }

        /// <summary>
        /// Series no of that asset     
        /// </summary>
        [Required]
        public string AssetSeriesNo { get; set; }

        public bool Equals(Asset asset)
        {
            if (asset == null) return false;

            return (
                this.AssetName.Equals(asset.AssetName)
                );
        }
    }
}

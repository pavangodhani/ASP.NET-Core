using AssetInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Helpers
{
    public class AssetNameComparer : IEqualityComparer<Asset>
    {
        public bool Equals(Asset x, Asset y)
        {
            return x.AssetName == y.AssetName;
        }

        public int GetHashCode(Asset asset)
        {
            int hashAssetName = asset.AssetName.GetHashCode();

            return hashAssetName;
        }
    }
}

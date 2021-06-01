using AssetInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Helpers
{
    public class AssetComparer : IEqualityComparer<Asset>
    {
        public bool Equals(Asset x, Asset y)
        {
            return x.AssetName == y.AssetName && x.AssetSeriesNo == y.AssetSeriesNo;
        }

        public int GetHashCode([DisallowNull] Asset obj)
        {
            return 0;
        }
    }
}

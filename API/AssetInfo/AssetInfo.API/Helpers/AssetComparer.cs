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
        public bool Equals([AllowNull] Asset x, [AllowNull] Asset y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Asset obj)
        {
            throw new NotImplementedException();
        }
    }
}

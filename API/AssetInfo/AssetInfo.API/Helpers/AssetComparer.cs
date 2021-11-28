using AssetInfo.API.Entities;
using AssetInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Helpers
{
    public class AssetComparer : IEqualityComparer<AssetNameAndSeriesNo>
    {
        public bool Equals(AssetNameAndSeriesNo x, AssetNameAndSeriesNo y)
        {
            return x.AssetName == y.AssetName && x.AssetSeriesNo == y.AssetSeriesNo;
        }

        public int GetHashCode(AssetNameAndSeriesNo obj)
        {
            //string asset = obj.AssetName + " " + obj.AssetSeriesNo;

            int hashCode = obj.AssetName.GetHashCode() + obj.AssetSeriesNo.GetHashCode();
            return hashCode;
        }
    }
}

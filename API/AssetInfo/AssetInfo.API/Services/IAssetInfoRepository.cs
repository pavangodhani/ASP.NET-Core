using AssetInfo.API.Entities;
using System.Collections.Generic;

namespace AssetInfo.API.Services
{
    public interface IAssetInfoRepository
    {
        public Asset GetAsset(int id);
        public IEnumerable<Asset> GetAssets();
        public IEnumerable<Asset> GetAssetsForMachineType(string machineType);
        public IEnumerable<Asset> GetAssetsWithLatestSeriesNumber();
        public bool AssetExists(Asset asset);
        public void AddAsset(Asset asset);
        public void RemoveAsset(Asset asset);
        public void UpdateAsset(Asset asset);
        public bool Save();
    }
}

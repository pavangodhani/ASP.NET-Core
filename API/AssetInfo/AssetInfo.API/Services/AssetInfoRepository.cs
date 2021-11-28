using AssetInfo.API.DbContexts;
using AssetInfo.API.Entities;
using AssetInfo.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetInfo.API.Services
{
    public class AssetInfoRepository : IAssetInfoRepository
    {
        public AssetInfoContext _context;

        public AssetInfoRepository(AssetInfoContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public IEnumerable<Asset> GetAssets()
        {
            return _context.Assets.ToList<Asset>();
        }

        public IEnumerable<Asset> GetAssetsForMachineType(string machineType)
        {
            return _context.Assets.Where(
                a => a.MachineType == machineType
                ).ToList<Asset>();
        }

        public Asset GetAsset(int id)
        {
            return _context.Assets.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Asset> GetAssetsWithLatestSeriesNumber()
        {
            AssetNameComparer assetComparer = new AssetNameComparer();

            return _context.Assets.OrderByDescending(
                a => a.AssetSeriesNo
                ).ToHashSet<Asset>(assetComparer);
        }

        public void AddAsset(Asset asset)
        {
            _context.Assets.Add(asset);
        }

        public bool AssetExists(Asset asset)
        {
            return _context.Assets.Any(a =>
                a.AssetName == asset.AssetName &&
                a.AssetSeriesNo == asset.AssetSeriesNo &&
                a.MachineType == asset.MachineType
                );
        }

        public void RemoveAsset(Asset asset)
        {
            _context.Assets.Remove(asset);
        }

        public void UpdateAsset(Asset asset)
        {
            //
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

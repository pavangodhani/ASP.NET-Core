using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Services
{
    public class DownloadService : IDownloadService
    {
        public string DownloadFile()
        {
            throw new NotImplementedException();
        }

        public string GetZipPath()
        {
            string localizationDbSeedFolderPath =
                Path.Combine(Directory.GetCurrentDirectory(), "LocalizationDbSeed");
            string zipFilePath =
                Path.Combine(Path.GetTempPath(), "LocalizationDbSeed.zip");

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            ZipFile.CreateFromDirectory(localizationDbSeedFolderPath, zipFilePath);

            return zipFilePath;
        }

        //private async Task<StorageFolder> SelectFolderDirectory()
        //{
        //    var folderPicker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Desktop };
        //    folderPicker.FileTypeFilter.Add("*");

        //    StorageFolder pickedFolder = await folderPicker.PickSingleFolderAsync();
        //    if (pickedFolder != null)
        //    {
        //        StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", pickedFolder);
        //    }

        //    return pickedFolder;
        //}
    }
}

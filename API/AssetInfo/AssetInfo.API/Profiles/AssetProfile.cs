using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Entities.Asset, Models.MachineTypesForAssetDto>();
            CreateMap<Entities.Asset, Models.AssetForMachineTypeDto>();

            CreateMap<Models.AssetForMachineTypeDto, Entities.Asset>();
            CreateMap<Models.AssetDto, Entities.Asset>();
            CreateMap<Entities.Asset, Models.AssetDto>();
            CreateMap<Models.AssetForCreationDto, Entities.Asset>();
            CreateMap<Models.AssetForUpdateDto, Entities.Asset>();
            CreateMap<Entities.Asset, Models.AssetForUpdateDto>();
        }
    }
}

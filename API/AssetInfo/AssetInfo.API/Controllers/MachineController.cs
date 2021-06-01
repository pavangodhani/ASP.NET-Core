using AssetInfo.API.Models;
using AssetInfo.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetInfo.API.Controllers
{
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json", "application/xml")]
    [Route("api/machines")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineInfoRepository _machineInfoRepository;

        public readonly IMapper _mapper;

        public MachineController(IMachineInfoRepository machineInfoRepository, IMapper mapper)
        {
            _machineInfoRepository = machineInfoRepository ??
                throw new ArgumentNullException(nameof(machineInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all machineTypes
        /// </summary>
        /// <returns>
        /// All machineTypes which are available in database
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetMachineTypes()
        {
            var machineTypesFromRepo = _machineInfoRepository.GetMachineTypes();

            if(machineTypesFromRepo.Count() == 0)
            {
                return NotFound();
            }

            return Ok(machineTypesFromRepo);
        }

        /// <summary>
        /// Get the list of all machine types with series no of asset for given asset
        /// </summary>
        /// <param name="assetName">The name of the asset</param>
        /// <returns>Return list of all machineType with series no of given asset</returns>      
        [HttpGet("{assetName}")]
        public ActionResult<IEnumerable<MachineTypesForAssetDto>> GetMachineTypesForSpecificAsset(string assetName)
        {
            var machineTypesFromRepoForAsset = 
                _machineInfoRepository.GetMachineTypesForAsset(assetName);

            if(machineTypesFromRepoForAsset.Count() == 0)
            {
                return NotFound();
            }

            return Ok(
                _mapper.Map<IEnumerable<MachineTypesForAssetDto>>(machineTypesFromRepoForAsset)
                );
        }

        /// <summary>
        /// Get the machine types which are using the latest series of all the assets that it uses
        /// </summary>
        /// <returns>The list of the machine types</returns>
        [HttpGet("latest")]
        public ActionResult<IEnumerable<string>> GetMachineTypesWhichUseLatestAsse()
        {
            var machineTypesWhichUseLatestAsse = 
                _machineInfoRepository.GetMachineWhichUseLatestAssets();

            if(machineTypesWhichUseLatestAsse.Count() == 0)
            {
                return NotFound();
            }

            return Ok(machineTypesWhichUseLatestAsse);
        }
    }
}

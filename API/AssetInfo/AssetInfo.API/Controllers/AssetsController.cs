using AssetInfo.API.Entities;
using AssetInfo.API.Models;
using AssetInfo.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetInfo.API.Controllers
{
    
    [Produces("application/json", "application/xml")]
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetInfoRepository _assetInfoRepository;

        public readonly IMapper _mapper;

        public AssetsController(IAssetInfoRepository assetInfoRepository, IMapper mapper)
        {
            _assetInfoRepository = assetInfoRepository ??
                throw new ArgumentNullException(nameof(assetInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all assets or enter a machine type for get the list of assets for given machine type
        /// </summary>
        /// <param name="machineType">The type of the machine</param>
        /// <returns>Return list of assets</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]        
        public ActionResult<IEnumerable<Asset>> GetAssets([FromQuery]string machineType)
        {
            if (machineType == null)
            {
                var assetsFromRepo = _assetInfoRepository.GetAssets();

                if(assetsFromRepo.Count() == 0)
                {
                    NotFound();
                }

                return Ok(assetsFromRepo);
            }

            var assetsFromRepoForMachineType =
                _assetInfoRepository.GetAssetsForMachineType(machineType);

            if (assetsFromRepoForMachineType.Count() == 0)
            {
                return NotFound();
            }

            return Ok(
                _mapper.Map<IEnumerable<AssetForMachineTypeDto>>(assetsFromRepoForMachineType)
                );
        }

        /// <summary>
        /// Get asset by id 
        /// </summary>
        /// <param name="id">The id of the asset</param>
        /// <returns>Return the requested asset</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}", Name = "GetAsset")]
        public ActionResult<Asset> GetAsset(int id)
        {
            var assetFromRepo = _assetInfoRepository.GetAsset(id);

            if(assetFromRepo == null)
            {
                return NotFound();
            }

            return Ok(assetFromRepo);
        }

        /// <summary>
        /// Get the all asset with it's latest series no
        /// </summary>
        /// <returns>Return the asset with latest series no</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("latest")]
        public ActionResult<IEnumerable<AssetForMachineTypeDto>> GetAssetsWithLatestSeriesNo()
        {
            var assetsWithLatestSeriesNo = _assetInfoRepository.GetAssetsWithLatestSeriesNo();

            if(assetsWithLatestSeriesNo.Count() == 0)
            {
                return NotFound();
            }

            return Ok(
                _mapper.Map<IEnumerable<AssetForMachineTypeDto>>(assetsWithLatestSeriesNo)
                );
        }

        /// <summary>
        /// Create a new asset
        /// </summary>
        /// <param name="asset">The new asset</param>
        /// <returns>Return the new cretaed asset</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Asset> CreateAsset([FromBody]AssetForCreationDto asset)
        {
            var finalAsset = _mapper.Map<Entities.Asset>(asset);

            if (_assetInfoRepository.AssetExists(finalAsset))
            {
                return BadRequest();
            }

            _assetInfoRepository.AddAsset(finalAsset);
            _assetInfoRepository.Save();

            return CreatedAtRoute("GetAsset", new {id = finalAsset.Id}, finalAsset);
        }

        /// <summary>
        /// Delete asset by id
        /// </summary>
        /// <param name="id">The id of the asset</param>
        /// <returns>Delete the requested asset</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult DeleteAsset(int id)
        {
            var assetFromRepo = _assetInfoRepository.GetAsset(id);

            if(assetFromRepo == null)
            {
                return NotFound();
            }

            _assetInfoRepository.RemoveAsset(assetFromRepo);
            _assetInfoRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Update an asset
        /// </summary>
        /// <param name="id">The of the asset</param>
        /// <param name="asset">updated asset</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public IActionResult UpdateAsset(int id, [FromBody]AssetForUpdateDto asset)
        {
            var assetFromRepo = _assetInfoRepository.GetAsset(id);

            if(assetFromRepo == null)
            {
                var finalAsset = _mapper.Map<Entities.Asset>(asset);

                if (_assetInfoRepository.AssetExists(finalAsset))
                {
                    return BadRequest();
                }

                _assetInfoRepository.AddAsset(finalAsset);
                _assetInfoRepository.Save();


                return CreatedAtRoute("GetAsset", new { id = finalAsset.Id }, finalAsset);
            }

            _mapper.Map(asset, assetFromRepo);

            _assetInfoRepository.UpdateAsset(assetFromRepo);
            _assetInfoRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Partially update an asset
        /// </summary>
        /// <param name="id">The id of the asset which you want to get</param>
        /// <param name="patchDocument">The set of opration to apply for the asset</param>
        /// <returns>An ActionResult of type Asset</returns>
        /// <remarks>
        /// Sample request (This request updates the Asset's **Machine Type**)    
        ///     PATCH /api/assets/id    
        ///     [    
        ///         {     
        ///             "op": "replace",    
        ///             "path": "/machinetype",     
        ///             "value": "C100"    
        ///         }     
        ///     ]    
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id}")]
        public ActionResult<Asset> PartiallyUpdateAsset(int id, 
            [FromBody]JsonPatchDocument<AssetForUpdateDto> patchDocument)
        {
            var assetFromRepo = _assetInfoRepository.GetAsset(id);

            if(assetFromRepo == null)
            {
                var assetDto = new AssetForUpdateDto();

                patchDocument.ApplyTo(assetDto, ModelState);

                if (!TryValidateModel(assetDto))
                {
                    return ValidationProblem(ModelState);
                }

                var assetToAdd = _mapper.Map<Entities.Asset>(assetDto);

                _assetInfoRepository.AddAsset(assetToAdd);
                _assetInfoRepository.Save();

                return CreatedAtRoute("GetAsset", new { id = assetToAdd.Id}, assetToAdd);
            }

            var assetToPatch = _mapper.Map<Models.AssetForUpdateDto>(assetFromRepo);

            patchDocument.ApplyTo(assetToPatch, ModelState);

            if(!TryValidateModel(assetToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(assetToPatch, assetFromRepo);

            _assetInfoRepository.UpdateAsset(assetFromRepo);
            _assetInfoRepository.Save();

            return NoContent();
        }
    }
}

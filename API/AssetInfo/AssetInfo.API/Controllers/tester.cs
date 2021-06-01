using AssetInfo.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Controllers
{
    [Route("api/tester")]
    [ApiController]
    public class tester : ControllerBase
    {
        private readonly IMachineInfoRepository _assetInfoRepository;

        public readonly IMapper _mapper;

        public tester(IMachineInfoRepository assetInfoRepository, IMapper mapper)
        {
            _assetInfoRepository = assetInfoRepository ??
                throw new ArgumentNullException(nameof(assetInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        public IActionResult GetDemo()
        {
            return Ok( _assetInfoRepository.get);
        }

        [HttpGet("c")]
        public IActionResult GetM()
        {
            return Ok();
        }
    }
}

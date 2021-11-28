using AssetInfo.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace AssetInfo.API.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/download")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadService _machineInfoRepository;

        public readonly IMapper _mapper;

        public DownloadController(IDownloadService machineInfoRepository, IMapper mapper)
        {
            _machineInfoRepository = machineInfoRepository ??
                throw new ArgumentNullException(nameof(machineInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult DownloadFile()
        {
            var zipPath = _machineInfoRepository.DownloadFile();

            return PhysicalFile(zipPath, MimeTypes.GetMimeType(zipPath), Path.GetFileName(zipPath));
        }
    }
}

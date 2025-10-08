using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MotoRental.Application.DTOs;
using MotoRental.Application.Handlers;
using MotoRental.Domain.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;


namespace MotoRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RidersController : ControllerBase
    {
        private readonly RegisterRiderCommandHandler _registerHandler;
        private readonly UploadCnhImageCommandHandler _uploadHandler;
        private readonly IRiderRepository _riderRepository;


        public RidersController(RegisterRiderCommandHandler registerHandler, UploadCnhImageCommandHandler uploadHandler, IRiderRepository riderRepository)
        {
            _registerHandler = registerHandler;
            _uploadHandler = uploadHandler;
            _riderRepository = riderRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RiderCreateDto dto)
        {
            var result = await _registerHandler.HandleAsync(dto);
            return Created($"/api/riders/{result.Id}", result);
        }


        /// <summary>
        /// Upload CNH image as base64 in request body: { "imagem_cnh": "<base64>" }
        /// </summary>
        [HttpPost("{id:guid}/cnh")]
        public async Task<IActionResult> UploadCnh(Guid id, [FromBody] UploadCnhImageRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ImagemCnh))
                return BadRequest(new { message = "imagem_cnh is required in the request body (base64 string)." });


            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(request.ImagemCnh);
            }
            catch
            {
                return BadRequest(new { message = "Invalid base64 content." });
            }


            //var (ext, contentType) = ImageHelper.DetectImageType(bytes);
            //var fileName = $"{Guid.NewGuid()}{ext}";


            //using var ms = new MemoryStream(bytes);
            //var url = await _uploadHandler.HandleAsync(id, ms, fileName, contentType);

            var url = string.Empty;


            return Created($"/api/riders/{id}/cnh", new { url });
        }
    }


    public record UploadCnhImageRequest(string ImagemCnh);
}
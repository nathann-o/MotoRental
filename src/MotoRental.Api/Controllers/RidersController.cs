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
    [Route("entregadores")]
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
            try
            {
                var result = await _registerHandler.HandleAsync(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //NOT IMPLEMENTED YET
        /// <summary>
        /// Upload CNH image as base64 in request body: { "imagem_cnh": "<base64>" }
        /// </summary>
        [HttpPost("{id:guid}/cnh")]
        public IActionResult UploadCnh(Guid id, [FromBody] UploadCnhImageRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.imagem_cnh))
                return BadRequest(new { message = "imagem_cnh is required in the request body (base64 string)." });


            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(request.imagem_cnh);
            }
            catch
            {
                return BadRequest(new { message = "Invalid base64 content." });
            }


            //var (ext, contentType) = ImageHelper.DetectImageType(bytes);
            //var fileName = $"{Guid.NewGuid()}{ext}";


            //using var ms = new MemoryStream(bytes);
            //var url = await _uploadHandler.HandleAsync(id, ms, fileName, contentType);


            return Created();
        }
    }


    public record UploadCnhImageRequest(string imagem_cnh);
}
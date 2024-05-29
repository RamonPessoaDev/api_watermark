using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using ImageWatermarkApi.Models;
using System;
using System.IO;

namespace ImageWatermarkApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        [HttpPost("watermark")]
        public IActionResult ApplyWatermark([FromBody] WatermarkRequestDto request)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(request.Base64Image);
                using var ms = new MemoryStream(imageBytes);
                using var img = SKBitmap.Decode(ms);

                using var canvas = new SKCanvas(img);
                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 20
                };

                canvas.DrawText(request.WatermarkText, request.XPosition, request.YPosition, paint);
                canvas.Flush();

                using var memoryStream = new MemoryStream();
                using var skImage = SKImage.FromBitmap(img);
                using var data = skImage.Encode(SKEncodedImageFormat.Png, 100);
                data.SaveTo(memoryStream);

                var result = memoryStream.ToArray();
                return Ok(Convert.ToBase64String(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error applying watermark: {ex.Message}");
            }
        }
    }
}

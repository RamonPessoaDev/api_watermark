namespace ImageWatermarkApi.Models
{
    public class WatermarkRequestDto
    {
        public string Base64Image { get; set; }
        public string WatermarkText { get; set; }
        public float XPosition { get; set; }
        public float YPosition { get; set; }
    }
}

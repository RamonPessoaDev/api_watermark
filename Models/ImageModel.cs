using System;
using System.IO;

namespace ImageWatermarkApi.Models
{
    public class ImageModel
    {
        public byte[] Data { get; set; }

        public static explicit operator ImageModel(byte[] data)
        {
            return new ImageModel { Data = data };
        }

        public static explicit operator byte[](ImageModel model)
        {
            return model.Data;
        }
    }
}
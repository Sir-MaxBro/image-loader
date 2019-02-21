using System.Text;

namespace ImageLoader.Business.Models
{
    public class ImageInfoModel
    {
        public ImageInfoModel(string imageUrl, byte[] bytes)
        {
            this.Bytes = bytes;
            this.ImageUrl = imageUrl;
        }

        public int ByteSize => this.Bytes != null ? this.Bytes.Length : default(int);

        public int EndThreadId { get; set; }

        public int StartThreadId { get; set; }

        public long LoadedTime { get; set; }

        public byte[] Bytes { get; }

        public string ImageUrl { get; }

        public override string ToString()
        {
            var message = new StringBuilder()
                .AppendLine($"Image url: {this.ImageUrl}")
                .AppendLine($"Byte size: {this.ByteSize}")
                .AppendLine($"StartThreadId: {this.StartThreadId}")
                .AppendLine($"EndThreadId: {this.EndThreadId}")
                .AppendLine($"LoadedTime: {this.LoadedTime} ms");

            return message.ToString();
        }
    }
}

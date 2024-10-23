
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Dtos
{
    public record Response
    {
        /// <summary>
        /// Trạng thái thành công
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>/
        public object? Data { get; set; }
        /// <summary>
        /// Thông báo
        /// </summary>/
        public string? Messages { get; set; }
        /// <summary>
        /// Trạng thái HTTP
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Danh sách lỗi
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}

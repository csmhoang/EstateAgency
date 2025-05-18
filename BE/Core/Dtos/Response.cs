using System.Text.Json;

namespace Core;

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

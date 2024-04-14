using System.Text;

namespace PalpiteFC.Api.Extensions;

public static class HttpRequestExtension
{
    public static async Task<string?> GetRequestBody(this HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);

        var body = await reader.ReadToEndAsync();

        request.Body.Seek(0, SeekOrigin.Begin);

        return body;
    }
}
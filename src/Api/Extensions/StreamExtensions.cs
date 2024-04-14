namespace PalpiteFC.Api.Extensions;

public static class StreamExtensions
{
    public static async Task<string> GetString(this Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);

        var content = await new StreamReader(stream).ReadToEndAsync();

        stream.Seek(0, SeekOrigin.Begin);

        return content;
    }
}

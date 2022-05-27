using Microsoft.AspNetCore.Http;

namespace ProDeal.Application.Extensions
{
    public static class FileExtensions
    {
        public static async Task<List<string>> ReadAsList(this IFormFile file)
        {
            var content = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    content.Add(await reader.ReadLineAsync());
                }
            }

            return content;
        }
    }
}

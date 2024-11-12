namespace VethubLanding.Interfaces
{
    public interface IRestAPIService
    {
        Task<T> GetResponse<T>(string url, string? auth = null)
            where T : class;
        Task<T> PostResponse<T>(string url, string obj, string? auth = null)
            where T : class;
        Task<string> PostResponseFile(string url, IFormFile file, string? auth = null);
        Task<T> PutResponse<T>(string url, int id, string obj, string? auth = null)
            where T : class;
        Task<T> DeleteResponse<T>(string url, int id, string? auth = null)
            where T : class;
    }
}

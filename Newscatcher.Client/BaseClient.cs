using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Newscatcher.Client.Contracts.Abstract;
using Newscatcher.Client.SerializationContext;

namespace Newscatcher.Client;

public class BaseClient
{
    private readonly HttpClient client;
    private const string ApplicationJson = "application/json";

    protected BaseClient(Uri baseUrl, HttpClient client, string apiKey)
    {
        this.client = client;
        client.BaseAddress = baseUrl.AbsoluteUri.EndsWith("/") ? baseUrl :  new Uri(baseUrl + "/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
        client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "newscatcher.p.rapidapi.com");
    }
    
    protected async Task<TResponse> Get<TResponse>(string requestUri, CancellationToken cancellationToken) where TResponse : class
    {
        var response = await client.GetAsync(requestUri, cancellationToken);
        if (response.StatusCode is HttpStatusCode.TooManyRequests)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        var stringContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return (TResponse)JsonSerializer.Deserialize(stringContent, typeof(TResponse), NewsCatcherClientMetadataContext.Default);
    }
    
    protected async IAsyncEnumerable<TResponse> GetByPagination<TResponse>(string requestUri, int size, [EnumeratorCancellation] CancellationToken cancellationToken = default) where TResponse : class, IPageable
    {
        var currentPage = 1;
        var totalPages = 1;
        do
        {
            cancellationToken.ThrowIfCancellationRequested();
            var uriWithPagination = $"{requestUri}&lang=ru&page={currentPage}&page_size={size}";
            var response = await client.GetAsync(uriWithPagination, cancellationToken);
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                yield break;
            }
            if (!response.IsSuccessStatusCode)
            {
                yield break;
            }
            response.EnsureSuccessStatusCode();
            var stringContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var jsonResponse = (TResponse)JsonSerializer.Deserialize(stringContent, typeof(TResponse), NewsCatcherClientMetadataContext.Default);
            totalPages = jsonResponse.TotalPages;
            currentPage++;
            yield return jsonResponse;
        } while (currentPage < totalPages);
    }
}
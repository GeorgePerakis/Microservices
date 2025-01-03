using Services.Inventory.Dtos;

namespace Services.Inventory.Clients
{
    public class CatalogClient
    {
        private readonly HttpClient httpClient;

        public CatalogClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CatalogItemDto>> GetCatalogItems()
        {
            try
            {
                var response = await httpClient.GetAsync("/items");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>();
                }
                else
                {
                    throw new HttpRequestException($"Error retrieving catalog items: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to get catalog items.", ex);
            }
        }
    }
}
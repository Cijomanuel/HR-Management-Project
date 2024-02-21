namespace HrManagementSystem.Core.HttpClients
{
    using HrManagementSystem.Core.Data.Entities.Common;
    using IdentityServer4.Stores.Serialization;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Text;

    namespace WebApplicationCore1.HttpClients
    {
        public class GenericHttpClient : IGenericHttpClient
        {
            private readonly IConfiguration configuration;
            private readonly HttpClient client;
            private readonly ApiClientConfiguration clientConfiguration;

            public GenericHttpClient(IConfiguration configuration, HttpClient client, ApiClientConfiguration clientConfiguration)
            {
                this.configuration = configuration;
                this.client = client;
                this.clientConfiguration = clientConfiguration;
                client.BaseAddress = new System.Uri(clientConfiguration.BaseApiUri);
            }
            public async Task<TResponse> DeletAsync<TResponse>(string address)
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{address}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<TResponse>("success");
                    }
                    return JsonConvert.DeserializeObject<TResponse>("fail");
                }
            }

            public async Task<T> GetAsync<T>(string address)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{address}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //var username = clientConfiguration.BaseUserName;
                //var password = clientConfiguration.BasePassword;
                //string encoded = System.Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

                //request.Headers.Add("Authentication", "Basic" + encoded);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientConfiguration.BaseUserName}:{clientConfiguration.BasePassword}")));

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<T>(result);

                        return model;
                    }
                    throw new Exception("Api not returned");
                }

            }

            public async Task<T> GetWithId<T>(string address)
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"{address}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientConfiguration.BaseUserName}:{clientConfiguration.BasePassword}")));
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var model = JsonConvert.DeserializeObject<T>(result);
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            public async Task<T> GetWithId<T>(string address, dynamic dynamicRequest)
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"{address}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var model = JsonConvert.DeserializeObject<T>(result);
                        return model;
                    }
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            public async Task<T> PostAsync<T>(string address, dynamic dynamicRequest)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{address}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientConfiguration.BaseUserName}:{clientConfiguration.BasePassword}")));
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(result, new ClaimConverter());
                    }
                    return JsonConvert.DeserializeObject<T>("fail");

                }
            }

            public async Task<T> PutAsync<T>(string address, dynamic dynamicRequest)
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"{address}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                   Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientConfiguration.BaseUserName}:{clientConfiguration.BasePassword}")));
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {

                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    return JsonConvert.DeserializeObject<T>("fail");
                }
            }
        }
    }

}

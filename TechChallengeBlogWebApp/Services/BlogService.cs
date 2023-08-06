﻿using Microsoft.Extensions.Options;
using System.Text.Json;
using TechChallengeBlogWebApp.Models;
using TechChallengeBlogWebApp.Util;

#nullable disable

namespace TechChallengeBlogWebApp.Services
{
    public class BlogService
    {
        private readonly HttpClient _httpClient;

        public ApiBlogConfig ApiBlogConfig { get; }

        public BlogService(HttpClient httpClient, IOptions<ApiBlogConfig> apiBlogConfig)
        {
            _httpClient = httpClient;
            ApiBlogConfig = apiBlogConfig.Value;
        }

        private async Task AutenticarJwtAsync()
        {
            HttpResponseMessage resposta = await _httpClient.PostAsync($"{ApiBlogConfig.GerarTokenAsync}{ApiBlogConfig.ChaveSeguranca}", null);

            TokenResponseModel token;

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json = await resposta.Content.ReadAsStringAsync();
                    token = JsonSerializer.Deserialize<TokenResponseModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    break;
                default:
                    token = new();
                    break;
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        }

        public async Task<List<NoticiaModel>> PesquisarTodasNoticiasAsync()
        {
            await AutenticarJwtAsync();

            HttpResponseMessage resposta = await _httpClient.GetAsync(ApiBlogConfig.PesquisarTodasNoticias);

            switch (resposta.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    string json = await resposta.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<NoticiaModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                default:
                    return new();
            }
        }
    }
}

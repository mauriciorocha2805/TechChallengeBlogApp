using App.Blog.Domain.Entities;
using IntegrationTests.Config;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using TechChallengeBlogWebApi;
using Xunit;

namespace IntegrationTests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class NoticiaApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _textFixture;
        public NoticiaApiTests(IntegrationTestsFixture<StartupApiTests> textFixture)
        {
            _textFixture = textFixture;
        }

        [Fact(DisplayName = "Cadastrar uma notícia")]
        [Trait("Categoria", "Integração API - Notícia")]
        public async Task Testa_Cadastrar_Noticia()
        {
            //arrange
            var noticia = new Noticia
            {
                Titulo = "Notícia de Integração API",
                Conteudo = "Conteúdo da Notícia de Integração API",
                Autor = "Usuário Integração API",
                DataPublicacao = DateTime.Now
            };

            //act
            var postResponse = await _textFixture.Client.PostAsJsonAsync("/api/BlogNoticias", noticia);
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Editar uma notícia")]
        [Trait("Categoria", "Integração API - Notícia")]
        public async Task Testa_Editar_Noticia()
        {
            //arrange
            var noticia = new Noticia
            {
                Id = 5,
                Titulo = "Notícia de Integração API - Alterada",
                Conteudo = "Conteúdo da Notícia de Integração API",
                Autor = "Usuário Integração API",
                DataPublicacao = DateTime.Now
            };

            //act
            var postResponse = await _textFixture.Client.PutAsJsonAsync("/api/BlogNoticias", noticia);
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Obter todas as Notícias")]
        [Trait("Categoria", "Integração API - Notícia")]
        public async Task Testa_Obter_Todas_Noticias()
        {
            //act
            var postResponse = await _textFixture.Client.GetAsync("/api/BlogNoticias");
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Obter Notícia")]
        [Trait("Categoria", "Integração API - Notícia")]
        public async Task Testa_Obter_Noticia()
        {
            //act
            var postResponse = await _textFixture.Client.GetAsync("/api/BlogNoticias/5");
            postResponse.EnsureSuccessStatusCode();
        }
    }
}

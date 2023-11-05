using IntegrationTests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallengeBlogWebApp;
using Xunit;

namespace IntegrationTests
{
    [Collection(nameof(IntegrationWebAppTestsFixtureCollection))]
    public class NoticiaWebTests
    {
        private readonly IntegrationTestsFixture<StartupWebAppTests> _textFixture;
        public NoticiaWebTests(IntegrationTestsFixture<StartupWebAppTests> textFixture)
        {
            _textFixture = textFixture;
        }

        [Fact(DisplayName = "Cadastrar uma notícia")]
        [Trait("Categoria", "Integração Web - Notícia")]
        public async Task Testa_Cadastrar_Noticia()
        {
            //arrange
            var response = await _textFixture.Client.GetAsync("/Criar");
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            string token = _textFixture.ObterAntiForgeryToken(html);

            const string tituloNoticia = "Noticia de Integracao";
            var formData = new Dictionary<string, string>
            {
                {"Noticia.Titulo",tituloNoticia },
                {"Noticia.Conteudo","Conteúda da Notícia de Integração" },
                {"Noticia.Autor","Usuário Integração" },
                {"Noticia.DataPublicacao","2023-11-05" },
                {_textFixture.AntiForgeryFieldName, token}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Criar")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            //act
            response = await _textFixture.Client.SendAsync(postRequest);
            html = await response.Content.ReadAsStringAsync();
            Assert.Contains($"{tituloNoticia}", html);
        }

        [Fact(DisplayName = "Editar uma notícia")]
        [Trait("Categoria", "Integração Web - Notícia")]
        public async Task Testa_Editar_Noticia()
        {
            //arrange
            var response = await _textFixture.Client.GetAsync("/Editar?id=5");
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            string token = _textFixture.ObterAntiForgeryToken(html);

            const string tituloNoticia = "Noticia de Integracao Alterada";
            var formData = new Dictionary<string, string>
            {
                {"Noticia.Id", "5" },
                {"Noticia.Titulo",tituloNoticia },
                {"Noticia.Conteudo","Conteúdo Alterado" },
                {"Noticia.Autor","Usuário Integração" },
                {"Noticia.DataPublicacao","2023-11-05" },
                {_textFixture.AntiForgeryFieldName, token}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Editar?id=5")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            //act
            response = await _textFixture.Client.SendAsync(postRequest);
            html = await response.Content.ReadAsStringAsync();
            Assert.Contains($"{tituloNoticia}", html);
        }

        [Fact(DisplayName = "Obter todas as Notícias")]
        [Trait("Categoria", "Integração Web - Notícia")]
        public async Task Testa_Obter_Todas_Noticias()
        {
            var response = await _textFixture.Client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            const string tituloNoticia = "Noticia de Integracao Alterada";
            var html = await response.Content.ReadAsStringAsync();
            Assert.Contains($"{tituloNoticia}", html);
        }
    }
}

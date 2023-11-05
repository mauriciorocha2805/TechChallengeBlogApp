using Microsoft.AspNetCore.Mvc.Testing;
using TechChallengeBlogWebApi;
using TechChallengeBlogWebApp;
using Xunit;

namespace IntegrationTests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>>
    {

    }

    [CollectionDefinition(nameof(IntegrationWebAppTestsFixtureCollection))]
    public class IntegrationWebAppTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebAppTests>>
    {

    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestsFactory<TStartup> Factory;
        public HttpClient Client;
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public IntegrationTestsFixture()
        {
            Factory = new TestsFactory<TStartup>();

            var clientOptions = new WebApplicationFactoryClientOptions
            {

            };

            Client = Factory.CreateClient(clientOptions);
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = System.Text.RegularExpressions.Regex.Match(htmlBody, $"name=\"{AntiForgeryFieldName}\" type=\"hidden\" value=\"([^\"]+)\"");
            return requestVerificationTokenMatch.Success ? requestVerificationTokenMatch.Groups[1].Captures[0].Value : "";
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}

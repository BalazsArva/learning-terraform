using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningTerraform.Api.Constants;
using LearningTerraform.Api.Options;
using NUnit.Framework;

namespace LearningTerraform.Api.IntegrationTests.Middlewares
{
    [TestFixture]
    public class ApplicationVersionHeadersMiddlewareTests : IntegrationTestsBase
    {
        private const string DefaultCommitHash = "DefaultCommitHash";
        private const string DefaultAppVersion = "DefaultAppVersion";

        [Test]
        [Category(TestExecutionPolicies.PreDeployment)]
        [Category(TestExecutionPolicies.PostDeploymentNonProd)]
        [Category(TestExecutionPolicies.PostDeploymentProd)]
        public async Task InvokeAsync_HappyPath_IncludesVersionHeaders()
        {
            var client = CreateHttpClient();

            var response = await client.GetAsync("/health-check");

            var commitHashHeaderValues = response.Headers.GetValues(HeaderNames.CommitHash);
            var appVersionHeaderValues = response.Headers.GetValues(HeaderNames.Version);

            Assert.AreEqual(1, commitHashHeaderValues.Count());
            Assert.AreEqual(1, appVersionHeaderValues.Count());

            Assert.AreEqual(DefaultCommitHash, commitHashHeaderValues.First());
            Assert.AreEqual(DefaultAppVersion, appVersionHeaderValues.First());
        }

        protected override IDictionary<string, string> CreateAdditionalConfiguration()
        {
            return new Dictionary<string, string>
            {
                [$"{ApplicationVersionOptions.SectionName}:{nameof(ApplicationVersionOptions.Commit)}"] = DefaultCommitHash,
                [$"{ApplicationVersionOptions.SectionName}:{nameof(ApplicationVersionOptions.Version)}"] = DefaultAppVersion,
            };
        }
    }
}

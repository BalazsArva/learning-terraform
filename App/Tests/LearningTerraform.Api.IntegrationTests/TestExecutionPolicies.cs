namespace LearningTerraform.Api.IntegrationTests
{
    /// <summary>
    /// Contains well-known test categories that control in which cases integration tests should be executed.
    /// </summary>
    public static class TestExecutionPolicies
    {
        /// <summary>
        /// Indicates that the associated test should be executed as part of a pre-deployment test suite.
        /// </summary>
        public const string PreDeployment = "PreDeployment";

        /// <summary>
        /// Indicates that the associated test should be executed as part of a post-deployment test suite,
        /// but only against non-production environments. This may be the case when we need to create some
        /// test data that should not pollute the production environment.
        /// </summary>
        public const string PostDeploymentNonProd = "PostDeploymentNonProd";

        /// <summary>
        /// Incidates that the associated test should be executed as part of a post-deployment test suite,
        /// including production deployment test suites. This is applicable when the test neither requires
        /// any test data to be created nor does it rely on data that potentially contains PII.
        /// </summary>
        public const string PostDeploymentProd = "PostDeploymentProd";
    }
}

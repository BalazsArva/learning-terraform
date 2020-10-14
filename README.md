# learning-terraform

# Future plans

* [x] A sample, dockerized ASP.NET Core application which uses MS SQL, EF and DB migrations
* [x] Tests for the application:

  * [x] __Unit tests:__ because every application must have these. Especially pet projects.
  * [x] __Pre-deployment integration tests:__ Pre-deployment integration tests aim to verify the correctness of the application before deploying to any "real" environment. This can be done with ASP.NET Core in-memory hosting against dedicated dependencies (such as a DB instance reserved for this purpose). Dedicated dependencies are required to avoid any interference with other users or services. The goal of this stage is to catch as many failures as possible before committing to any more risky or complex stage, such as DB migration or application deployment. By running these tests before deployment and using a technique such as the ASP.NET Core in-memory testing framework, we enable the tests to verfiy special scenarios, such as simulate responses from other services that would otherwise be hard or impossible to force to happen consistently accross test runs, as it is often the case with third-party services.
  * [x] __Post-deployment smoke tests (all environments)__: these are meant to be run after a deployment. The goal is to verify that the application is alive and capable of serving requests. We must also ensure that we receive a response from the version we want (e.g. container could be started successfully, the runtime did not roll back to the previous version e.g. because of a startup failure). These should be run against a production as well, therefore these must not mutate data. A health check or a non-PII-containing endpoint can be a smoke test target. Once we start receiving responses from the newly deployed version, post-deployment integration tests can be run against non-production environments.
  * [x] __Post-deployment integration tests (lower environment):__ these are meant to be run after a deployment, but only on non-production environments. This test suite is usually a subset of/the same as the pre-deployment integration tests. The main difference is that the pre-deployment tests allow us to simulate a specific behavior in order to test the application as comprehensively as possible, as opposed to the post-deployment case, where we may not be able to run each scenario (for example, we are unable to force a dependency to behave in a specific way). This means that the post-deployment tests may exclude some tests from the pre-deployment suite, but it is a good way to ensure that when the application is hosted in a realistic environment, it still works properly (since the pre-deployment tests do not test a deployed version of the application). Consequently, any configuration or infrastructure-level errors should surface here, and should more or less guarantee the correctness of the production deployment as well, at least as long as the lower environments are representative enough of the production environment. Most test cases in this suite should not be run against production, since many operations will typically mutate some data in some way. If we run this suite against an environment that is representative of production (e.g. same infrastructure, real dependencies instead of mocks, etc.), and run smoke tests against production (to verify configuration, verify that the service is up and running), then it is implied that production should be correct as well.

* [ ] Initial pipeline:
  
  * Build application & execute unit tests
  * Create container image
  * Compose application, run pre-deployment tests
  * Create container registry, push image to container registry (only if the pre-deployment tests succeeded)

* [ ] Set up hosting for the application (initially without complicated stuff such as DB migration, rollback, backup, etc., just create something that will show something in the browser. Set up the infrastructure with terraform (container hosting, database, networks, etc.)

  * Need at least 2 environments (non-prod and prod) so that the environment-specific parts of the tests and pipelines can be tested.

* [ ] Extend the pipeline with the following:

  * [ ] Stop the running applications before deployment (so we can do migration and backup safely)
  * [ ] Backup database
  * [ ] Migrate database
  * [ ] Verify migration success
  * [ ] Deploy application
  * [ ] Run smoke tests (if failed, rollback, restore DB)

    * be able to force/simulate failure to test rollback

  * [ ] Run post-deployment integration tests (if nonprod; rollback & restore DB on failure)

    * be able to force/simulate failure to test rollback

* [ ] Destroy everything (for Azure cost saving purposes)

# learning-terraform

# Future plans

* [x] A sample, dockerized ASP.NET Core application which uses MS SQL, EF and DB migrations
* [ ] Tests for the application:

  * [ ] __Unit tests__
  * [ ] __Pre-deployment integration tests:__ this could be done with docker compose or asp.net core in-memory hosting against dedicated dependencies. The goal of this stage is to catch all failures before committing to DB migration and deployments. Dedicated dependency here means a private dependency (e.g. database) that is not used by anything else (e.g. application deployed to a non-prod environment).
  * [ ] __Post-deployment smoke tests (all environments)__: smoke tests that run after a deployment and verify that the application is alive and capable of serving requests. We must also ensure that we receive a response from the version we want (e.g. container could be started successfully, runtime did not roll back to the previous version because of startup failure). These can be run against prod as well, therefore these must not mutate data. A health check or a non-PII-containing endpoint can be a smoke test target. Once we start receiving responses from the newly deployed version, we can move on to the next item.
  * [ ] __Post-deployment integration tests (lower environment):__ tests that run after a deployment, but only on non-production environments. This is meant to ensure that the application works correctly with non-private dependencies as well, but these are not run against production since in most cases, data manipulation & preparation is necessary for a test scenario. Assuming that there is an environment that is identical to prod (except the data), this ensures that the application will work in production.

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

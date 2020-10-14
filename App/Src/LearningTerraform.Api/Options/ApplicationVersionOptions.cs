namespace LearningTerraform.Api.Options
{
    public class ApplicationVersionOptions
    {
        public const string SectionName = "ApplicationVersion";

        public string Commit { get; set; }

        public string Version { get; set; }
    }
}

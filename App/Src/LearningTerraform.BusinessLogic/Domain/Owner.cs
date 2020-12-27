using System.Collections.Generic;

namespace LearningTerraform.BusinessLogic.Domain
{
    public record Owner(string Id, string FirstName, string LastName, IEnumerable<Pet> Pets);
}

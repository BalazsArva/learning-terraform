using System;
using System.Threading.Tasks;
using LearningTerraform.Api.Contracts.Requests;
using LearningTerraform.Api.Contracts.Responses;
using LearningTerraform.BusinessLogic.Operations.Commands.CreatePet;
using LearningTerraform.BusinessLogic.Operations.Queries.GetPetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningTerraform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly ICreatePetCommandHandler createPetCommandHandler;
        private readonly IGetPetByIdQueryHandler getPetByIdQueryHandler;

        public PetsController(
            ICreatePetCommandHandler createPetCommandHandler,
            IGetPetByIdQueryHandler getPetByIdQueryHandler)
        {
            this.createPetCommandHandler = createPetCommandHandler ?? throw new ArgumentNullException(nameof(createPetCommandHandler));
            this.getPetByIdQueryHandler = getPetByIdQueryHandler ?? throw new ArgumentNullException(nameof(getPetByIdQueryHandler));
        }

        [HttpPost("/api/owners/{ownerId}/pets")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PetResponse))]
        public async Task<IActionResult> Create(string ownerId, CreatePetRequest request)
        {
            var petId = await createPetCommandHandler.HandleAsync(new CreatePetCommand
            {
                Name = request.Name,
                OwnerId = ownerId,
            });

            var pet = await getPetByIdQueryHandler.HandleAsync(new GetPetByIdQuery { Id = petId });
            var response = new PetResponse
            {
                Id = pet.Id,
                Name = pet.Name,
            };

            return CreatedAtAction(nameof(GetById), new { id = ownerId }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PetResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var pet = await getPetByIdQueryHandler.HandleAsync(new GetPetByIdQuery { Id = id });

            if (pet is null)
            {
                return NotFound();
            }

            return Ok(new PetResponse
            {
                Id = pet.Id,
                Name = pet.Name,
            });
        }
    }
}

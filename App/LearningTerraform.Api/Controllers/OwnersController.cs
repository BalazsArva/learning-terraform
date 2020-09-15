using System;
using System.Threading.Tasks;
using LearningTerraform.Api.Contracts.Requests;
using LearningTerraform.Api.Contracts.Responses;
using LearningTerraform.BusinessLogic.Operations.Commands.CreateOwner;
using LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningTerraform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly ICreateOwnerCommandHandler createOwnerCommandHandler;
        private readonly IGetOwnerByIdQueryHandler getOwnerByIdQueryHandler;

        public OwnersController(
            ICreateOwnerCommandHandler createOwnerCommandHandler,
            IGetOwnerByIdQueryHandler getOwnerByIdQueryHandler)
        {
            this.createOwnerCommandHandler = createOwnerCommandHandler ?? throw new ArgumentNullException(nameof(createOwnerCommandHandler));
            this.getOwnerByIdQueryHandler = getOwnerByIdQueryHandler ?? throw new ArgumentNullException(nameof(getOwnerByIdQueryHandler));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OwnerResponse))]
        public async Task<IActionResult> Create(CreateOwnerRequest request)
        {
            var ownerId = await createOwnerCommandHandler.HandleAsync(new CreateOwnerCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            });

            var owner = await getOwnerByIdQueryHandler.HandleAsync(new GetOwnerByIdQuery { Id = ownerId });
            var response = new OwnerResponse
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
            };

            return CreatedAtAction(nameof(GetById), new { id = ownerId }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OwnerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var owner = await getOwnerByIdQueryHandler.HandleAsync(new GetOwnerByIdQuery { Id = id });

            if (owner is null)
            {
                return NotFound();
            }

            return Ok(new OwnerResponse
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
            });
        }
    }
}

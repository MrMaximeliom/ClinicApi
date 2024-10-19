using DentalClinic.DataTransferObjects;
using DentalClinic.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Controllers
{
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var deffredResults = _unitOfWork.Users.GetAllDeferred();

            var r = await _unitOfWork.Users.GetAllAsync();

            var results = await deffredResults.ToListAsync();

            var resultsDto = r.Adapt<List<UsersDto>>();

            return Ok(resultsDto);

        }


    }
}

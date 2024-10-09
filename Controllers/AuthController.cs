using DentalClinic.Interfaces;
using DentalClinic.Models;
using DentalClinic.Services;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/auth")]
    [Produces("application/json")]
    [ApiController] 
    public class AuthController(IAuthService authService,IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;  

        // login api
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Login(RequestToken token)
        {
            var auth = await _authService.GetTokenAsync(token); 

            if(!auth.IsAuthenticated)
            {
                return BadRequest(auth.Message);
            }
            return Ok(auth);
        }

        // Register new users api
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Register(RegisterPatient patient)
        {
            var auth = await _authService.RegisterAsync(patient);

            if (!auth.IsAuthenticated)
            {
                return BadRequest(auth.Message);
            }

            return Ok(auth);
        }

        // revoke token api
        [HttpPut("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> RevokeToken(string userId,string refreshToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("either there is no user with this token or this token is invalid");

            }
            var isRevoked = await _authService.RevokeTokenAsync(user,refreshToken);

            if (!isRevoked)
            {
                return BadRequest("either there is no user with this token or this token is invalid");

            }
            return Ok("refresh token has been successfuly revoked");
         
        }

        // refresh token api
        [HttpPut("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var auth = await _authService.RefreshTokenAsync(refreshToken);

            if (!auth.IsAuthenticated)
            {
                return BadRequest("either there is no user with this token or this token token is invalid!");
            }
            return Ok(auth);

        }

        // Add user to a named role 
        [HttpPost("add-user-to-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddUserToRoleToken(AddRole model)
        {
            var resultText = await _authService.AddUserToRoleAsync(model);  

            if(!string.IsNullOrEmpty(resultText))
            {
                return BadRequest("either there is no user witht this token or this token is invalid!");
            }

            return Ok("User has been added to role successfully");
        }


    }
}

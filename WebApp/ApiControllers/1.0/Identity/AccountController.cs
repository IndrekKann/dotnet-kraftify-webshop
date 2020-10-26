using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Identity;

namespace WebApp.ApiControllers._1._0.Identity
{
    /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase 
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.App.Identity.AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        /// <param name="bll"></param>
        public AccountController(IConfiguration configuration, UserManager<Domain.App.Identity.AppUser> userManager, SignInManager<Domain.App.Identity.AppUser> signInManager, ILogger<AccountController> logger, IAppBLL bll)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _bll = bll;
        }
        
        /// <summary>
        /// Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="model">login data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return NotFound(new MessageDTO("User not found!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims
                        .Append(new Claim(ClaimTypes.GivenName, appUser.FirstName))
                        .Append(new Claim(ClaimTypes.Surname, appUser.LastName))
                        .Append(new Claim(ClaimTypes.MobilePhone, appUser.Phone)), 
                    _configuration["JWT:SigningKey"], 
                    _configuration["JWT:Issuer"], 
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );
                _logger.LogInformation($"WebApi login. User {appUser.Email} logged in.");
                return Ok(new JwtResponseDTO()
                {
                    Token = jwt, Status = $"User {appUser.Email} logged in.", 
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Phone = appUser.Phone
                });
            }
            
            _logger.LogInformation($"WebApi login. User {appUser.Email} failed login attempt!");
            return NotFound(new MessageDTO("User not found!"));
        }

        /// <summary>
        /// Endpoint for user registration and immediate log-in (jwt generation) 
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser != null)
            {
                _logger.LogInformation($"WebApi register. User {model.Email} already registered!");
                return NotFound(new MessageDTO("User already registered!"));
            }

            appUser = new Domain.App.Identity.AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            
            // On account registration, the creation of shopping cart also happens
            var shoppingCart = new BLL.App.DTO.ShoppingCart
            {
                Id = Guid.NewGuid(),
                AppUserId = appUser.Id
            };
            _bll.ShoppingCarts.Add(shoppingCart);
            await _bll.SaveChangesAsync();
            // Registration continues
            
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {appUser.Email} created a new account with password.");

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims
                            .Append(new Claim(ClaimTypes.GivenName, appUser.FirstName))
                            .Append(new Claim(ClaimTypes.Surname, appUser.LastName))
                            .Append(new Claim(ClaimTypes.MobilePhone, appUser.Phone)),
                        _configuration["JWT:SigningKey"],
                        _configuration["JWT:Issuer"],
                        _configuration.GetValue<int>("JWT:ExpirationInDays")
                    );
                    _logger.LogInformation($"WebApi register. User {user.Email} logged in.");
                    return Ok(new JwtResponseDTO()
                    {
                        Token = jwt, Status = $"User {user.Email} created and logged in.",
                        FirstName = appUser.FirstName, 
                        LastName = appUser.LastName, 
                        Phone = appUser.Phone
                    });
                }

                _logger.LogInformation($"User {appUser.Email} not found after creation!");
                return BadRequest(new MessageDTO("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new MessageDTO() {Messages = errors});
        }
        
    }
}

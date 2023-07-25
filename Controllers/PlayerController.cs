using Player.Api.Models;
using Player.Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Player.Api.Exceptions;
using System.Security.Cryptography;
using NuGet.Common;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Player.Api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Player.Api.Controllers;

/// <summary>
///     Provides a controller for Player requests.
/// </summary>
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    /// <summary>
    ///     A <see cref="ILogger" /> for the <see cref="PlayerController" /> class representing the logger to be called.
    /// </summary>
    private readonly ILogger<PlayerController> _logger;

    /// <summary>
    ///     A local <see cref="IPlayerServices" /> IPlayerServices service variable
    /// </summary>
    private readonly IPlayerServices _service;

    private readonly IConfiguration _configuration;
   
    public static Models.Player player = new Models.Player();

    /// <summary>
    ///     Initializes a new instance of the <see cref="PlayerController" />.
    /// </summary>
    /// <param name="services">A <see cref="IPlayerServices" /> service</param>
    /// <param name="logger">
    ///     A <see cref="ILogger" /> for the <see cref="AdvisorController" /> class representing the logger to
    ///     be called.
    /// </param>
    public PlayerController(IPlayerServices services, ILogger<PlayerController> logger, IConfiguration conf)
    {
        _service = services;
        _logger = logger;
        _configuration = conf;
    }


    //I am not writting this user to the database as it not part oif the requirement. You can create any user using the register method and attemp to 
    //to login. you will recieve a token that you can use on your session as it required for security purposes.
    //You can instert records for the user you would like to test with then do all the operations. 2 test users has been seeded in the system to make it easier 
    //for you to test.
    [HttpPost("Register")]
    public async Task<ActionResult<Models.Player>> Register(Models.UserDto request)
    {

        CreatePasswordHass(request.password, out byte[] passwordhash, out byte[] passwordSalt);
        player.passwordSalt = passwordSalt;
        player.passwordHash = passwordhash;
        player.username = request.username;
        return Ok(player);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(Models.UserDto request)
    {

        if (player.username != request.username)
        {
            return BadRequest("Player does not exist");
        }
        if (!VerifyPasswordHash(request.password, player.passwordHash, player.passwordSalt))
        {
            return BadRequest("Incorrect password");
        }
        var token = CreateToken(request);
        
        return Ok(token);

    }



    /// <summary>
    ///     Get Player Balance
    /// </summary>
    /// <returns>A <see cref="{Player}"/> Get Player Balance</returns>
    [HttpGet("GetPlayerBalance")]
    [Authorize]
    public async Task<IActionResult> GetPlayerBalance(int playerId)
    {
            try
            {
                var playerBalance =  _service.GetPlayerBalance(playerId);
                if (playerBalance == null) return NotFound("Player Not Found");
                return Ok(playerBalance);
            }
            catch (Exception ex)
            {
                Logging.WriteErrorLog(ex.Message);
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
    }

    /// <summary>
    ///     Debit Player Balance
    /// </summary>
    /// <returns>A <see cref="{Player}"/> Debit Player</returns>
    [HttpPut("DebitPlayer")]
    [Authorize]
    public async Task<IActionResult> DebitPlayer(int playerId,double value, string? note)
    {
        try
        {
            await _service.DebitPlayerAsync(playerId,value,note);
            return Ok("Player "+ playerId + " has been debited successfully with values " + value );
        }
        catch (Exception ex)
        {
            Logging.WriteErrorLog(ex.Message);
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     Get Player Balance
    /// </summary>
    /// <returns>A <see cref="{Player}"/> Debit Player</returns>
    [HttpPut("CreditPlayer")]
    [Authorize]
    public async Task<IActionResult> CreditPlayer(int playerId, double value, string? note)
    {
        try
        {
            await _service.CreditPlayerAsync(playerId, value, note);
            return Ok("Player " + playerId + " has been credited successfully with values " + value);
        }
        catch (Exception ex)
        {
            Logging.WriteErrorLog(ex.Message);
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     PST RefundPlayer
    /// </summary>
    /// <returns>A <see cref="{Player}"/> Debit Player</returns>
    [HttpPut("RefundPlayer")]
    [Authorize]
    public async Task<IActionResult> RefundPlayer(int playerId, double value, string? note)
    {
        try
        {
            await _service.RefundPlayerAsync(playerId, value, note);
            return Ok("Player " + playerId + " has been Refunded successfully with values " + value);
        }
        catch (Exception ex)
        {
            Logging.WriteErrorLog(ex.Message);
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }


    private void CreatePasswordHass(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using(var haac = new HMACSHA512())
        {
            passwordSalt = haac.Key;
            passwordHash = haac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  
        }
    }

    private bool VerifyPasswordHash(string password, byte[]passwordHash, byte[] passwordSalt)
    {
        using(var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
       
    }

    private string CreateToken(Models.UserDto user)
    {
        List<Claim> claim = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.username )
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claim,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);
       var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

}
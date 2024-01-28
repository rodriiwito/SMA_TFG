using BootesConsulta.Database.Models;
using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Helpers;
using BootesConsulta.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootesConsulta.Features.UserInfo;

[ApiController]
[Route("api/user")]
public class UserInfoController : Controller
{
    private readonly ILoginRepository _loginRepository;
    public UserInfoController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteUser()
    {
        string token = HttpContext.Request.Headers["Authorization"];
        token = token[7..];
        try
        {
            await _loginRepository.Delete(new()
            {
                Email = TokenHelper.GetEmail(token),
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Ok();
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        string token = HttpContext.Request.Headers["Authorization"];
        token = token[7..];
        try
        {
            await _loginRepository.UpdateUser(new()
            {
                Email = TokenHelper.GetEmail(token),
                Country = request.Country,
                Organization = request.Organization,
                UserType = request.UserType
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Ok();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetUserInfo()
    {
        string token = HttpContext.Request.Headers["Authorization"];
        token = token[7..];
        SelectUserResult result;
        try
        {
            result = await _loginRepository.SelectUser(new()
            {
                Email = TokenHelper.GetEmail(token)
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Ok(result);
    }
    public class UpdateUserRequest
    {
        public string Country { get; set; }
        public string Organization { get; set; }
        public UserType UserType { get; set; }
    }
}

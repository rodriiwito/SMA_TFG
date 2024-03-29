﻿using BootesConsulta.Models;
using Dapper;

namespace BootesConsulta.Database.Models;

public class CreateUserParameters
{
    public const string SpName = "Register_User";
    public string Email { get; set; }
    public string Password { get; set; }
    public string Country { get; set; }
    public string Organization { get; set; }
    public UserType UserType { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        dynamicParameters.Add("@Password", Password);
        dynamicParameters.Add("@Country", Country);
        dynamicParameters.Add("@Organization", Organization);
        dynamicParameters.Add("@UserType", UserType);
        return dynamicParameters;
    }
}

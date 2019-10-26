﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookieJWT.Server.Common
{
    /// <summary>
    /// Refers from https://www.c-sharpcorner.com/article/how-to-use-jwt-authentication-with-web-api/
    /// </summary>
    public class JwtTokenManager
    {
        private readonly string secret;

        public JwtTokenManager(string secret)
        {
            this.secret = secret;
        }

        public string GenerateToken(Claim[] claims)
        {
            var key = Convert.FromBase64String(secret);
            var securityKey = new SymmetricSecurityKey(key);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(15),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };


            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(tokenDesc);
            return handler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipal(string token, out SecurityToken securityToken)
        {
            securityToken = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                if (jwtToken == null)
                {
                    return null;
                }

                var key = Convert.FromBase64String(secret);
                var tokenParams = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var principal = tokenHandler.ValidateToken(token, tokenParams, out securityToken);
                return principal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

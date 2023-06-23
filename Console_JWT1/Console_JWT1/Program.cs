using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel;
using System.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Console_JWT1
{
    class Program
    {
        //Nuget install-package  "System.IdentityModel.Tokens.Jwt" 
        static void Main(string[] args)
        {
            Console.WriteLine("");
            string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            
            var securityKey = new Microsoft
               .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                            (securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);
            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
           {
               { "some ", "hello "},
               { "scope", "http://dummy.com/"},
           };
            //
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            Console.WriteLine(tokenString);
            Console.WriteLine("Consume Token");
            var token = handler.ReadJwtToken(tokenString);

            Console.WriteLine(token.Payload.Where(a=>a.Key== "scope").FirstOrDefault().Value);

            Console.ReadLine();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace MicrosServices.APIGateway.Global
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OpenID Connect隐式流客户端（MVC）
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,//隐式方式
                    RequireConsent=false,//如果不需要显示否同意授权 页面 这里就设置为false
                    RedirectUris = { "http://localhost:5002/signin-oidc" },//登录成功后返回的客户端地址
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },//注销登录后返回的客户端地址

                    AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}

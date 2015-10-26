using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using Entity.Models;
using Entity.Code;

namespace Entity
{
    public static class AuthConfig
    {
        
        public static void RegisterAuth()
        {
            //OAuthWebSecurity.RegisterClient(client: new VKAuthenticationClient("here client_id", "here client_secret"), displayName: "Вконтакте", extraData: null);
            OAuthWebSecurity.RegisterClient(client: new VKAuthenticationClient("5119810", "GUAfpyRg6Wu0ajxFqZ7Z"), displayName: "Вконтакте", extraData: null);
                                                                     
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // следует обновить сайт. Дополнительные сведения: http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}

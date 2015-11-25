using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.AspNet;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;
using Entity.Models;

namespace Entity.Code
{
    public class VKAuthenticationClient : IAuthenticationClient
    {
        public string appId;
        public string appSecret;
        public string redirectUri;
        public int scope = 16;

        public VKAuthenticationClient(string appId, string appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }
        string IAuthenticationClient.ProviderName {
            get { return "vkontakte"; }
        }
        class AccessToken {
            public string access_token = null;
            public string user_id = null;
        }
        void IAuthenticationClient.RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            var APP_ID = this.appId;
            this.redirectUri=context.Server.UrlEncode(returnUrl.ToString());
            var address = String.Format("https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri={2}&display=popup&response_type=code", APP_ID, scope, this.redirectUri);
                HttpContext.Current.Response.Redirect(address, false);
        }
        AuthenticationResult IAuthenticationClient.VerifyAuthentication(HttpContextBase context)
        {
            try {
                string code=context.Request["code"];
                var address = String.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}",this.appId,this.appSecret,code,this.redirectUri);
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                Person person;

                var response = client.DownloadString(address);
                var accessToken=JsonConvert.DeserializeObject<AccessToken>(response);

                address = String.Format("https://api.vk.com/method/users.get?uids={0}&fields=nickname", accessToken.user_id);
                client.Encoding = System.Text.Encoding.UTF8;
                response = client.DownloadString(address);
                person = JsonConvert.DeserializeObject<Persons>(response).People[0];
                return new AuthenticationResult(true,(this as IAuthenticationClient).ProviderName,accessToken.user_id,person.FirstName + " " +person.LastName,new Dictionary<string,string>());
            }
            catch(Exception ex){
                return new AuthenticationResult(ex);
            }
        }
    }
}
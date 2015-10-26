using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.AspNet;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;

namespace Entity.Code
{
    public class VKAuthenticationClient : IAuthenticationClient
    {
        public string appId;
        public string appSecret;
        public string redirectUri;
        public string redirectUrien;
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
            //this.redirectUri = returnUrl.ToString();
            this.redirectUri=context.Server.UrlEncode(returnUrl.ToString());
            //this.redirectUri = "http://oauth.vk.com/blank.html";
            //this.redirectUri ="http://localhost:56929/Account/ExternalLoginCallback";
            var address = String.Format("https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri={2}&display=popup&response_type=code", APP_ID, scope, this.redirectUri);
                HttpContext.Current.Response.Redirect(address, false);
        }
        AuthenticationResult IAuthenticationClient.VerifyAuthentication(HttpContextBase context)
        {
            try {
                string code=context.Request["code"];
                var address = String.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}",this.appId,this.appSecret,code,this.redirectUri);
                WebClient client = new WebClient();
                //client.UseDefaultCredentials = true;
                //client.Proxy = null;
                var response = client.DownloadString(address);
                //byte[] Data;
                //using (WebClient WC = new WebClient())
                //{
                //    try
                //    {
                //        Data = WC.DownloadData(address);
                //    }
                //    catch (WebException ex)
                //    {
                //        if (ex.Response == null)
                //            throw;
                //        using (WebResponse Response = ex.Response)
                //        {
                //            using (Stream Stream = Response.GetResponseStream())
                //            {
                //                using (BinaryReader Reader = new BinaryReader(Stream))
                //                {
                //                    Data = Reader.ReadBytes((int)Stream.Length);

                //                }
                //            }
                //        }
                //    }
                //}
               // var response=System.Text.Encoding.UTF8.GetString(Data);
                //var response = VKAuthenticationClient.Load(address);
                //var accessToken = VKAuthenticationClient.DeserializeJson<AccessToken>(response);
                var accessToken=JsonConvert.DeserializeObject<AccessToken>(response);
                return new AuthenticationResult(true,(this as IAuthenticationClient).ProviderName,accessToken.user_id,"Это" + "ты!",new Dictionary<string,string>());
            }
            catch(Exception ex){
                return new AuthenticationResult(ex);
            }
        }
        public static string Load(string address)
        {
            var request = WebRequest.Create(address) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T DeserializeJson<T>(string input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(input);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity.Models;
using System.Net;
using Newtonsoft.Json;

namespace Entity.Bisness_Logic
{
    public static class DownloadUsers
    {
        public static Person DownloadUserInformation(int uid,EntityContext context)
        {
            
            Person personFromDb = context.People.FirstOrDefault(x => x.UID == uid);
            if (personFromDb != null) return null;
            Person person = new Person();
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string urlGetUser = String.Format("https://api.vk.com/method/users.get?uids={0}&fields=nickname", person.UID);
            var response = client.DownloadString(urlGetUser);
            if (urlGetUser.Contains("error")) return null;
            person = JsonConvert.DeserializeObject<Persons>(response).People[0];
            context.People.Add(DownloadUsers.DownloadUserInformation(person.UID, context));
            context.SaveChanges();
            return person;
        }
        public static int GetUserId(string mask)
        {
            WebClient client = new WebClient();
            string urlResolveScreenName = "https://api.vk.com/method/utils.resolveScreenName?screen_name=" + mask;
            string jsonStringFriends = client.DownloadString(urlResolveScreenName);
            ResolveScreenName resolvScreenName = JsonConvert.DeserializeObject<ResponseScreenName>(jsonStringFriends).response;
            return resolvScreenName.ObjectId;
        }

    }
}
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
        private static WebClient client = new WebClient();

        public static Person DownloadUserInformation(int uid)
        {           
            Person person = new Person();
            client.Encoding = System.Text.Encoding.UTF8;
            string urlGetUser = String.Format("https://api.vk.com/method/users.get?uids={0}&fields=nickname", uid);
            var response = client.DownloadString(urlGetUser);
            if (urlGetUser.Contains("error")) return null;
            person = JsonConvert.DeserializeObject<Persons>(response).People[0];
            return person;
        }
        public static int GetUserId(string mask)
        {            
            string urlResolveScreenName = "https://api.vk.com/method/utils.resolveScreenName?screen_name=" + mask;
            string jsonStringFriends = client.DownloadString(urlResolveScreenName);
            ResolveScreenName resolvScreenName = JsonConvert.DeserializeObject<ResponseScreenName>(jsonStringFriends).response;
            return resolvScreenName.ObjectId;
        }
        public static List<Person> DownloadFriends(int id)
        {
            String urlGetFriends = "https://api.vkontakte.ru/method/friends.get?user_id=" + id + "&fields=nickname";
            List<Person> persons;
            string jsonStringFriends = client.DownloadString(urlGetFriends);
            if (jsonStringFriends.Contains("error")) return null;
            persons = JsonConvert.DeserializeObject<Persons>(jsonStringFriends).People;
            return persons;
        }

    }
}
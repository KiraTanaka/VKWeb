using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Models
{
    [Serializable]
    public class UserInformationToAdd
    {
        public string url;
        public bool addFriends;
        public bool addMembersOfGroup;
    }
}
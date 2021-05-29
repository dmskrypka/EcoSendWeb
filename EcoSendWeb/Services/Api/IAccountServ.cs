using EcoSendWeb.Models.BO.Account;
using System;
using System.Collections.Generic;

namespace EcoSendWeb.Services.Api
{
    public interface IAccountServ
    {
        UserBO GetUserOnLogin(string email, string password);
        UserBO GetUser(Guid userId);

        string RegisterUser(UserBO user, string password);

        IEnumerable<RegisteredPerson> GetRegisteredPersons();

        bool IsUserInRole(Guid userId, string role);

        void SaveUserInfo(UserBO user);
    }
}

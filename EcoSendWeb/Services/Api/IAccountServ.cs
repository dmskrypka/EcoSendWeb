using EcoSendWeb.Models.BO.Account;
using System;

namespace EcoSendWeb.Services.Api
{
    public interface IAccountServ
    {
        UserBO GetUserOnLogin(string email, string password);
        UserBO GetUser(Guid userId);

        string RegisterUser(UserBO user, string password);

        bool IsUserInRole(Guid userId, string role);

        void SaveUserInfo(UserBO user);
    }
}

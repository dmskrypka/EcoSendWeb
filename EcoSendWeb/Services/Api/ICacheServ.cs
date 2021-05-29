using System;

namespace EcoSendWeb.Services.Api
{
    public interface ICacheServ
    {
        void SetObject<T>(string strKey, T value);
        void SetObject<T>(string strKey, T value, TimeSpan timeout);
        T GetObject<T>(string strKey);
        void RemoveObject(string strKey);
    }
}

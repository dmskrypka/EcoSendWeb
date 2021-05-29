using EcoSendWeb.Models.BO.Parcel;
using System;
using System.Collections.Generic;

namespace EcoSendWeb.Services.Api
{
    public interface IParcelServ
    {
        IList<ParcelBO> GetAllParcels();
        IList<ParcelBO> GetUserReceivedParcels(Guid userId);
        IList<ParcelBO> GetUserSendParcels(Guid userId);

        ParcelBO GetParcelInfoById(int parcelId);

        IList<PackBO> GetAllParcelTypes();

        int SaveSendParcelInfo(ParcelBO parcel, decimal price);
        void ReceiveParcel(int parcelId, bool isPackRecycled);
        IList<ParcelBO> GetAllSendParcels(bool approved);

        void ApproveParcel(int parcelId, Guid employeeId);

        IList<MovementBO> GetUserMovements(Guid userId);

        PackBO GetPackById(int packId);

        void SavePaymentInfo(decimal price, ParcelBO parcel);

        bool SavePaymentResult(string liqPayOrderId, bool success);

        Guid? SubtractMovement(string liqPayOrderId);
    }
}

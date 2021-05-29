using EcoSendWeb.App_Start;
using EcoSendWeb.Helpers;
using EcoSendWeb.Models.BO.Parcel;
using EcoSendWeb.Models.DAO;
using EcoSendWeb.Services.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EcoSendWeb.Services.Impl
{
    public class ParcelServ : IParcelServ
    {
        public IList<ParcelBO> GetAllParcels()
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IList<ParcelBO>>(SqlQueryHelper.ExecuteSqlQuery(db.Database.Connection, GetAllParcelsQuery()).Rows);
            }
        }

        public IList<ParcelBO> GetUserReceivedParcels(Guid userId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string query = GetAllParcelsQuery($"WHERE r.[fk_user] = '{userId}'");

                return MappingProfilesConfig.Mapper.Map<IList<ParcelBO>>(SqlQueryHelper.ExecuteSqlQuery(db.Database.Connection, query).Rows);
            }
        }

        public IList<ParcelBO> GetUserSendParcels(Guid userId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string query = GetAllParcelsQuery($"WHERE p.[fk_sender] = '{userId}'");

                return MappingProfilesConfig.Mapper.Map<IList<ParcelBO>>(SqlQueryHelper.ExecuteSqlQuery(db.Database.Connection, query).Rows);
            }
        }

        public IList<ParcelBO> GetAllSendParcels(bool approved)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string query = GetAllParcelsQuery($" WHERE p.[confirmed_date] IS {(approved ? "NOT" : "")} NULL");

                return MappingProfilesConfig.Mapper.Map<IList<ParcelBO>>(SqlQueryHelper.ExecuteSqlQuery(db.Database.Connection, query).Rows);
            }
        }

        public ParcelBO GetParcelInfoById(int parcelId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string query = GetAllParcelsQuery($" WHERE p.[pk_parcel] = {parcelId}");

                return MappingProfilesConfig.Mapper.Map<IEnumerable<ParcelBO>>(SqlQueryHelper.ExecuteSqlQuery(db.Database.Connection, query).Rows).FirstOrDefault();
            }
        }

        public IList<PackBO> GetAllParcelTypes()
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IList<PackBO>>(db.tblPacks);
            }
        }

        public int SaveSendParcelInfo(ParcelBO parcel, decimal price)
        {
            int parcelId = 0;

            using (EcoSendEntities db = new EcoSendEntities())
            {
                tblParcel tblParcel = new tblParcel();
                tblPack pack = db.tblPacks.Single(x => x.pk_pack == parcel.PackType);

                if (db.tblRecipients.FirstOrDefault(x => x.first_name == parcel.RecipientFirstName &&
                    x.last_name == parcel.RecipientLastName &&
                    x.email == parcel.RecipientEmail.ToLower()) is tblRecipient recipient)
                {
                    tblParcel.fk_recipient = recipient.pk_recipient;
                }
                else
                {
                    recipient = new tblRecipient
                    {
                        pk_recipient = Guid.NewGuid(),
                        first_name = parcel.RecipientFirstName,
                        last_name = parcel.RecipientLastName,
                        email = parcel.RecipientEmail.ToLower()
                    };

                    if (db.tblUsers.FirstOrDefault(x => x.first_name == parcel.RecipientFirstName &&
                        x.last_name == parcel.RecipientLastName &&
                        x.email == parcel.RecipientEmail.ToLower()) is tblUser user)
                    {
                        recipient.fk_user = user.pk_user;
                    }

                    db.tblRecipients.Add(recipient);

                    tblParcel.fk_recipient = recipient.pk_recipient;
                }

                tblParcel.fk_pack = parcel.PackType;
                tblParcel.fk_sender = parcel.SenderId;
                tblParcel.price = price;
                tblParcel.destination_city = parcel.DestCity;
                tblParcel.destination_street = parcel.DestStreet;
                tblParcel.destination_postal_code = parcel.DestPostalCode;
                tblParcel.destination_country = parcel.DestCountry;
                tblParcel.created_date = DateTime.Now;

                db.tblParcels.Add(tblParcel);
                db.SaveChanges();

                parcelId = tblParcel.pk_parcel;
            }

            return parcelId;
        }

        public void ReceiveParcel(int parcelId, bool isPackRecycled)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                if (db.tblParcels.FirstOrDefault(x => x.pk_parcel == parcelId && !x.received_date.HasValue) is tblParcel parcel)
                {
                    parcel.received_date = DateTime.Now;
                    parcel.is_pack_recycled = true;

                    db.SaveChanges();
                }
            }
        }

        public void ApproveParcel(int parcelId, Guid employeeId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            using (DbContextTransaction tx = db.Database.BeginTransaction())
            {
                try
                {
                    if (db.tblParcels.FirstOrDefault(x => x.pk_parcel == parcelId && !x.confirmed_date.HasValue) is tblParcel parcel)
                    {
                        parcel.confirmed_date = DateTime.Now;
                        parcel.fk_employee = employeeId;

                        if (db.tblRecipients.FirstOrDefault(x => x.pk_recipient == parcel.fk_recipient && x.fk_user.HasValue) is tblRecipient recipient)
                        {
                            db.tblUserMovements.Add(new tblUserMovement
                            {
                                fk_parcel = parcel.pk_parcel,
                                fk_user = recipient.fk_user.Value,
                                points = db.tblPacks.Single(x => x.pk_pack == parcel.fk_pack).points,
                                created_date = DateTime.Now
                            });
                        }

                        db.SaveChanges();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public IList<MovementBO> GetUserMovements(Guid userId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string query = $@"SELECT um.[fk_parcel]
	                                ,p1.[name]
	                                ,um.[points]
                                    ,um.[created_date]
                              FROM [dbo].[tblUserMovements] um
                              INNER JOIN [dbo].[tblParcels] p ON p.[pk_parcel] = um.[fk_parcel]
                              INNER JOIN [dbo].[tblPacks] p1 ON p1.[pk_pack] = p.[fk_pack]
                              WHERE um.[fk_user] = '{userId}'
                              ORDER BY um.[created_date] DESC";

                return MappingProfilesConfig.Mapper.Map<IList<MovementBO>>(SqlQueryHelper.ExecuteSqlQuery(db.Database.Connection, query).Rows);
            }
        }

        public PackBO GetPackById(int packId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                return MappingProfilesConfig.Mapper.Map<PackBO>(db.tblPacks.Single(x => x.pk_pack == packId));
            }
        }

        public void SavePaymentInfo(decimal price, ParcelBO parcel)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                db.tblPayments.Add(new tblPayment
                {
                    pk_payment = Guid.NewGuid(),
                    fk_user = parcel.SenderId,
                    fk_parcel = parcel.Id,
                    amount = price,
                    success = false,
                    created_date = DateTime.Now
                });

                db.SaveChanges();
            }
        }

        public bool SavePaymentResult(string liqPayOrderId, bool success)
        {
            try
            {
                string[] parts = liqPayOrderId.Split('_');
                int parcelId = Convert.ToInt32(parts[1]);
                using (EcoSendEntities db = new EcoSendEntities())
                {
                    if(db.tblPayments.OrderByDescending(x => x.created_date).FirstOrDefault(x => x.fk_parcel == parcelId) is tblPayment payment)
                    {
                        payment.success = success;
                        payment.result_date = DateTime.Now;

                        db.SaveChanges();

                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        public Guid? SubtractMovement(string liqPayOrderId)
        {
            string[] parts = liqPayOrderId.Split('_');

            int parcelId = Convert.ToInt32(parts[1]);
            int points = Convert.ToInt32(parts[2]);

            if (points > 0)
            {
                using (EcoSendEntities db = new EcoSendEntities())
                {
                    if (db.tblParcels.FirstOrDefault(x => x.pk_parcel == parcelId) is tblParcel parcel)
                    {
                        db.tblUserMovements.Add(new tblUserMovement
                        {
                            fk_parcel = parcel.pk_parcel,
                            fk_user = parcel.fk_sender,
                            points = (-1 * points),
                            created_date = DateTime.Now
                        });

                        db.SaveChanges();

                        return parcel.fk_sender;
                    }
                }
            }

            return null;
        }

        private string GetAllParcelsQuery(string condition = "")
        {
            return $@"SELECT p.[pk_parcel]
                           ,p.[fk_sender]
	                       ,u.[first_name] AS [sender_first_name]
	                       ,u.[last_name] AS [sender_last_name]
	                       ,p.[fk_pack]
						   ,p2.[points]
						   ,p.[is_pack_recycled]
                           ,p.[fk_recipient]
	                       ,r.[first_name] AS [recipient_first_name]
	                       ,r.[last_name] AS [recipient_last_name]
	                       ,r.[email] AS [recipient_email]
                           ,p.[price]
                           ,p.[destination_city]
                           ,p.[destination_street]
                           ,p.[destination_postal_code]
                           ,p.[destination_country]
                           ,p.[confirmed_date]
                           ,p.[fk_employee]
                           ,p.[created_date]
                           ,p.[received_date]
						   ,CASE
								WHEN p3.[success] = 1 THEN p3.[result_date]
								ELSE NULL
							END AS [paid_date]
                    FROM [dbo].[tblParcels] p
                    INNER JOIN [dbo].[tblRecipients] r ON r.[pk_recipient] = p.[fk_recipient]
                    INNER JOIN [dbo].[tblUsers] u ON u.[pk_user] = p.[fk_sender]
					INNER JOIN [dbo].[tblPacks] p2 ON p2.[pk_pack] = p.[fk_pack]
					INNER JOIN [dbo].[tblPayments] p3 ON p3.[fk_parcel] = p.[pk_parcel]
                    {condition} 
                    ORDER BY p.[created_date] DESC";
        }
    }
}
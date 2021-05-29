using EcoSendWeb.App_Start;
using EcoSendWeb.Infrastructure;
using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Models.DAO;
using EcoSendWeb.Services.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EcoSendWeb.Services.Impl
{
    public class AccountServ : IAccountServ
    {
        public UserBO GetUserOnLogin(string email, string password)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string pswdHash = HashPassword(password);
                if (db.tblUsers.FirstOrDefault(x => x.email == email && x.password_hash == pswdHash) is tblUser user)
                {
                    user.last_login = DateTime.Now;
                    db.SaveChanges();

                    UserBO userBO = MappingProfilesConfig.Mapper.Map<UserBO>(user);
                    userBO.Points = db.tblUserMovements.Where(x => x.fk_user == user.pk_user)
                        .Select(x => x.points)
                        .DefaultIfEmpty(0)
                        .Sum();

                    return userBO;
                }
            }

            return null;
        }

        public string RegisterUser(UserBO user, string password)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                tblUser regp = db.tblUsers.FirstOrDefault(x => x.email == user.Email);
                if (regp == null)
                {
                    Guid userId = Guid.NewGuid();

                    db.tblUsers.Add(new tblUser
                    {
                        pk_user = userId,
                        first_name = user.FirstName,
                        last_name = user.LastName,
                        email = user.Email.ToLower(),
                        phone = user.Phone,
                        city = user.City,
                        street = user.Street,
                        postal_code = user.PostalCode,
                        pasport = user.Pasport,
                        password_hash = HashPassword(password),
                        created_date = DateTime.Now
                    });

                    db.tblUserToRoles.Add(new tblUserToRole
                    {
                        pk_map = Guid.NewGuid(),
                        fk_user = userId,
                        fk_role = (int)UserRoles.Client
                    });

                    if(db.tblRecipients.FirstOrDefault(x => x.first_name == user.FirstName &&
                        x.last_name == user.LastName &&
                        x.email == user.Email.ToLower()) is tblRecipient recipient)
                    {
                        recipient.fk_user = userId;
                    }

                    db.SaveChanges();

                    return "Please, login to yours new account";
                }
                else
                {
                    return "Email already registered.";
                }
            }
        }

        public IEnumerable<RegisteredPerson> GetRegisteredPersons()
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IEnumerable<RegisteredPerson>>(db.tblUsers);
            }
        }

        //public IEnumerable<UserUpload> GetUsersUploads()
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        string query = @"SELECT TOP (100)
        //                               rp.[pk_id] as UserId
        //                        	  ,ui.[pk_id] as UploadId
        //                        	  ,rp.[email] as Email
        //                              ,[part_number_count] as UploadCount
        //                              ,[upload_date] as UploadDate
        //                        FROM [AirDb].[dbo].[tblUploadInfo] ui
        //                        LEFT JOIN [AirDb].[dbo].[tblLoginInfo] li ON ui.fk_session = li.pk_session
        //                        LEFT JOIN [AirDb].[dbo].[tblRegisteredPersons] rp ON rp.pk_id = li.fk_user_id
        //                        ORDER BY ui.[upload_date] DESC";

        //        return db.Database.SqlQuery<UserUpload>(query).ToList();
        //    }
        //}

        private string HashPassword(string strPassword)
        {
            try
            {
                using (SHA256Managed h = new SHA256Managed())
                {
                    byte[] rgData = h.ComputeHash(Encoding.UTF8.GetBytes(strPassword));
                    return Convert.ToBase64String(rgData, Base64FormattingOptions.None);
                }
            }
            catch (Exception e)
            {
                throw new Exception("HashPassword: " + e.Message);
            }
        }

        public bool IsUserInRole(Guid userId, string role)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                string query = $@"SELECT CASE WHEN EXISTS (
		                                SELECT *
		                                FROM [dbo].[tblUserToRoles] u2r
		                                INNER JOIN [dbo].[tblUserRoles] ur ON ur.[pk_role] = u2r.[fk_role] 
		                                WHERE ur.[name] = '{role}' AND u2r.[fk_user] = '{userId}'
	                                ) THEN CAST(1 AS BIT)
	                                ELSE CAST(0 AS BIT)
                                END";

                return db.Database.SqlQuery<bool>(query).ToList().First();
            }
        }

        public UserBO GetUser(Guid userId)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                if(db.tblUsers.FirstOrDefault(x => x.pk_user == userId) is tblUser user) 
                {
                    UserBO userBO = MappingProfilesConfig.Mapper.Map<UserBO>(user);
                    userBO.Points = db.tblUserMovements.Where(x => x.fk_user == user.pk_user)
                        .Select(x => x.points)
                        .DefaultIfEmpty(0)
                        .Sum();

                    return userBO;
                }

                return null;
            }
        }

        public void SaveUserInfo(UserBO user)
        {
            using (EcoSendEntities db = new EcoSendEntities())
            {
                if(db.tblUsers.FirstOrDefault(x => x.pk_user == user.Id) is tblUser tblUser)
                {
                    tblUser.email = user.Email;
                    tblUser.first_name = user.FirstName;
                    tblUser.last_name = user.LastName;
                    tblUser.phone = user.Phone;
                    tblUser.city = user.City;
                    tblUser.street = user.Street;
                    tblUser.postal_code = user.PostalCode;
                    tblUser.pasport = user.Pasport;
                    tblUser.modified_date = DateTime.Now;

                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Unknown user.");
                }
            }
        }
    }
}
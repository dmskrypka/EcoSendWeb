using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoSendWeb.Infrastructure
{
    public class Defs
    {
        public static readonly string C_TEMPDATAKEY_NEWPARCEL = Guid.NewGuid().ToString();


        public static readonly string C_VIEWDATAKEY_IDENTITY = Guid.NewGuid().ToString();
        public static readonly string C_VIEWDATAKEY_BALANCE = Guid.NewGuid().ToString();

        public static readonly string C_TEMPDATAKEY_LOGONINFO = Guid.NewGuid().ToString();

        public static readonly string C_TEMPDATAKEY_CHARTDATA = Guid.NewGuid().ToString();

        public static readonly string C_TEMPDATAKEY_REGS = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_MESSAGE = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_ACCOUNT = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_ACCOUNTEDIT = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_ORDERS = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_SHOWRECAPCHA = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_CAPCHAANSWER = Guid.NewGuid().ToString();
        public static readonly string C_TEMPDATAKEY_CUSTOMERS = Guid.NewGuid().ToString();

        public static readonly string C_COOKIEKEY_AGE = "eco.age";

        public static readonly string C_SESSIONKEY_BASKET = Guid.NewGuid().ToString();

        public static readonly string C_CACHEKEY_PRODUCTS_XML = Guid.NewGuid().ToString();
        public static readonly string C_CACHEKEY_CAPTCHA = Guid.NewGuid().ToString();

        public static readonly string C_CACHEKEY_ALLVM = Guid.NewGuid().ToString();
        public static readonly string C_CACHEKEY_VOUCHERSVM = Guid.NewGuid().ToString();
        public static readonly string C_CACHEKEY_PRODUCTSVM = Guid.NewGuid().ToString();
        public static readonly string C_CACHEKEY_CHRISTMAS_PRODUCTSVM = Guid.NewGuid().ToString();
        public static readonly string C_CACHEKEY_TEST_XML = Guid.NewGuid().ToString();
        public static readonly string C_CACHEKEY_TEST_VM = Guid.NewGuid().ToString();

        public static readonly string C_CACHEKEY_CUSTOMER_AMOUNT = "PointsForCustomer";



        public static readonly int C_BAD_REGISTRATION_ALERT = 20;

        public enum ConditionType
        {
            OH = 1,
            SV = 2,
            AR = 3
        }

        public enum CategoryType
        {
            FromMe = 0,
            ToMe = 1
        }
    }
}
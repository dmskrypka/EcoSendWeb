//using Mvc.Mailer;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Net.Configuration;
//using System.Text;
//using System.Linq;
//using EcoSendWeb.Models.View.Home;
//using EcoSendWeb.Models.View.Account;
//using EcoSendWeb.Models.View.Shop;

//namespace EcoSendWeb.Mails
//{
//    public class Mailer : MailerBase, IMailer
//    {
//        public Mailer()
//        {
//            this.MasterName = "_Layout";
//        }

//        public virtual MvcMailMessage ResendPassword(string strEmail, Guid credentialsRequest)
//        {
//            ViewData.Model = credentialsRequest;

//            SmtpSection section = (SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");

//            MvcMailMessage mailMessage = Populate(x => {
//                x.ViewName = "ResendPassword";
//                x.From = new System.Net.Mail.MailAddress(section.From, "Zaûi dobrÈ Ëasy");
//                x.To.Add(strEmail);
//                x.Subject = "Zaûi dobrÈ Ëasy - odpoveÔ na ûiadosù o zaslanie hesla";
//                x.BodyEncoding = Encoding.UTF8;
//                x.SubjectEncoding = Encoding.UTF8;
//                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
//            });

//            Dictionary<string, string> resources = this.CreateMailResources(mailMessage, "ResendPassword");
//            this.PopulateBody(mailMessage, "ResendPassword", resources);

//            Mailer.AdjustMessageEncoding(mailMessage);

//            return mailMessage;
//        }

//        public virtual MvcMailMessage RegistrationConfirm(RegistrationFormVM vm)
//        {
//            ViewData.Model = vm;

//            SmtpSection section = (SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");

//            MvcMailMessage mailMessage = Populate(x => {
//                x.ViewName = "RegistrationConfirm";
//                x.From = new System.Net.Mail.MailAddress(section.From, "Zaûi dobrÈ Ëasy");
//                x.To.Add(vm.Email);
//                x.Subject = "Zaûi dobrÈ Ëasy - potvrdenie registr·cie";
//                x.BodyEncoding = Encoding.UTF8;
//                x.SubjectEncoding = Encoding.UTF8;
//                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
//            });

//            Dictionary<string, string> resources = this.CreateMailResources(mailMessage, "RegistrationConfirm");
//            this.PopulateBody(mailMessage, "RegistrationConfirm", resources);

//            Mailer.AdjustMessageEncoding(mailMessage);

//            return mailMessage;
//        }

//        public virtual MvcMailMessage Message(ContactFormVM vm)
//        {
//            ViewData.Model = vm;

//            SmtpSection section = (SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");

//            MvcMailMessage mailMessage = this.Populate(x => {
//                x.ViewName = "Message";
//                x.From = new System.Net.Mail.MailAddress(section.From, "Zaûi dobrÈ Ëasy");
//                x.To.Add(section.From);
//                x.To.Add(vm.Email);
//                x.Subject = "Zaûi dobrÈ Ëasy - kontaktn˝ formul·r";
//                x.BodyEncoding = Encoding.UTF8;
//                x.SubjectEncoding = Encoding.UTF8;
//                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
//            });

//            //Dictionary<string, string> resources = this.CreateMailResources(mailMessage, "Message");
//            //this.PopulateBody(mailMessage, "Message", resources);

//            Mailer.AdjustMessageEncoding(mailMessage);

//            return mailMessage;
//        }

//        public virtual MvcMailMessage OrderConfirm(OrderVM vm)
//        {
//            ViewData.Model = vm;

//            SmtpSection section = (SmtpSection)System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp");

//            MvcMailMessage mailMessage = this.Populate(x => {
//                x.ViewName = "OrderConfirm";
//                x.From = new System.Net.Mail.MailAddress(section.From, "Zaûi dobrÈ Ëasy");
//                x.To.Add(section.From);
//                x.CC.Add(vm.Email);
//                if (vm.Email != vm.DeliveryData.Email)
//                {
//                    x.CC.Add(vm.DeliveryData.Email);
//                }
//                x.Subject = "Zaûi dobrÈ Ëasy - potvrdenie objedn·vky";
//                x.BodyEncoding = Encoding.UTF8;
//                x.SubjectEncoding = Encoding.UTF8;
//                x.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
//            });

//            Dictionary<string, string> resources = this.CreateMailResources(mailMessage, "OrderConfirm");
//            this.PopulateBody(mailMessage, "OrderConfirm", resources);

//            Mailer.AdjustMessageEncoding(mailMessage);

//            return mailMessage;
//        }

//        private Dictionary<string, string> CreateMailResources(System.Net.Mail.MailMessage mailMessage, string viewName)
//        {
//            Dictionary<string, string> resources = new Dictionary<string, string>();
//            //resources["hi-logo"] = CurrentHttpContext.Server.MapPath("~/Content/themes/eco/images/hi-3.png");
//            //resources["spacer"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/spacer.gif");
//            //resources["facebook"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/Facebook.jpg");
//            //resources["instagram"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/Instagram.jpg");
//            //resources["google"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/Google-plus.jpg");
//            //resources["youtube"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/Youtube.jpg");
//            //resources["myolympus3"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/MujOlympus_zpravodaj_03.jpg");
//            //resources["myolympus8"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/MujOlympus_zpravodaj_08.jpg");
//            //resources["myolympus9"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/MujOlympus_zpravodaj_09.jpg");
//            //resources["myolympus10"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/MujOlympus_zpravodaj_10.jpg");
//            //resources["myolympus11"] = CurrentHttpContext.Server.MapPath("~/Content/themes/olympus/images/MujOlympus_zpravodaj_11.jpg");

//            return resources;
//        }

//        private static void AdjustMessageEncoding(MvcMailMessage mailMessage)
//        {
//            int index = -1;
//            for (int i = 0; i < mailMessage.AlternateViews.Count; i++)
//            {
//                if (mailMessage.AlternateViews[i].ContentType.MediaType == "text/html")
//                {
//                    index = i;
//                    break;
//                }
//            }

//            if (index >= 0)
//            {
//                mailMessage.AlternateViews[index].ContentType = new System.Net.Mime.ContentType("text/html; charset=utf-8");
//            }
//        }
//    }
//}
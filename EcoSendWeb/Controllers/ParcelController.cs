using EcoSendWeb.App_Start;
using EcoSendWeb.Helpers.LiqPay;
using EcoSendWeb.Infrastructure;
using EcoSendWeb.Models.BO.Parcel;
using EcoSendWeb.Models.View.Parcel;
using EcoSendWeb.Services.Api;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EcoSendWeb.Controllers
{
    [Authorize]
    public class ParcelController : Controller
    {
        private readonly IParcelServ parcelServ;
        private readonly IAccountServ accountServ;

        private LiqPayApiHelper liqPayApiHelper;

        public string LiqPayPublicKey { get; set; }
        public string LiqPayPrivateKey { get; set; }

        public ParcelController(IParcelServ parcelServ, IAccountServ accountServ)
        {
            this.parcelServ = parcelServ;
            this.accountServ = accountServ;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if(liqPayApiHelper == null)
            {
                liqPayApiHelper = new LiqPayApiHelper(LiqPayPrivateKey, LiqPayPublicKey);
            }
        }

        public ActionResult Index()
        {
            IList<ParcelVM> vm = MappingProfilesConfig.Mapper.Map<IList<ParcelVM>>(parcelServ.GetUserSendParcels((User as IExtendedPrincipal)?.Id ?? Guid.Empty));
            return View(vm);
        }

        public ActionResult Movements()
        {
            IList<MovementVM> vm = MappingProfilesConfig.Mapper.Map<IList<MovementVM>>(parcelServ.GetUserMovements((User as IExtendedPrincipal)?.Id ?? Guid.Empty));
            return View(vm);
        }

        [HttpPost]
        public ActionResult ShowParcelInfo([Bind(Prefix = "Parcel")] int parcelId)
        {
            ParcelVM vm = MappingProfilesConfig.Mapper.Map<ParcelVM>(parcelServ.GetParcelInfoById(parcelId));
            vm.PackTypes = new SelectList(MappingProfilesConfig.Mapper.Map<IEnumerable<SelectListItem>>(parcelServ.GetAllParcelTypes()), "Value", "Text");

            return PartialView("ParcelInfoPartial", vm);
        }

        public ActionResult NewParcel()
        {
            NewParcelVM vm = new NewParcelVM
            {
                PackTypes = new SelectList(MappingProfilesConfig.Mapper.Map<IEnumerable<SelectListItem>>(parcelServ.GetAllParcelTypes()), "Value", "Text"),
                Points = (User as IExtendedPrincipal).Points
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult NewParcel(NewParcelVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.PackTypes = new SelectList(MappingProfilesConfig.Mapper.Map<IEnumerable<SelectListItem>>(parcelServ.GetAllParcelTypes()), "Value", "Text");

                Session[Defs.C_TEMPDATAKEY_NEWPARCEL] = vm;

                return RedirectToAction("CheckNewParcel");
            }

            ViewBag.Message = "Invalid data";

            return View(vm);
        }

        public ActionResult CheckNewParcel()
        {
            if(Session[Defs.C_TEMPDATAKEY_NEWPARCEL] is NewParcelVM parcelVM)
            {
                if (User is IExtendedPrincipal extendedPrincipal)
                {
                    ParcelBO parcel = MappingProfilesConfig.Mapper.Map<ParcelBO>(parcelVM);
                    parcel.SenderId = extendedPrincipal.Id;
                    parcel.SenderFirstName = extendedPrincipal.FirstName;
                    parcel.SenderLastName = extendedPrincipal.LastName;

                    PackBO pack = parcelServ.GetPackById(parcelVM.PackType);

                    decimal price = pack.Price - parcelVM.Points;

                    parcel.Id = parcelServ.SaveSendParcelInfo(parcel, price);

                    LiqPayRequestVM liqPayRequestVM = liqPayApiHelper.GetLiqPayRequestVM(
                        price,
                        parcel.Id,
                        parcelVM.Points,
                        Url.Action("LiqPayResult", "Parcel", FormMethod.Post, Request.Url.Scheme));

                    parcelServ.SavePaymentInfo(price, parcel);

                    Tuple<NewParcelVM, LiqPayRequestVM> vm = new Tuple<NewParcelVM, LiqPayRequestVM>(parcelVM, liqPayRequestVM);
                    return View("CheckNewParcel", vm);
                }
                else
                {
                    throw new HttpUnhandledException("Unknown user");
                }
            }
            else
            {
                throw new ArgumentNullException("View modes is empty");
            }
        }

        public ActionResult SendParcel()
        {
            NewParcelVM vm = new NewParcelVM
            {
                PackTypes = new SelectList(MappingProfilesConfig.Mapper.Map<IEnumerable<SelectListItem>>(parcelServ.GetAllParcelTypes()), "Value", "Text"),
                Points = (User as IExtendedPrincipal).Points
            };

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LiqPayResult(string data, string signature)
        {
            if(liqPayApiHelper.ValidateResponse(data, signature))
            {
                LiqPayApiResponse liqPayApiResponse = liqPayApiHelper.DecodeApiResponse(data);
                parcelServ.SavePaymentResult(liqPayApiResponse.OrderId, liqPayApiResponse.Status == LiqpayStatuses.Success);

                if (liqPayApiResponse.Status == LiqpayStatuses.Success)
                {
                    Guid? userId = parcelServ.SubtractMovement(liqPayApiResponse.OrderId);
                    if (userId.HasValue)
                    {
                        FormAuth.SetAuthCookie(accountServ.GetUser(userId.Value), Response);
                    }

                    ViewBag.Message = "Thanks!";

                    return View("LiqPaySuccess");
                }

                ViewBag.Message = $"{liqPayApiResponse.ErrorCode}<br/>{liqPayApiResponse.ErrorDescription}";
            }
            else
            {
                ViewBag.Message = "Unknown payment";
            }

            return View("LiqPayError");
        }

        [HttpPost]
        public ActionResult SaveParcel(NewParcelVM vm)
        {
            if(User is IExtendedPrincipal extendedPrincipal)
            {
                ParcelBO parcel = MappingProfilesConfig.Mapper.Map<ParcelBO>(vm);
                parcel.SenderId = extendedPrincipal.Id;
                parcel.SenderFirstName = extendedPrincipal.FirstName;
                parcel.SenderLastName = extendedPrincipal.LastName;
                parcel.RecipientId = Guid.NewGuid();

                parcelServ.SaveSendParcelInfo(parcel, vm.Points);

                return JavaScript(@"require(['bootbox'], function (bootbox) {
                    bootbox.alert('Saved', function () { $('#Message').val(''); location.reload(); });
                });");
            }
            else
            {
                return JavaScript(@"require(['bootbox'], function (bootbox) {
                    bootbox.alert('Error', function () { $('#Message').val(''); });
                });");
            }
        }

        [HttpPost]
        public ActionResult ReceiveParcel(int parcelId, bool isPackRecycled)
        {
            parcelServ.ReceiveParcel(parcelId, isPackRecycled);

            return Json(new { success = true, response = "Thanks, wait until it will be approved." });
        }

        [HttpPost]
        public ActionResult ChangeCategory(int catid)
        {
            IList<ParcelBO> parcels = catid == 1 ? 
                parcelServ.GetUserReceivedParcels((User as IExtendedPrincipal)?.Id ?? Guid.Empty) : 
                parcelServ.GetUserSendParcels((User as IExtendedPrincipal)?.Id ?? Guid.Empty);

            Tuple<int, IList<ParcelVM>> vm = new Tuple<int, IList<ParcelVM>>(catid, MappingProfilesConfig.Mapper.Map<IList<ParcelVM>>(parcels)); 

            return PartialView("ParcelTablePartial", vm);
        }

        [Authorize(Roles = "worker")]
        public ActionResult UsersParcels()
        {
            IList<ParcelVM> vm = MappingProfilesConfig.Mapper.Map<IList<ParcelVM>>(parcelServ.GetAllParcels());
            return View(vm);
        }

        [Authorize(Roles = "worker")]
        [HttpPost]
        public ActionResult ApproveParcel([Bind(Prefix = "Parcel")] int parcelId)
        {
            Guid userId = (User as IExtendedPrincipal).Id;

            parcelServ.ApproveParcel(parcelId, userId);

            FormAuth.SetAuthCookie(accountServ.GetUser(userId), Response);

            return JavaScript(@"require(['bootbox'], function (bootbox) {
                    bootbox.alert('Parcel approved.', function () { $('#Message').val(''); location.reload(); });
            });");
        }
    }
}
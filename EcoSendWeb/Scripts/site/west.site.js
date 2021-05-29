define(["require", "exports", "./west.site.extensions", "./west.site.ui", "bootbox"], function (require, exports, extensions, ui, bootbox) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var AjaxRequest = /** @class */ (function () {
        function AjaxRequest() {
        }
        AjaxRequest.Post = function (url, postData, onSuccess, suppressError) {
            if (suppressError === void 0) { suppressError = false; }
            if (postData) {
                $.ajax(url, {
                    type: "POST",
                    data: JSON.stringify(postData),
                    contentType: "application/json;charset=UTF-8",
                    success: onSuccess,
                    error: function (xhr) {
                        if (!suppressError) {
                            alert("Transfer failure: " + xhr.status.toString() + "\n" + xhr.responseText);
                        }
                    }
                });
            }
            else {
                $.ajax(url, {
                    type: "POST",
                    success: onSuccess,
                    error: function (xhr) {
                        if (!suppressError) {
                            alert("Transfer failure: " + xhr.status.toString() + "\n" + xhr.responseText);
                        }
                    }
                });
            }
        };
        AjaxRequest.PostForm = function (url, postData, onSuccess, suppressError) {
            if (suppressError === void 0) { suppressError = false; }
            $.ajax(url, {
                type: "POST",
                data: postData,
                success: onSuccess,
                error: function (xhr) {
                    if (!suppressError) {
                        alert("Transfer failure: " + xhr.status.toString() + "\n" + xhr.responseText);
                    }
                }
            });
        };
        AjaxRequest.UploadFile = function (data, onSuccess, suppressError) {
            if (suppressError === void 0) { suppressError = false; }
            data.submit()
                .done(function (data) {
                onSuccess(data);
            })
                .fail(function (xhr) {
                if (!suppressError) {
                    alert("Transfer failure: " + xhr.status.toString() + "\n" + xhr.responseText);
                }
            });
        };
        return AjaxRequest;
    }());
    exports.AjaxRequest = AjaxRequest;
    var PageLayout = /** @class */ (function () {
        function PageLayout() {
        }
        PageLayout.scrollTo = function (anchorId) {
            var pageTop = $("#" + anchorId);
            if (pageTop.length == 0) {
                pageTop = $("." + anchorId);
            }
            if (pageTop.length == 0) {
                pageTop = $("[data-id='" + anchorId + "']");
            }
            if (pageTop.length > 0 && !pageTop.parent().is(":hidden")) {
                window.setTimeout(function () {
                    $("html, body").animate({
                        scrollTop: pageTop.offset().top
                    }, 1000);
                }, 300);
            }
        };
        PageLayout.preventDoubleSubmission = function (formId, params) {
            var form = $("#" + formId);
            $(form).on("submit", function (e) {
                if ($(e.target).valid() && params.every(PageLayout.validateParams)) {
                    if ($(e.target).data("submitted") === true) {
                        e.preventDefault();
                        return false;
                    }
                    else {
                        $(e.target).data("submitted", true);
                    }
                }
            });
        };
        PageLayout.validateParams = function (value, index, array) {
            if (value == null)
                return true;
            return $("#" + value).is(":checked");
        };
        return PageLayout;
    }());
    exports.PageLayout = PageLayout;
    var ForgottenPassword = /** @class */ (function () {
        function ForgottenPassword() {
        }
        ForgottenPassword.onForgottenPassword = function (frmId, emailId) {
            var fplink = $("#" + frmId + " #fp");
            $(fplink).on("click", function (e) {
                $(e.target).attr("href", $(e.target).attr("href").replace("__email__", $("#" + frmId + " #" + emailId).val()));
            });
            $(fplink).on("onFpBegin", function (e, url, retinfo) {
                retinfo.retval = ForgottenPassword.onFpBegin(url, frmId, emailId);
            });
            $(fplink).on("onFpComplete", function (e, url) {
                ForgottenPassword.onFpComplete(url, frmId);
            });
        };
        ForgottenPassword.onFpBegin = function (url, frmId, emailId) {
            var v = $("#" + frmId + " #" + emailId).valid();
            if (!v) {
                $("#" + frmId + " #fp").attr("href", url);
            }
            return v;
        };
        ForgottenPassword.onFpComplete = function (url, frmId) {
            $("#" + frmId + " #fp").attr("href", url);
        };
        return ForgottenPassword;
    }());
    exports.ForgottenPassword = ForgottenPassword;
    var Basket = /** @class */ (function () {
        function Basket(basketId, panelId, itemKeyName, refreshUrl) {
            this.itemKeyName = itemKeyName;
            this.basketId = basketId;
            this.$basket = $("#" + this.basketId);
            var $body = $("body");
            $body.on("onPanelUpdate", function (e) {
                AjaxRequest.Post(refreshUrl, null, function (result) {
                    $("#" + panelId).replaceWith(result);
                });
            });
        }
        Basket.prototype.refresh = function (result) {
            this.$basket.html(result);
            this.onRemoveItem(this.removeItemUrl);
        };
        Basket.prototype.onRemoveItem = function (url) {
            var _this = this;
            this.removeItemUrl = url;
            this.$basket.find("input[data-id='remove']").on("click", function (event) {
                var postData = { id: $(event.target).data(_this.itemKeyName) };
                AjaxRequest.Post(url, postData, function (result) {
                    _this.refresh(result);
                    $("body").trigger("onPanelUpdate");
                });
            });
        };
        Basket.prototype.onAddItem = function (openerCss, dialogCss, url) {
            this.addItemUrl = url;
            this.refreshOnAddItem(openerCss, dialogCss);
        };
        Basket.prototype.refreshOnAddItem = function (openerCss, dialogCss) {
            var _this = this;
            var $opener = $("." + openerCss);
            $opener.off("click");
            $opener.on("click", function (e) {
                e.preventDefault();
                var $this = $(e.currentTarget);
                var $form = $this.closest("form");
                var rvt = $form.find("input[name='__RequestVerificationToken']").val();
                var data = {
                    __RequestVerificationToken: rvt
                };
                var tmpUrl = _this.addItemUrl;
                tmpUrl += "?Product=" + $this.data("product").toString();
                var units = $form.find("input[name='Units']").val();
                if (isNaN(parseInt(units))) {
                    return;
                }
                if (!extensions.StringExtensions.isNullOrEmpty(units)) {
                    if (tmpUrl.indexOf("?") > -1) {
                        tmpUrl += "&Units=" + units;
                    }
                    else {
                        tmpUrl += "?Units=" + units;
                    }
                }
                else {
                    return;
                }
                ui.BootboxDialog.Open(tmpUrl, "", data, function () {
                    var $body = $("body");
                    $body.trigger("onPanelUpdate");
                    $("." + dialogCss).find("#back").on("click", function (e) {
                        e.preventDefault();
                        bootbox.hideAll();
                    });
                }, null, null, null, dialogCss);
            });
        };
        return Basket;
    }());
    exports.Basket = Basket;
    var Registration = /** @class */ (function () {
        function Registration(frmId) {
            this.$frm = $("#" + frmId);
            this.aclicked = false;
        }
        Registration.prototype.checkAgreement = function () {
            this.$frm.on("submit", function (e) {
                var $checkbox1 = $(e.target).find("[name='RulesAgreement']");
                var $checkbox2 = $(e.target).find("[name='Agreement']");
                if (!$checkbox1.is(":checked") || !$checkbox2.is(":checked")) {
                    bootbox.alert("Je nutné potvrdiť súhlas s podmienkami súťaže.");
                    return false;
                }
            });
        };
        Registration.prototype.checkAgreementContact = function () {
            this.$frm.on("submit", function (e) {
                var $checkbox2 = $(e.target).find("[name='Agreement']");
                if (!$checkbox2.is(":checked")) {
                    bootbox.alert("Je nutné potvrdiť súhlas.");
                    return false;
                }
            });
        };
        Registration.prototype.checkAgreementAge = function () {
            this.$frm.on("submit", function (e) {
                var $checkbox1 = $(e.target).find("[name='Agreement']");
                if (!$checkbox1.is(":checked")) {
                    bootbox.alert("Je nutné potvrdiť, že ste fajčiar. Ak nie ste fajčiar, prosím opustite túto webovú stránku.");
                    return false;
                }
            });
        };
        Registration.prototype.showMessage = function (mesasge) {
            bootbox.alert(mesasge);
        };
        Registration.prototype.onAgreementClick = function (url) {
            var _this = this;
            var $checkbox2 = this.$frm.find("[name='Agreement']");
            var $agreement = this.$frm.find("#aagreement");
            $checkbox2.on("click", function (e) {
                var $this = $(e.target);
                if (!_this.aclicked && $this.is(":checked")) {
                    var aUrl = $agreement.attr("href");
                    var b = bootbox.alert("Pre pokračovanie je potrebné oboznámiť sa s informáciami o spracovávaní osobných údajov " +
                        "(<a href='" + aUrl + "' id='aHref' target='blank'>kliknite sem</a>).", function () {
                        if (!_this.aclicked)
                            $this.prop("checked", false);
                    });
                    var $aHref = $("#aHref");
                    $aHref.on("click", function (e) {
                        _this.aclicked = true;
                        b.modal('hide');
                    });
                }
            });
            $agreement.on("click", function (e) {
                _this.aclicked = true;
                $checkbox2.prop("checked", true);
                _this.$frm.find("input[type='hidden'][name='" + $checkbox2.attr("name") + "']").val("true");
                // ui.BootboxDialog.Open(url, "", null, null, null, null, true, "west-reginfo-dialog");
            });
        };
        Registration.prototype.onAgreementShow = function (url) {
            var $agreement = this.$frm.find("#aagreement");
            $agreement.on("click", function (e) {
                ui.BootboxDialog.Open(url, "", null, null, null, null, true, "west-reginfo-dialog");
            });
        };
        Registration.prototype.onCheckboxClick = function () {
            var _this = this;
            var $checkbox = this.$frm.find("[type='checkbox']");
            $checkbox.on("click", function (e) {
                var $this = $(e.target);
                _this.$frm.find("input[type='hidden'][name='" + $this.attr("name") + "']").val($this.is(":checked") ? "true" : "false");
            });
        };
        return Registration;
    }());
    exports.Registration = Registration;
    var Customers = /** @class */ (function () {
        function Customers() {
        }
        Customers.onCheckboxClick = function () {
            var $checkbox = $("#frmnewcustomer").find("[type='checkbox']");
            $checkbox.on("click", function (e) {
                var $this = $(e.target);
                $("#frmnewcustomer").find("input[type='hidden'][name='" + $this.attr("name") + "']").val($this.is(":checked") ? "true" : "false");
            });
        };
        Customers.prototype.onAddCode = function (btnCssClass, url) {
            this.openDialog(url, "Zadávanie kódov", btnCssClass, function () {
                $("#adddialog").off("newRegistration");
                $("#adddialog").on("newRegistration", function (e) {
                    $("#newreg").off("click");
                    $("#newreg").on("click", function (e) {
                        var $form = $(e.currentTarget).closest("form");
                        var $input = $form.find("input[type='text']");
                        $input.removeAttr("disabled");
                        $input.attr("value", "");
                        $form.find(".west-msg").html("");
                        $form.find("button[type='reset']").css("display", "none");
                        $form.find("button[type='submit']").css("display", "inline");
                    });
                });
                PageLayout.preventDoubleSubmission("frmaddcodes", [null, null]);
            });
        };
        Customers.prototype.onBuyTicket = function (btnCssClass, url) {
            this.openDialog(url, "Zadávanie lístkov", btnCssClass, null);
        };
        Customers.prototype.openDialog = function (url, title, btnCssClass, onOpen) {
            var $btn = $("." + btnCssClass);
            $btn.off("click");
            $btn.on("click", function (e) {
                e.preventDefault();
                var postData = {
                    customerId: $(e.currentTarget).closest("tr").data("id")
                };
                ui.BootboxDialog.Open(url, title, postData, onOpen, {
                    cancel: {
                        label: "Zavrieť"
                    }
                }, null, true);
            });
        };
        Customers.prototype.onNew = function (btnId, url) {
            var $btn = $("#" + btnId);
            $btn.off("click");
            $btn.on("click", function (e) {
                e.preventDefault();
                ui.BootboxDialog.Open(url, "Nový zákazník", null, null, {
                    cancel: {
                        label: "Zavrieť"
                    }
                }, null, true);
            });
        };
        Customers.prototype.onEdit = function (editCssClass, url) {
            var $td = $("." + editCssClass);
            $td.off("click");
            $td.on("click", function (e) {
                var postData = {
                    customerId: $(e.currentTarget).closest("tr").data("id")
                };
                ui.BootboxDialog.Open(url, "Editace zákazníka", postData, null, {
                    cancel: {
                        label: "Zavrieť"
                    }
                }, null, true);
            });
        };
        return Customers;
    }());
    exports.Customers = Customers;
});
//# sourceMappingURL=west.site.js.map
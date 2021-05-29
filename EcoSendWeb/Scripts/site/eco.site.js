define(["require", "exports", "./eco.site.ui", "bootbox"], function (require, exports, ui, bootbox) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.Parcel = exports.Account = exports.Categories = exports.AjaxRequest = void 0;
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
    var Categories = /** @class */ (function () {
        function Categories(containerId) {
            this.$container = $("#" + containerId);
        }
        Categories.prototype.onChangeCategory = function (tblContainer, btnCss, actStyle, url) {
            var $btns = this.$container.find("." + btnCss);
            $btns.each(function () {
                var $btn = $(this);
                var id = $btn.data("catid");
                $btn.off("click");
                $btn.on("click", function (e) {
                    $btns.each(function () {
                        if (id == $(this).data("catid")) {
                            if ($(this).hasClass(actStyle) == false) {
                                $(this).addClass(actStyle);
                                var data = {
                                    catid: id
                                };
                                AjaxRequest.Post(url, data, function (result) {
                                    $("#" + tblContainer).html(result);
                                });
                            }
                        }
                        else {
                            $(this).removeClass(actStyle);
                        }
                    });
                });
            });
        };
        return Categories;
    }());
    exports.Categories = Categories;
    var Account = /** @class */ (function () {
        function Account(containerId) {
            this.$container = $("#" + containerId);
        }
        Account.prototype.onLogout = function (btn, url) {
            var $btn = $("#" + btn);
            $btn.off("click");
            $btn.on("click", function () {
                bootbox.confirm({
                    message: "Are you sure that you want logout?",
                    buttons: {
                        confirm: {
                            label: 'Yes',
                            className: 'btn-success'
                        },
                        cancel: {
                            label: 'No',
                            className: 'btn-danger'
                        }
                    },
                    callback: function (result) {
                        if (result === true) {
                            AjaxRequest.Post(url, null, function () {
                                location.reload();
                            });
                        }
                    }
                });
            });
        };
        return Account;
    }());
    exports.Account = Account;
    var Parcel = /** @class */ (function () {
        function Parcel(containerId) {
            this.$container = $("#" + containerId);
        }
        Parcel.prototype.onShowInfo = function (btn, url, recycleUrl) {
            var $btn = this.$container.find("." + btn);
            $btn.off("click");
            $btn.on("click", function (e) {
                var $this = $(e.currentTarget);
                var parcelId = $this.data("parcel");
                var cat = $this.data("cat");
                var received = $this.data("received");
                var postData = {
                    Parcel: parcelId
                };
                var buttons;
                if (cat === 1 && received === "False") {
                    buttons = {
                        ok: {
                            label: "Receive",
                            className: "btn",
                            callback: function () {
                                var postData = {
                                    parcelId: parcelId,
                                    isPackRecycled: false
                                };
                                AjaxRequest.Post(recycleUrl, postData, function (result) {
                                    bootbox.alert(result.response);
                                    location.reload();
                                });
                            }
                        },
                        recycle: {
                            label: "Receive and recycle pack",
                            className: "btn btn-success",
                            callback: function () {
                                var postData = {
                                    parcelId: parcelId,
                                    isPackRecycled: true
                                };
                                AjaxRequest.Post(recycleUrl, postData, function (result) {
                                    bootbox.alert(result.response);
                                    location.reload();
                                });
                            }
                        },
                        cancel: {
                            label: "Close",
                            className: "btn",
                        }
                    };
                }
                else {
                    buttons = {
                        cancel: {
                            label: "Close",
                            className: "btn",
                        }
                    };
                }
                ui.BootboxDialog.Open(url, "Parcel info", postData, null, buttons, null, true);
            });
        };
        Parcel.prototype.onSendParcel = function (btn, createUrl, saveUrl) {
            var $btn = this.$container.find("#" + btn);
            $btn.off("click");
            $btn.on("click", function () {
                ui.BootboxDialog.Open(createUrl, "Send parcel", null, null, {
                    ok: {
                        label: "OK",
                        className: "btn btn-success",
                        callback: function () {
                            var postData = {
                                RecipientFirstName: $('#RecipientFirstName').val(),
                                RecipientLastName: $('#RecipientLastName').val(),
                                RecipientEmail: $('#RecipientEmail').val(),
                                DestCity: $('#DestCity').val(),
                                DestStreet: $('#DestStreet').val(),
                                DestPostalCode: $('#DestPostalCode').val(),
                                DestCountry: $('#DestCountry').val(),
                                PackType: $('#PackType').val(),
                                Points: $('#Points').val()
                            };
                            AjaxRequest.Post(saveUrl, postData, function (result) {
                                bootbox.alert(result.response);
                            });
                        }
                    },
                    cancel: {
                        label: "Close",
                        className: "btn",
                    }
                }, null, true);
            });
        };
        Parcel.prototype.onShowInfoManage = function (btn, url, approveUrl) {
            var $btn = this.$container.find("." + btn);
            $btn.off("click");
            $btn.on("click", function (e) {
                var $this = $(e.currentTarget);
                var parcelId = $this.data("parcel");
                var approved = $this.data("approved");
                var postData = {
                    Parcel: parcelId
                };
                var buttons;
                if (approved === "False") {
                    buttons = {
                        ok: {
                            label: "Approve",
                            className: "btn btn-success",
                            callback: function () {
                                AjaxRequest.Post(approveUrl, postData, function (result) {
                                    bootbox.alert(result.response);
                                    location.reload();
                                });
                            }
                        },
                        cancel: {
                            label: "Close",
                            className: "btn",
                        }
                    };
                }
                else {
                    buttons = {
                        cancel: {
                            label: "Close",
                            className: "btn",
                        }
                    };
                }
                ui.BootboxDialog.Open(url, "Parcel info", postData, null, buttons, null, true);
            });
        };
        return Parcel;
    }());
    exports.Parcel = Parcel;
});
//# sourceMappingURL=eco.site.js.map
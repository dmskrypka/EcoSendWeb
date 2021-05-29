define(["require", "exports", "./air.site.extensions", "./air.site.ui", "bootbox"], function (require, exports, extensions, ui, bootbox) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.Account = exports.Categories = exports.Action = exports.AirInfos = exports.AirInfoFinder = exports.PageLayout = exports.AjaxRequest = void 0;
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
    // TODO: naseptavac
    var AirInfoFinder = /** @class */ (function () {
        function AirInfoFinder(containerId, formId, url) {
            var _this = this;
            this.$input = $("#" + containerId);
            this.$frm = $("#" + formId);
            $.get(url, function (data) {
                _this.$input.typeahead({
                    source: data,
                    items: 'all'
                });
                _this.onChange(data);
            }, 'json');
        }
        AirInfoFinder.prototype.onChange = function (data) {
            var _this = this;
            this.$input.change(function () {
                var current = _this.$input.typeahead("getActive");
                _this.$input.data("pn", "");
                if (current) {
                    var value = _this.$input.val().toLowerCase();
                    // Some item from your model is active!
                    if (current.name.toLowerCase() == value) {
                        // This means the exact match is found. Use toLowerCase() if you want case insensitive match.
                        _this.$input.data("pn", current.id);
                    }
                    else {
                        // This means it is only a partial match, you can either add a new item
                        // or take the active if you don't want new items
                        _this.$input.data("pn", "");
                    }
                }
                else {
                    // Nothing is active so it is a new value (or maybe empty value)
                    _this.$input.data("pn", "");
                }
            });
        };
        AirInfoFinder.prototype.onSearch = function () {
            var _this = this;
            var $group = this.$frm.find(".input-group");
            this.$frm.find("button").on("click", function (e) {
                e.preventDefault();
                var user = _this.$input.data("pn");
                if (extensions.StringExtensions.isNullOrEmpty(user)) {
                    //user = "00000000-0000-0000-0000-000000000000"; // set default Guid value for show all users list
                    //$group.addClass("input-validation-error");
                    //return;
                    location.reload();
                }
                $group.removeClass("input-validation-error");
                var value = _this.$input.val();
                _this.$input.val(user);
                _this.$input.click();
                _this.$frm.submit();
                _this.$input.val(value);
            });
        };
        return AirInfoFinder;
    }());
    exports.AirInfoFinder = AirInfoFinder;
    var AirInfos = /** @class */ (function () {
        function AirInfos(airinfocontainer) {
            this.container = airinfocontainer;
            this.$container = $("#" + airinfocontainer);
        }
        AirInfos.prototype.onEditBase = function (btnEdit, saveUrl, updateUrl) {
            var _this = this;
            var $btn = $("#" + btnEdit);
            $btn.off("click");
            $btn.on("click", function (e) {
                var url = $btn.data("url");
                var id = $btn.data("id");
                var c = _this.container;
                var postData = { id: id };
                ui.BootboxDialog.Open(url, "Edit air info", postData, null, {
                    ok: {
                        label: "Save",
                        className: "btn",
                        callback: function () {
                            var pn = $('#PartNumber').val();
                            var airInfo = {
                                Id: id,
                                PartNumber: pn,
                                Description: $('#Description').val(),
                                MfgName: $('#MfgName').val(),
                                Segment: $('#Segment').val(),
                                ATA: $('#ATA').val(),
                                ABC: $('#ABC').val(),
                                Mod: $('#Mod').val(),
                                Aircraft: $('#Aircraft').val(),
                                ReparePrice: $('#ReparePrice').val(),
                                SuggestedPurchasePrice: $('#SuggestedPurchasePrice').val(),
                                TwlvMonthsTotalSales: $('#TwlvMonthsTotalSales').val(),
                                TwlvMonthsTotalNoBids: $('#TwlvMonthsTotalNoBids').val(),
                                TwlvMonthsTotalQuotes: $('#TwlvMonthsTotalQuotes').val(),
                                Conditions: [
                                    {
                                        ConditionType: 1,
                                        MinPrice: $('#MinPrice1').val(),
                                        MaxPrice: $('#MktPrice1').val(),
                                        Comment: $('#Comment1').val()
                                    },
                                    {
                                        ConditionType: 2,
                                        MinPrice: $('#MinPrice2').val(),
                                        MaxPrice: $('#MktPrice2').val(),
                                        Comment: $('#Comment2').val()
                                    },
                                    {
                                        ConditionType: 3,
                                        MinPrice: $('#MinPrice3').val(),
                                        MaxPrice: $('#MktPrice3').val(),
                                        Comment: $('#Comment3').val()
                                    }
                                ]
                            };
                            var savePostData = {
                                data: airInfo,
                                isNewData: false
                            };
                            var pnData = { pn: pn };
                            AjaxRequest.Post(saveUrl, savePostData, function (result) {
                                if (result.success == false) {
                                    bootbox.alert(result.response);
                                }
                                else {
                                    AjaxRequest.Post(updateUrl, pnData, function (result) {
                                        if (result.success == false) {
                                            bootbox.alert(result.response);
                                        }
                                        else {
                                            $('#' + c).html(result);
                                        }
                                    });
                                }
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
        AirInfos.prototype.onAdd = function (btnEdit, editUrl, saveUrl) {
            var $btn = $("#" + btnEdit);
            $btn.off("click");
            $btn.on("click", function (e) {
                var postData = { id: "00000000-0000-0000-0000-000000000000" };
                ui.BootboxDialog.Open(editUrl, "Edit air info", postData, null, {
                    ok: {
                        label: "Save",
                        className: "btn",
                        callback: function () {
                            var airInfo = {
                                PartNumber: $('#PartNumber').val(),
                                Description: $('#Description').val(),
                                MfgName: $('#MfgName').val(),
                                Segment: $('#Segment').val(),
                                ATA: $('#ATA').val(),
                                ABC: $('#ABC').val(),
                                Mod: $('#Mod').val(),
                                Aircraft: $('#Aircraft').val(),
                                ReparePrice: $('#ReparePrice').val(),
                                SuggestedPurchasePrice: $('#SuggestedPurchasePrice').val(),
                                TwlvMonthsTotalSales: $('#TwlvMonthsTotalSales').val(),
                                TwlvMonthsTotalNoBids: $('#TwlvMonthsTotalNoBids').val(),
                                TwlvMonthsTotalQuotes: $('#TwlvMonthsTotalQuotes').val(),
                                Conditions: [
                                    {
                                        ConditionType: 1,
                                        MinPrice: $('#MinPrice1').val(),
                                        MaxPrice: $('#MktPrice1').val(),
                                        Comment: $('#Comment1').val()
                                    },
                                    {
                                        ConditionType: 2,
                                        MinPrice: $('#MinPrice2').val(),
                                        MaxPrice: $('#MktPrice2').val(),
                                        Comment: $('#Comment2').val()
                                    },
                                    {
                                        ConditionType: 3,
                                        MinPrice: $('#MinPrice3').val(),
                                        MaxPrice: $('#MktPrice3').val(),
                                        Comment: $('#Comment3').val()
                                    }
                                ]
                            };
                            var postData = {
                                data: airInfo,
                                isNewData: true
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
        AirInfos.prototype.onDelete = function (btnDelete, deleteUrl) {
            var $btn = $("#" + btnDelete);
            $btn.off("click");
            $btn.on("click", function (e) {
                var postData = { id: $btn.data("id") };
                bootbox.confirm({
                    message: "Are you sure that you want to delete this air info data?",
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
                        if (result == true) {
                            AjaxRequest.Post(deleteUrl, postData, function (result) {
                                bootbox.alert(result.response, function () {
                                    location.reload();
                                });
                            });
                        }
                    }
                });
            });
        };
        AirInfos.prototype.onShowHistory = function (tblContainer, btnClass, url) {
            var $tbl = this.$container.find("#" + tblContainer);
            var $btns = $tbl.find("." + btnClass);
            $btns.each(function () {
                var $btn = $(this);
                var id = $btn.data("id");
                var ct = $btn.data("condition");
                $btn.off("click");
                $btn.on("click", function (e) {
                    var postData = {
                        airInfoId: id,
                        conditionType: ct
                    };
                    ui.BootboxDialog.Open(url, ct.toUpperCase() + " condition change history", postData, null, {
                        cancel: {
                            label: "Close",
                            className: "btn",
                        }
                    }, null, true, "condtitions-bootbox");
                });
            });
        };
        return AirInfos;
    }());
    exports.AirInfos = AirInfos;
    var Action = /** @class */ (function () {
        function Action(containerId, loader) {
            this.$container = $("#" + containerId);
            this.$loader = $("#" + loader);
        }
        Action.prototype.onHandleInputChanges = function (inputCss, inputAttr) {
            this.$container.find('.' + inputCss).each(function () {
                var $input = $(this), $label = $input.next('label'), labelVal = $label.html();
                $input.on('change', function (e) {
                    var fileName = '';
                    Action.formData = new FormData();
                    if (this.files && this.files.length > 1) {
                        fileName = (this.getAttribute(inputAttr) || '').replace('{count}', this.files.length);
                        for (var i = 0; i < this.files.length; i++) {
                            Action.formData.append("files", this.files[i]);
                        }
                    }
                    else if (this.files[0]) {
                        fileName = this.files[0].name;
                        Action.formData.append('files', this.files[0]);
                    }
                    else {
                        this.formData = null;
                    }
                    if (fileName)
                        $label.find('span').html(fileName);
                    else
                        $label.html(labelVal);
                });
                // Firefox bug fix
                $input
                    .on('focus', function () { $input.addClass('has-focus'); })
                    .on('blur', function () { $input.removeClass('has-focus'); });
            });
        };
        Action.prototype.onShowInfoImport = function (btnCss, urlAfterResult) {
            var _this = this;
            var $btn = this.$container.find("." + btnCss);
            $btn.off("click");
            $btn.on("click", function (e) {
                _this.onChangeLoaderVisibility(true);
                $.ajax(urlAfterResult, {
                    type: "POST",
                    data: Action.formData,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        this.onChangeLoaderVisibility(false);
                    },
                    error: function (xhr) {
                        alert("File export error: " + xhr.status.toString() + "\n" + xhr.responseText);
                        this.onChangeLoaderVisibility(false);
                    }
                });
                e.preventDefault();
                return false;
            });
        };
        Action.prototype.onShowInfoExport = function (btnCss, textareaCss, urlAfterResult) {
            var _this = this;
            var $btn = this.$container.find("." + btnCss);
            $btn.off("click");
            $btn.on("click", function (e) {
                var loader = _this.$loader;
                var text = $('#' + textareaCss).val();
                var data = { data: text };
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json;charset=UTF-8",
                    url: urlAfterResult,
                    beforeSend: function () {
                        loader.show();
                    },
                    //success: function (reponse) {
                    //    var blob = new Blob([reponse], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
                    //    var downloadUrl = URL.createObjectURL(blob);
                    //    var a = document.createElement("a");
                    //    a.href = downloadUrl;
                    //    a.download = "ReportFile.xlsx";
                    //    document.body.appendChild(a);
                    //    a.click();
                    //},
                    complete: function () {
                        loader.hide();
                    }
                });
                e.preventDefault();
                return false;
            });
        };
        Action.prototype.onShowInfoExportABC = function (btnCss, abcCss, urlAfterResult) {
            var _this = this;
            var $btn = this.$container.find("." + btnCss);
            $btn.off("click");
            $btn.on("click", function (e) {
                var loader = _this.$loader;
                var abc;
                $('.' + abcCss).find("input[name='abc']:checked").each(function () {
                    if (abc == undefined) {
                        abc = $(this).attr('id') + '|';
                    }
                    else {
                        abc += $(this).attr('id') + '|';
                    }
                });
                var data = { abcData: abc };
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json;charset=UTF-8",
                    url: urlAfterResult,
                    beforeSend: function () {
                        loader.show();
                    },
                    //success: function (reponse) {
                    //    var blob = new Blob([reponse], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
                    //    var downloadUrl = URL.createObjectURL(blob);
                    //    var a = document.createElement("a");
                    //    a.href = downloadUrl;
                    //    a.download = "ReportFile.xlsx";
                    //    document.body.appendChild(a);
                    //    a.click();
                    //},
                    complete: function () {
                        loader.hide();
                    }
                });
                e.preventDefault();
                return false;
            });
        };
        Action.prototype.onDownloadUsersUploads = function (tableId, btnClass, url) {
            var $tbl = this.$container.find("#" + tableId);
            var $btns = $tbl.find("." + btnClass);
            $btns.each(function () {
                var $btn = $(this);
                var id = $btn.data("id");
                var email = $btn.data("email");
                $btn.off("click");
                $btn.on("click", function (e) {
                    var postData = {
                        id: id,
                        email: email
                    };
                    AjaxRequest.Post(url, postData, function (result) { });
                    e.preventDefault();
                    return false;
                });
            });
        };
        Action.prototype.onChangeLoaderVisibility = function (isVisible) {
            if (isVisible) {
                this.$loader.show();
            }
            else {
                this.$loader.hide();
            }
        };
        return Action;
    }());
    exports.Action = Action;
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
                                    cType: id,
                                    airInfoId: $(this).data("airid")
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
        Categories.prototype.onEditCategory = function (tblContainer, btnCss, editStyle, saveStyle, url) {
            var $tbl = $("#" + tblContainer);
            var $btns = $tbl.find("." + btnCss);
            var $cont = this.$container;
            $btns.each(function () {
                var $btn = $(this);
                var $fa = $btn.find(".fa");
                $btn.off("click");
                $btn.on("click", function (e) {
                    if ($fa.hasClass(editStyle) == true) {
                        $fa.removeClass(editStyle);
                        $fa.addClass(saveStyle);
                        var id = $btn.data("cat");
                        var $row = $tbl.find("#cat-" + id);
                        $row.children().each(function () {
                            $(this).find("input").prop("readonly", false);
                        });
                    }
                    else {
                        var $actBtn = $cont.find(".btn-active");
                        var airId = $actBtn.data("airid");
                        var id = $btn.data("cat");
                        var $row = $tbl.find("#cat-" + id);
                        var catInfo = {
                            id: id,
                            CategoryType: $actBtn.data("catid"),
                            Company: $row.find("#Company").val(),
                            ConditionType: $row.find("#ConditionType").val(),
                            Date: $row.find("#StrDate").val(),
                            Qty: $row.find("#Qty").val(),
                            LtDays: $row.find("#LtDays").val(),
                            UnitPrice: $row.find("#UnitPrice").val(),
                            SerialNumber: $row.find("#SerialNumber").val(),
                            Comment: $row.find("#Comment").val()
                        };
                        var data = {
                            airInfoId: airId,
                            catInfo: catInfo
                        };
                        AjaxRequest.Post(url, data, function (result) {
                            if (result.success == false) {
                                bootbox.alert(result.response);
                                e.preventDefault();
                                return false;
                            }
                            else {
                                $tbl.html(result);
                            }
                        });
                    }
                });
            });
        };
        Categories.prototype.onDeleteCategory = function (tblContainer, btnCss, url) {
            var _this = this;
            var $tbl = $("#" + tblContainer);
            var $btn = $tbl.find("." + btnCss);
            $btn.off("click");
            $btn.on("click", function (e) {
                var $actBtn = _this.$container.find(".btn-active");
                var data = {
                    airInfoId: $actBtn.data("airid"),
                    catId: $btn.data("cat"),
                    cType: $actBtn.data("catid")
                };
                bootbox.confirm({
                    message: "Are you sure that you want to delete this category info data?",
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
                        if (result == true) {
                            AjaxRequest.Post(url, data, function (result) {
                                if (result.success == false) {
                                    bootbox.alert(result.response);
                                }
                                else {
                                    $tbl.html(result);
                                }
                            });
                        }
                    }
                });
            });
        };
        Categories.prototype.onAddCategoryData = function (tblContainer, btnCss, url) {
            var _this = this;
            var $tbl = $("#" + tblContainer);
            var $btn = $tbl.find("#" + btnCss);
            $btn.off("click");
            $btn.on("click", function (e) {
                var $actBtn = _this.$container.find(".btn-active");
                var id = $actBtn.data("airid");
                var catInfo = {
                    CategoryType: $actBtn.data("catid"),
                    Company: $('#cat-company').val(),
                    ConditionType: $('#cat-condition-type').val(),
                    Date: $('#cat-date').val(),
                    Qty: $('#cat-qty').val(),
                    LtDays: $('#cat-lt-days').val(),
                    UnitPrice: $('#cat-unit-price').val(),
                    SerialNumber: $('#cat-serial-number').val(),
                    Comment: $('#cat-comment').val()
                };
                var data = {
                    airInfoId: id,
                    catInfo: catInfo
                };
                AjaxRequest.Post(url, data, function (result) {
                    if (result.success == false) {
                        bootbox.alert(result.response);
                    }
                    else {
                        $tbl.html(result);
                    }
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
            $btn.on("click", function (e) {
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
                        if (result == true) {
                            AjaxRequest.Post(url, null, function (result) {
                                location.reload();
                            });
                        }
                    }
                });
            });
        };
        Account.prototype.onShowList = function (btn, title, url) {
            var $btn = $("#" + btn);
            $btn.off("click");
            $btn.on("click", function (e) {
                ui.BootboxDialog.Open(url, title, null, null, {
                    cancel: {
                        label: "Close",
                        className: "btn",
                    }
                }, null, true);
            });
        };
        Account.prototype.onValidationChange = function (dataRow, selector, url) {
            var $rows = this.$container.find("." + dataRow);
            $rows.each(function () {
                var id = $(this).data("id");
                var $selector = $(this).find("." + selector);
                $selector.off("change");
                $selector.on("change", function () {
                    var postData = {
                        id: id,
                        isValid: this.value
                    };
                    AjaxRequest.Post(url, postData, function (result) { });
                });
            });
        };
        return Account;
    }());
    exports.Account = Account;
});
//# sourceMappingURL=air.site.js.map
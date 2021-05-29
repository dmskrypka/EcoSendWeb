import extensions = require("./eco.site.extensions");
import ui = require("./eco.site.ui");
import bootbox = require("bootbox");

export class AjaxRequest {

    static Post(url: string, postData: any, onSuccess: (result: any) => void, suppressError: boolean = false): void {

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

    }

    static PostForm(url: string, postData: any, onSuccess: (result: any) => void, suppressError: boolean = false): void {

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

    }

    public static UploadFile(data: any, onSuccess: (result: any) => void, suppressError: boolean = false): void {

        data.submit()
            .done((data: any) => {
                onSuccess(data);
            })
            .fail((xhr: any) => {
                if (!suppressError) {
                    alert("Transfer failure: " + xhr.status.toString() + "\n" + xhr.responseText);
                }
            });

    }

}

export class PageLayout {

    public static scrollTo(anchorId: string): void {

        var pageTop: JQuery = $("#" + anchorId);

        if (pageTop.length == 0) {
            pageTop = $("." + anchorId);
        }

        if (pageTop.length == 0) {
            pageTop = $("[data-id='" + anchorId + "']");
        }

        if (pageTop.length > 0 && !pageTop.parent().is(":hidden")) {

            window.setTimeout(() => {

                $("html, body").animate({
                    scrollTop: pageTop.offset().top
                }, 1000);

            }, 300);

        }
    }

    public static preventDoubleSubmission(formId: string, params: any[]): void {

        var form: JQuery = $("#" + formId);

        $(form).on("submit", (e: JQueryEventObject) => {

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

    }

    private static validateParams(value: any, index: number, array: any[]): boolean {

        if (value == null)
            return true;

        return $("#" + value).is(":checked");

    }

}

// TODO: naseptavac
export class AirInfoFinder {

    private $input: JQuery;
    private $frm: JQuery;

    constructor(containerId: string, formId: string, url: string) {

        this.$input = $("#" + containerId);
        this.$frm = $("#" + formId);

        $.get(url, (data) => {

            this.$input.typeahead({
                source: data,
                items: <any>'all'
            });

            this.onChange(data);

        }, 'json');
    }

    private onChange(data): void {

        this.$input.change(() => {

            var current = this.$input.typeahead(<any>"getActive");

            this.$input.data("pn", "");

            if (current) {

                var value: string = this.$input.val().toLowerCase();

                // Some item from your model is active!
                if (current.name.toLowerCase() == value) {
                    // This means the exact match is found. Use toLowerCase() if you want case insensitive match.
                    this.$input.data("pn", current.id);
                } else {
                    // This means it is only a partial match, you can either add a new item
                    // or take the active if you don't want new items
                    this.$input.data("pn", "");
                }
            } else {
                // Nothing is active so it is a new value (or maybe empty value)
                this.$input.data("pn", "");
            }
        });

    }

    public onSearch(): void {

        var $group: JQuery = this.$frm.find(".input-group");

        this.$frm.find("button").on("click", (e: JQueryEventObject) => {

            e.preventDefault();

            var user: string = this.$input.data("pn");

            if (extensions.StringExtensions.isNullOrEmpty(user)) {

                //user = "00000000-0000-0000-0000-000000000000"; // set default Guid value for show all users list
                //$group.addClass("input-validation-error");
                //return;
                location.reload();
            }

            $group.removeClass("input-validation-error");

            var value: string = this.$input.val();
            this.$input.val(user);

            this.$input.click();

            this.$frm.submit();

            this.$input.val(value);

        });
    }
}

export class AirInfos {
    private container: string;
    private $container: JQuery;

    public constructor(airinfocontainer: string) {
        this.container = airinfocontainer;
        this.$container = $("#" + airinfocontainer);
    }

    public onEditBase(btnEdit: string, saveUrl: string, updateUrl: string): void {

        var $btn: JQuery = $("#" + btnEdit);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            var url: string = $btn.data("url");
            var id: string = $btn.data("id");
            var c = this.container;

            var postData = { id };

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

                        var savePostData =
                        {
                            data: airInfo,
                            isNewData: false
                        };

                        var pnData = { pn };

                        AjaxRequest.Post(saveUrl, savePostData, (result: any) => {
                            if (result.success == false) {
                                bootbox.alert(result.response);
                            }
                            else {
                                AjaxRequest.Post(updateUrl, pnData, (result: any) => {
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
    }

    public onAdd(btnEdit: string, editUrl: string, saveUrl: string): void {

        var $btn: JQuery = $("#" + btnEdit);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

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

                        var postData =
                        {
                            data: airInfo,
                            isNewData: true
                        };

                        AjaxRequest.Post(saveUrl, postData, (result: any) => {
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
    }

    public onDelete(btnDelete: string, deleteUrl: string): void {

        var $btn: JQuery = $("#" + btnDelete);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

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
                        AjaxRequest.Post(deleteUrl, postData, (result: any) => {
                            bootbox.alert(result.response, () => {
                                location.reload();
                            });
                        });
                    }
                }
            });
        });
    }

    public onShowHistory(tblContainer: string, btnClass: string, url: string): void {

        var $tbl = this.$container.find("#" + tblContainer);
        var $btns = $tbl.find("." + btnClass);

        $btns.each(function () {

            var $btn = $(this);
            var id = $btn.data("id");
            var ct = $btn.data("condition");

            $btn.off("click");
            $btn.on("click", (e: JQueryEventObject) => {

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
    }
}

export class Action {

    private $container: JQuery;
    private $loader: JQuery;
    private static formData: FormData;

    constructor(containerId: string, loader: string) {

        this.$container = $("#" + containerId);
        this.$loader = $("#" + loader);
    }

    public onHandleInputChanges(inputCss: string, inputAttr: string): void {

        this.$container.find('.' + inputCss).each(function () {
            var $input = $(this),
                $label = $input.next('label'),
                labelVal = $label.html();

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
    }

    public onShowInfoImport(btnCss: string, urlAfterResult: string): void {

        var $btn: JQuery = this.$container.find("." + btnCss);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            this.onChangeLoaderVisibility(true);

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
    }

    public onShowInfoExport(btnCss: string, textareaCss: string, urlAfterResult: string): void {

        var $btn: JQuery = this.$container.find("." + btnCss);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            var loader = this.$loader;

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

    }

    public onShowInfoExportABC(btnCss: string, abcCss: string, urlAfterResult: string): void {

        var $btn: JQuery = this.$container.find("." + btnCss);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            var loader = this.$loader;
            var abc: string;

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
    }

    public onDownloadUsersUploads(tableId: string, btnClass: string, url: string): void {

        var $tbl: JQuery = this.$container.find("#" + tableId);
        var $btns: JQuery = $tbl.find("." + btnClass);

        $btns.each(function () {

            var $btn = $(this);
            var id: string = $btn.data("id");
            var email: string = $btn.data("email");

            $btn.off("click");
            $btn.on("click", (e: JQueryEventObject) => {

                var postData = {
                    id,
                    email
                };

                AjaxRequest.Post(url, postData, (result: any) => { });

                e.preventDefault();
                return false;
            });
        });
    }

    public onChangeLoaderVisibility(isVisible: boolean) {
        if (isVisible) {
            this.$loader.show();
        }
        else {
            this.$loader.hide();
        }
    }
}

export class Categories {

    private $container: JQuery;

    constructor(containerId: string) {

        this.$container = $("#" + containerId);
    }

    public onChangeCategory(tblContainer: string, btnCss: string, actStyle: string, url: string): void {
        var $btns: JQuery = this.$container.find("." + btnCss);

        $btns.each(function () {
            var $btn = $(this);
            var id: string = $btn.data("catid");

            $btn.off("click");
            $btn.on("click", (e: JQueryEventObject) => {

                $btns.each(function () {

                    if (id == $(this).data("catid")) {
                        if ($(this).hasClass(actStyle) == false) {
                            $(this).addClass(actStyle);

                            var data = {
                                catid: id
                            };

                            AjaxRequest.Post(url, data,
                                (result: any) => {
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
    }

    public onEditCategory(tblContainer: string, btnCss: string, editStyle: string, saveStyle: string, url: string): void {
        var $tbl = $("#" + tblContainer);
        var $btns = $tbl.find("." + btnCss);
        var $cont = this.$container;

        $btns.each(function () {
            var $btn = $(this);
            var $fa = $btn.find(".fa");

            $btn.off("click");
            $btn.on("click", (e: JQueryEventObject) => {
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

                    var $actBtn: JQuery = $cont.find(".btn-active");
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

                    AjaxRequest.Post(url, data,
                        (result: any) => {
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
    }

    public onDeleteCategory(tblContainer: string, btnCss: string, url: string): void {

        var $tbl = $("#" + tblContainer);
        var $btn = $tbl.find("." + btnCss);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            var $actBtn: JQuery = this.$container.find(".btn-active");

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
                        AjaxRequest.Post(url, data,
                            (result: any) => {
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
    }

    public onAddCategoryData(tblContainer: string, btnCss: string, url: string): void {

        var $tbl = $("#" + tblContainer);
        var $btn = $tbl.find("#" + btnCss);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            var $actBtn: JQuery = this.$container.find(".btn-active");
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

            AjaxRequest.Post(url, data,
                (result: any) => {
                    if (result.success == false) {
                        bootbox.alert(result.response);
                    }
                    else {
                        $tbl.html(result);
                    }
                });
        });
    }
}

export class Account {

    private $container: JQuery;

    constructor(containerId: string) {

        this.$container = $("#" + containerId);
    }

    public onLogout(btn: string, url: string): void {

        var $btn: JQuery = $("#" + btn);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

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
                        AjaxRequest.Post(url, null, (result: any) => {
                            location.reload();
                        });
                    }
                }
            });
        });
    }

    public onShowList(btn: string, title: string, url: string): void {

        var $btn: JQuery = $("#" + btn);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            ui.BootboxDialog.Open(url, title, null, null, {
                cancel: {
                    label: "Close",
                    className: "btn",
                }
            }, null, true);

        });
    }

    public onValidationChange(dataRow: string, selector: string, url: string): void {

        var $rows = this.$container.find("." + dataRow);

        $rows.each(function () {

            var id = $(this).data("id");
            var $selector = $(this).find("." + selector);

            $selector.off("change");
            $selector.on("change", function () {

                var postData =
                {
                    id: id,
                    isValid: this.value
                };

                AjaxRequest.Post(url, postData, (result: any) => { });
            });
        });
    }
}

export class Parcel {

    private $container: JQuery;

    constructor(containerId: string) {

        this.$container = $("#" + containerId);

    }

    public onShowInfo(btn: string, url: string, recycleUrl: string): void {

        const $btn: JQuery = this.$container.find("." + btn);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            const $this: JQuery = $(e.currentTarget);
            const parcelId: string = $this.data("parcel");
            const cat = $this.data("cat");
            const received = $this.data("received");

            const postData = {
                Parcel: parcelId
            };

            let buttons;

            if (cat === 1 && received === "False") {
                buttons = {
                    ok: {
                        label: "Receive",
                        className: "btn",
                        callback: function () {

                            const postData = {
                                parcelId: parcelId,
                                isPackRecycled: false
                            };

                            AjaxRequest.Post(recycleUrl, postData, (result: any) => {
                                bootbox.alert(result.response);
                                location.reload();
                            });
                        }
                    },
                    recycle: {
                        label: "Receive and recycle pack",
                        className: "btn btn-success",
                        callback: function () {

                            const postData = {
                                parcelId: parcelId,
                                isPackRecycled: true
                            };

                            AjaxRequest.Post(recycleUrl, postData, (result: any) => {
                                bootbox.alert(result.response);
                                location.reload();
                            });
                        }
                    },
                    cancel: {
                        label: "Close",
                        className: "btn",
                    }
                }
            }
            else {
                buttons = {
                    cancel: {
                        label: "Close",
                        className: "btn",
                    }
                }
            }

            ui.BootboxDialog.Open(url, "Parcel info", postData, null, buttons, null, true);

        });
    }

    public onSendParcel(btn: string, createUrl: string, saveUrl: string): void {

        const $btn: JQuery = this.$container.find("#" + btn);

        $btn.off("click");
        $btn.on("click", () => {

            ui.BootboxDialog.Open(createUrl, "Send parcel", null, null, {
                ok: {
                    label: "OK",
                    className: "btn btn-success",
                    callback: function () {

                        const postData = {
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

                        AjaxRequest.Post(saveUrl, postData, (result: any) => {
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
    }

    public onShowInfoManage(btn: string, url: string, approveUrl: string): void {

        const $btn: JQuery = this.$container.find("." + btn);

        $btn.off("click");
        $btn.on("click", (e: JQueryEventObject) => {

            const $this: JQuery = $(e.currentTarget);
            const parcelId: string = $this.data("parcel");
            const approved = $this.data("approved");

            const postData = {
                Parcel: parcelId
            };

            let buttons;

            if (approved === "False") {
                buttons = {
                    ok: {
                        label: "Approve",
                        className: "btn btn-success",
                        callback: function () {

                            AjaxRequest.Post(approveUrl, postData, (result: any) => {
                                bootbox.alert(result.response);
                                location.reload();
                            });
                        }
                    },
                    cancel: {
                        label: "Close",
                        className: "btn",
                    }
                }
            }
            else {
                buttons = {
                    cancel: {
                        label: "Close",
                        className: "btn",
                    }
                }
            }

            ui.BootboxDialog.Open(url, "Parcel info", postData, null, buttons, null, true);

        });
    }
}

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
}

export class Account {

    public onLogout(btn: string, url: string): void {

        const $btn: JQuery = $("#" + btn);

        $btn.off("click");
        $btn.on("click", () => {

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
                        AjaxRequest.Post(url, null, () => {
                            location.reload();
                        });
                    }
                }
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

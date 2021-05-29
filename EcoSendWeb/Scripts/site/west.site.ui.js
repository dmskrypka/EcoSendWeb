define(["require", "exports", "./west.site.extensions", "./west.site", "bootbox"], function (require, exports, extensions, site, bootbox) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var BootboxDialog = /** @class */ (function () {
        function BootboxDialog() {
        }
        BootboxDialog.Open = function (url, title, postData, onOpen, buttons, html, animate, cssClass) {
            if (extensions.StringExtensions.isNullOrEmpty(html)) {
                site.AjaxRequest.PostForm(url, postData, function (result) {
                    bootbox.dialog({
                        title: title,
                        message: result,
                        buttons: buttons,
                        animate: (animate !== null) ? animate : true,
                        className: extensions.StringExtensions.isNullOrEmpty(cssClass) ? null : cssClass
                    });
                    if (onOpen)
                        onOpen();
                });
            }
            else {
                bootbox.dialog({
                    title: title,
                    message: html,
                    buttons: buttons,
                    animate: (animate !== null) ? animate : true,
                    className: extensions.StringExtensions.isNullOrEmpty(cssClass) ? null : cssClass
                });
                if (onOpen)
                    onOpen();
            }
        };
        return BootboxDialog;
    }());
    exports.BootboxDialog = BootboxDialog;
});
//# sourceMappingURL=west.site.ui.js.map
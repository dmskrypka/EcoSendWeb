define(["require", "exports", "./air.site.extensions", "./air.site", "bootbox"], function (require, exports, extensions, site, bootbox) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.BootboxDialog = void 0;
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
//# sourceMappingURL=air.site.ui.js.map
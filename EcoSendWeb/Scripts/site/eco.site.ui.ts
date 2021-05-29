import extensions = require("./eco.site.extensions");
import site = require("./eco.site");
import bootbox = require("bootbox");

export class BootboxDialog {

    public static Open(url: string, title: string, postData?: any, onOpen?: () => void, buttons?: any, html?: string, animate?: boolean, cssClass?: string): void {

        if (extensions.StringExtensions.isNullOrEmpty(html)) {

            site.AjaxRequest.PostForm(url, postData, (result: string) => {

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

    }

}
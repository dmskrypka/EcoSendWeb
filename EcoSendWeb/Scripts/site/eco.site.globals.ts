/// <amd-dependency path="date" />
/// <amd-dependency path="linq" />
/// <amd-dependency path="jquery-unobtrusive" />
/// <amd-dependency path="jquery-validate" />
/// <amd-dependency path="jquery-validate-globalize" />
/// <amd-dependency path="jquery-validate-unobtrusive" />
/// <amd-dependency path="jquery-slicknav" />
/// <amd-dependency path="bootstrap" />

/// <reference path="../typings/jquery.slicknav/jquery.slicknav.d.ts"/>

import jQuery = require("jquery");
import Globalize = require("./globalloader");

export module Global {

    export function setup() {

        var w = <any>window;
        w.Globalize = Globalize;
        w.parseDate = (<any>Globalize).parseDate;

        jQuery(function ($) {

            $("#airnavigation").slicknav({
                label: "",
                prependTo: "#wrapper > .container"
            });

        });
    }
} 
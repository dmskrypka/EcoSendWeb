/// <amd-dependency path="date" />
/// <amd-dependency path="linq" />
/// <amd-dependency path="jquery-unobtrusive" />
/// <amd-dependency path="jquery-validate" />
/// <amd-dependency path="jquery-validate-globalize" />
/// <amd-dependency path="jquery-validate-unobtrusive" />
/// <amd-dependency path="jquery-slicknav" />
/// <amd-dependency path="bootstrap" />
define(["require", "exports", "jquery", "./globalloader", "date", "linq", "jquery-unobtrusive", "jquery-validate", "jquery-validate-globalize", "jquery-validate-unobtrusive", "jquery-slicknav", "bootstrap"], function (require, exports, jQuery, Globalize) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var Global;
    (function (Global) {
        function setup() {
            var w = window;
            w.Globalize = Globalize;
            w.parseDate = Globalize.parseDate;
            jQuery(function ($) {
                $("#westnavigation").slicknav({
                    label: "",
                    prependTo: "#wrapper > .container"
                });
            });
        }
        Global.setup = setup;
    })(Global = exports.Global || (exports.Global = {}));
});
//# sourceMappingURL=west.site.globals.js.map
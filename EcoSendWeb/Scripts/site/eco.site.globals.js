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
    exports.Global = void 0;
    var Global;
    (function (Global) {
        function setup() {
            var w = window;
            w.Globalize = Globalize;
            w.parseDate = Globalize.parseDate;
            jQuery(function ($) {
                $("#airnavigation").slicknav({
                    label: "",
                    prependTo: "#wrapper > .container"
                });
            });
        }
        Global.setup = setup;
    })(Global = exports.Global || (exports.Global = {}));
});
//# sourceMappingURL=eco.site.globals.js.map
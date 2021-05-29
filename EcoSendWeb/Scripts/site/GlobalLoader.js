define(["require", "exports", "globalize", "jquery", "linq"], function (require, exports, Globalize, jQuery, Enumerable) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.load = void 0;
    function load(name, req, onload, config) {
        var cldrToLoad = ['supplemental/likelySubtags', 'main/{lang}/ca-gregorian', 'main/{lang}/numbers', 'supplemental/timeData'];
        var lang = window.config.locale.split("-")[0];
        var loads = Enumerable.From(cldrToLoad).Select(function (n) { return jQuery.get(req.toUrl("cldr/" + n.replace("{lang}", lang) + ".json")); }).ToArray();
        jQuery.when.apply(jQuery, loads).done(function () {
            for (var i = 0; i < arguments.length; ++i) {
                var data = arguments[i];
                Globalize.load(data[0]);
            }
            Globalize.locale(window.config.locale);
            onload(Globalize);
        });
    }
    exports.load = load;
});
//# sourceMappingURL=GlobalLoader.js.map
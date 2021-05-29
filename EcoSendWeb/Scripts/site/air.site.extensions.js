define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.ObjectExtensions = exports.StringExtensions = void 0;
    var StringExtensions = /** @class */ (function () {
        function StringExtensions() {
        }
        StringExtensions.isNullOrEmpty = function (s) {
            return !s || s.length == 0;
        };
        StringExtensions.splitKeyValue = function (input, separator) {
            input = input.trim();
            var pos = input.indexOf(separator);
            if (pos >= 0) {
                var len = separator.length;
                return {
                    key: input.substring(0, pos),
                    value: input.substr(pos + len)
                };
            }
            else {
                return {
                    key: input,
                    value: null
                };
            }
        };
        return StringExtensions;
    }());
    exports.StringExtensions = StringExtensions;
    var ObjectExtensions = /** @class */ (function () {
        function ObjectExtensions() {
        }
        ObjectExtensions.isEmptyObject = function (obj) {
            for (var name in obj) {
                if (obj.hasOwnProperty(name)) {
                    return false;
                }
            }
            return true;
        };
        return ObjectExtensions;
    }());
    exports.ObjectExtensions = ObjectExtensions;
});
//# sourceMappingURL=air.site.extensions.js.map
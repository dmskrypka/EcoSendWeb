import Globalize = require("globalize");
import jQuery = require("jquery");
import Enumerable = require("linq");

export function load(name: string, req, onload, config): void {
    var cldrToLoad: string[] = ['supplemental/likelySubtags', 'main/{lang}/ca-gregorian', 'main/{lang}/numbers', 'supplemental/timeData'];
    var lang: string = (<any>window).config.locale.split("-")[0];
    
    var loads: JQueryXHR[] = Enumerable.From(cldrToLoad).Select((n) => jQuery.get(req.toUrl("cldr/" + n.replace("{lang}", lang) + ".json"))).ToArray();
    jQuery.when.apply(jQuery, loads).done(function () {
        for (var i: number = 0; i < arguments.length; ++i) {
            var data = arguments[i];
            Globalize.load(data[0])
        }

        Globalize.locale((<any>window).config.locale);

        onload(Globalize);
    });


}
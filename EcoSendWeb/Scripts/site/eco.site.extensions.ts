import interfaces = require("./eco.site.interfaces");

export class StringExtensions {

    public static isNullOrEmpty(s: string): boolean {
        return !s || s.length == 0;
    }

    public static splitKeyValue(input: string, separator: string): interfaces.IStringKeyValue {

        input = input.trim();

        var pos: number = input.indexOf(separator);
        if (pos >= 0) {
            var len: number = separator.length;
            return <interfaces.IStringKeyValue>{
                key: input.substring(0, pos),
                value: input.substr(pos + len)
            };
        } else {
            return <interfaces.IStringKeyValue>{
                key: input,
                value: null
            };
        }

    }
}

export class ObjectExtensions {

    public static isEmptyObject(obj: any): boolean {

        for (var name in obj) {
            if (obj.hasOwnProperty(name)) {
                return false;
            }
        }

        return true;
    }

}




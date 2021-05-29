var require = {
    baseUrl: "/Scripts",
    shim: {
        "linq": {
            "exports": "Enumerable"
        },
        "jquery-validate": {
            "deps": ['jquery']
        },
        "jquery-validate-unobtrusive": {
            "deps": ['jquery-validate']
        },
        "jquery-validate-globalize": {
            "deps": ['jquery', 'globalize']
        },
        "jquery-unobtrusive": {
            "deps": ['jquery']
        },
        "bootstrap": {
            "deps": ['jquery']
        },
        "jquery-slicknav": {
            "deps": ['jquery']
        }
    },
    paths: {
        "jquery": "jquery-3.1.1",
        "jquery-validate": "jquery.validate",
        "jquery-validate-unobtrusive": "jquery.validate.unobtrusive",
        "jquery-unobtrusive": "jquery.unobtrusive-ajax",
        "jquery-validate-globalize": "jquery.validate.globalize",
        "jquery-slicknav": "jquery.slicknav",
        "date": "globalize/date",
        "number": "globalize/number"
    }
};

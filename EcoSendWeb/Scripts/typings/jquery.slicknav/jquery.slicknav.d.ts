interface Plugin {
    (): JQuery;
    (options: any): JQuery;
}

interface JQuery {
    slicknav: Plugin;
}
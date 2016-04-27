import {bootstrap}    from "angular2/platform/browser";
import {provide} from "angular2/core"
import {AppComponent} from "./appComponent";
import {ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy, APP_BASE_HREF, PathLocationStrategy} from "angular2/router"

bootstrap(AppComponent, [ROUTER_PROVIDERS,
    provide(APP_BASE_HREF, { useValue: '/' }),
    provide(LocationStrategy, { useClass: HashLocationStrategy })]); 
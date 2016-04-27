import {Component, provide} from "angular2/core"
import {RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from "angular2/router"
import { HTTP_PROVIDERS } from "angular2/http"
import {DashboardComponent, IVehicleServiceToken, StaticVehicleService, VehicleService, VehicleListComponent, VehicleComponent} from "./appCore"



@Component({
    selector: "my-app",
    directives: [ROUTER_DIRECTIVES, DashboardComponent],
    providers: [HTTP_PROVIDERS, provide(IVehicleServiceToken, { useClass: StaticVehicleService })],
    templateUrl: "./app/appComponent.html",
    styles: [
        `nav ul {list-style-type: none;}
        nav ul li {padding: 4px;display:inline-block}`
    ]
})
@RouteConfig([
    { path: "/dashboard", name: "Dashboard", component: DashboardComponent, useAsDefault: true },
    { path: "/vehicles", name: "Vehicles", component: VehicleListComponent }
    //{ path: "/vehicle/:id", name: "Vehicle", component: VehicleComponent }
])
export class AppComponent
{
    public changed(changedCharacter: IBaseData): void
    {
        const message = `Event changed: ${changedCharacter.name}`;
        toastr.success(message);
        console.log(message);
    }
}
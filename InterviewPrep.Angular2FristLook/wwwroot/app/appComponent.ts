import {Component} from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';
import {VehicleService} from "./shared/vehicleService"
import {DashboardComponent} from "./dashboard/dashboardComponent"

@Component({
    selector: "my-app",
    directives: [ROUTER_DIRECTIVES],
    providers: [VehicleService, ROUTER_PROVIDERS],
    template: "<router-outlet></router-outlet>"
})
@RouteConfig([
    { path: '/dashboard', name: 'Dashboard', component: DashboardComponent, useAsDefault: true },
])
export class AppComponent
{

}
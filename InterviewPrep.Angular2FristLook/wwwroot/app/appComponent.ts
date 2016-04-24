/// <reference path="../typings/browser.d.ts" />

import {Component} from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from 'angular2/router';
import {VehicleService} from "./shared/vehicleService"
import {DashboardComponent} from "./dashboard/dashboardComponent"
import { HTTP_PROVIDERS } from 'angular2/http';

@Component({
    selector: "my-app",
    directives: [ROUTER_DIRECTIVES],
    providers: [VehicleService, ROUTER_PROVIDERS, HTTP_PROVIDERS],
    template: "<router-outlet></router-outlet>"
})
@RouteConfig([
    { path: '/dashboard', name: 'Dashboard', component: DashboardComponent, useAsDefault: true },
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
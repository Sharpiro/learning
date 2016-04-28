import {Component} from "angular2/core"
import {RouteConfig, ROUTER_DIRECTIVES} from "angular2/router"
import {VehicleListComponent} from "./vehicleListComponent"
import {VehicleComponent} from "./vehicleComponent"


@Component({
    selector: "story-vehicles-root",
    template: "<router-outlet></router-outlet>",
    directives: ROUTER_DIRECTIVES
})
@RouteConfig([
    { path: "/", name: "Vehicles", component: VehicleListComponent, useAsDefault: true },
    { path: "/:id", name: "Vehicle", component: VehicleComponent }
])
export class VehiclesComponent { }
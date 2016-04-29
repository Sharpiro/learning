import {Component, OnInit, Inject, EventEmitter, Output} from "angular2/core"
import {ROUTER_DIRECTIVES, RouteConfig} from "angular2/router"
import {Observable} from "rxjs/rx"
import {IVehicleService, VehicleComponent } from "./vehicles"
import {NestedComponent, CustomPipe} from "../blocks/blocks"

@Component({
    selector: "vehicleList",
    templateUrl: "./app/vehicles/vehicleListComponent.html",
    directives: [ROUTER_DIRECTIVES, NestedComponent]
})
export class VehicleListComponent implements OnInit
{
    public vehicles: Observable<IBaseData[]>;
    public selectedVehicle: IBaseData;
    @Output() changed = new EventEmitter<IBaseData>();

    constructor( @Inject("IVehicleServiceToken") private dataService: IVehicleService) { }

    public ngOnInit(): void
    {
        this.vehicles = this.dataService.getVehicles();
    }

    public select(vehicle: IBaseData): void
    {
        this.selectedVehicle = vehicle;
        this.changed.emit(this.selectedVehicle);
    }
}
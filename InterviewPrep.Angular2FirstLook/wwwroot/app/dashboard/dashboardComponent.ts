import {Component, Output, EventEmitter, OnInit, Inject, Injectable, provide, OpaqueToken} from "angular2/core";
import {NestedComponent} from "../shared/nestedComponent"
import {CustomPipe} from "../shared/customPipe"
import {IVehicleService, IVehicleServiceToken} from "../interfaces/IVehicleService"
import {StaticVehicleService} from "../shared/staticVehicleService"
import {Observable} from "rxjs/Rx"

@Component({
    selector: "my-app",
    templateUrl: "./app/dashboard/dashboardComponent.html",
    styleUrls: ["./app/dashboard/dashboardComponent.css"],
    styles: [``],
    directives: [NestedComponent],
    pipes: [CustomPipe]
})
export class DashboardComponent implements OnInit
{
    public vehicles: Observable<IBaseData[]>;
    public selectedVehicle: IBaseData;
    public transformMe: string;
    @Output() changed = new EventEmitter<IBaseData>();
    private errorMessage: any;

    constructor( @Inject(IVehicleServiceToken) private dataService: IVehicleService)
    {
        this.transformMe = "data123";
    }

    public ngOnInit(): void
    {
        this.vehicles = this.dataService.getVehicles();
    }

    public select(vehicle: IBaseData): void
    {
        this.selectedVehicle = vehicle;
        this.changed.emit(this.selectedVehicle);
    }

    public setClasses(vehicle: IBaseData): any
    {
        const classes = { selected: true, list: true };
        return classes;
    }
}
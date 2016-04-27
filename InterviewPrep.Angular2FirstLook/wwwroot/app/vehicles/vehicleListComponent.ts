import {Component, OnInit, Inject, EventEmitter, Output} from "angular2/core"
import {Observable} from "rxjs/rx"
import {IVehicleService, NestedComponent} from "../appCore"
import {IVehicleServiceToken} from "../interfaces/IVehicleService"


@Component({
    selector: "vehicleList",
    templateUrl: "./app/vehicles/vehicleListComponent.html",
    directives: [NestedComponent]
})
export class VehicleListComponent implements OnInit
{
    public vehicles: Observable<IBaseData[]>;
    public selectedVehicle: IBaseData;
    @Output() changed = new EventEmitter<IBaseData>();

    constructor( @Inject(IVehicleServiceToken) private dataService: IVehicleService) { }

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
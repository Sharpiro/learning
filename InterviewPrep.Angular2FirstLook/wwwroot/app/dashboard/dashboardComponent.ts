import {Component, OnInit, Inject, EventEmitter, Output} from "angular2/core"
import {Observable} from "rxjs/Rx"
import {IVehicleService} from "../vehicles/vehicles"
import {CustomPipe, NestedComponent} from "../blocks/blocks"

@Component({
    selector: "my-app",
    templateUrl: "./app/dashboard/dashboardComponent.html",
    styleUrls: ["./app/dashboard/dashboardComponent.css"],
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

    constructor( @Inject("IVehicleServiceToken") private dataService: IVehicleService)
    {
        this.transformMe = "data123";
    }

    public ngOnInit(): void
    {
        console.log();
        this.vehicles = this.dataService.getVehicles();
    }

    public select(vehicle: IBaseData): void
    {
        this.selectedVehicle = vehicle;
        this.changed.emit(this.selectedVehicle);
    }
}
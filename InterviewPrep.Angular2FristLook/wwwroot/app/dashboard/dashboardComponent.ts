import {Component, Output, EventEmitter, OnInit, Inject, Injectable, provide, OpaqueToken} from "angular2/core";
import {NestedComponent} from "../shared/nestedComponent"
import {CustomPipe} from "../shared/customPipe"
import {IVehicleService} from "../interfaces/IVehicleService"
import {StaticVehicleService} from "../shared/staticVehicleService"
//import {IVEHICLESERVICE} from "../appComponent"

export let IVEHICLESERVICE = new OpaqueToken('StaticVehicleService');

@Component({
    selector: "my-app",
    templateUrl: "./app/dashboard/dashboardComponent.html",
    styleUrls: ["./app/dashboard/dashboardComponent.css"],
    styles: [``],
    providers: [provide(IVEHICLESERVICE, { useValue: StaticVehicleService })],
    directives: [NestedComponent],
    pipes: [CustomPipe]
})
@Injectable()
export class DashboardComponent implements OnInit
{
    public vehicles: Array<IBaseData>;
    public selectedVehicle: IBaseData;
    public transformMe: string;
    @Output() changed = new EventEmitter<IBaseData>();
    private errorMessage: any;

    //constructor( @Inject(APP_CONFIG) private _config: Config) { }

    constructor( @Inject(IVEHICLESERVICE) private dataService: IVehicleService)
    {
        this.transformMe = "data123";
    }

    public ngOnInit(): void
    {
        this.getVehicles();
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

    private getVehicles(): void
    {
        this.dataService.getVehicles().subscribe(
            vehicles => this.vehicles = vehicles,
            error => this.errorMessage = error
        );
    }
}
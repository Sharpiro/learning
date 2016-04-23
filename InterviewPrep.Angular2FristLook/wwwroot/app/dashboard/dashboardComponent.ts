import {Component} from "angular2/core";
import {VehicleService} from "../shared/vehicleService"
import {NestedComponent} from "../shared/nestedComponent"

@Component({
    selector: "my-app",
    templateUrl: "./app/dashboard/dashboardComponent.html",
    styleUrls: ["./app/dashboard/dashboardComponent.css"],
    directives: [NestedComponent]
})
export class DashboardComponent
{
    public vehicles: Array<IBaseData>;
    public selectedVehicle: IBaseData;

    constructor(private dataService: VehicleService)
    {
        dataService.getVehicles().subscribe(vehicles => this.vehicles = vehicles);
    }

    public select(vehicle: IBaseData): void
    {
        this.selectedVehicle = vehicle;
        console.log("vehicle selected");
    }

    public compare(item1: IBaseData, item2: IBaseData): boolean
    {
        return true;
    }
}
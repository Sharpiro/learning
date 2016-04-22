import {Component} from "angular2/core";
import {VehicleService} from "../shared/vehicleService"

@Component({
    selector: "my-app",
    templateUrl: "/app/dashboard/dashboardComponent.html",
})
export class DashboardComponent
{
    public vehicles: Array<IBaseData>;

    constructor(private dataService: VehicleService)
    {
        this.vehicles = dataService.getVehicles();
    }
}
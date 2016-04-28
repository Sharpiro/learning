﻿import {Component, Inject, OnInit, Input} from "angular2/core"
import {RouteParams, Router} from "angular2/router"
import {IVehicleService, IVehicleServiceToken} from "../interfaces/IVehicleService"


@Component({
    selector: "vehicle",
    templateUrl: "./app/vehicles/vehicleComponent.html"
})
export class VehicleComponent implements OnInit
{
    @Input() vehicle: IBaseData;

    constructor(private routeParams: RouteParams, @Inject(IVehicleServiceToken) private vehicleService: IVehicleService,
        private router: Router)
    {

    }

    public ngOnInit(): void
    {
        if (!this.vehicle)
        {
            let id = +this.routeParams.get("id");
            this.vehicleService.getVehicle(id).subscribe(vehicle => this.vehicle = vehicle);
        }
    }

    public goToVehicleList(): void
    {
        const route = ["Vehicles"]
        this.router.navigate(route);
    }
}
import {Component, OnInit, Inject, EventEmitter, Output, ViewChild} from "angular2/core"
import {ROUTER_DIRECTIVES, RouteConfig} from "angular2/router"
import {Observable} from "rxjs/rx"
import {IVehicleService, VehicleComponent } from "./vehicles"
import {NestedComponent, CustomPipe, FilterTextService} from "../blocks/blocks"
import {FilterTextComponent} from "../blocks/filterText/filterTextComponent"

@Component({
    selector: "vehicleList",
    templateUrl: "./app/vehicles/vehicleListComponent.html",
    directives: [FilterTextComponent, ROUTER_DIRECTIVES, NestedComponent],
    providers: [FilterTextService]
})
export class VehicleListComponent implements OnInit
{
    public vehicles: IBaseData[];
    public filteredVehicles: IBaseData[];
    public selectedVehicle: IBaseData;
    @Output() changed = new EventEmitter<IBaseData>();
    @ViewChild(FilterTextComponent) filterComponent: FilterTextComponent;

    constructor( @Inject("IVehicleServiceToken") private dataService: IVehicleService,
        private filterService: FilterTextService) { }

    public ngOnInit(): void
    {
        this.getVehicles();
    }

    public select(vehicle: IBaseData): void
    {
        var temp = this.filterComponent;
        this.selectedVehicle = vehicle;
        this.changed.emit(this.selectedVehicle);
    }

    public getVehicles()
    {
        this.dataService.getVehicles().subscribe(vehicles =>
        {
            this.filteredVehicles = this.vehicles = vehicles
            if (this.filterComponent)
                this.filterComponent.clear();
        });
    }

    public filterListComponentChanged(searchText: string): void
    {
        this.filteredVehicles = this.filterService.filter(searchText, ['id', 'name', 'type'], this.vehicles);
    }
}
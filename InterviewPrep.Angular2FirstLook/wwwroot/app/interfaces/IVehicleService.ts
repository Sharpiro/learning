import {Observable} from "rxjs/Rx" // load the full rxjs
import {OpaqueToken} from "angular2/core"

export interface IVehicleService
{
    getVehicles(): Observable<IBaseData[]>;
    getVehicle(id: number): Observable<IBaseData>;
}

export let IVehicleServiceToken = new OpaqueToken("IVehicleService");
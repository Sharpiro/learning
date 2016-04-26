import {Observable} from "rxjs/Rx" // load the full rxjs

export interface IVehicleService
{
    getVehicles(): Observable<IBaseData[]>;
}
import {Inject, Injectable} from 'angular2/core';
import { Http, Response } from 'angular2/http';
import 'rxjs/Rx'; // load the full rxjs
import {Observable} from "rxjs/Rx" // load the full rxjs
import {IVehicleService} from "../interfaces/IVehicleService"

@Injectable()
export class StaticVehicleService implements IVehicleService
{
    constructor(private httpService: Http) { }

    public static test = "test data";

    public getVehicles(): Observable<IBaseData[]>
    {
        var promise = this.httpService.get("/content/json/vehicles.json")
            .map((response: Response) => <IBaseData[]>(response.json().data))
            .catch(this.handleError);
        return promise;
    }

    private handleError(error: Response): Observable<any>
    {
        console.error(error);
        var obs = Observable.throw(error.json().error || "Server Error");
        return obs;
    }
}
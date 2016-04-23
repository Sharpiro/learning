import {Inject, Injectable} from 'angular2/core';
import { Http, Response } from 'angular2/http';
import 'rxjs/Rx'; // load the full rxjs
import {Observable} from "rxjs/Rx" // load the full rxjs

@Injectable()
export class VehicleService
{
    constructor(private httpService: Http)
    {

    }

    public getVehicles(): Observable<any>
    {
        var promise = this.httpService.get("/content/json/vehicles.json")
            .map((response: Response) => response.json().data);
        return promise;
    }
}
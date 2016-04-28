import {Injectable} from 'angular2/core';
import { Http, Response } from 'angular2/http';
import 'rxjs/Rx';
import {Observable} from "rxjs/Rx"
import {IVehicleService} from "../appCore"

@Injectable()
export class StaticVehicleService implements IVehicleService
{
    constructor(private httpService: Http) { }

    public getVehicles(): Observable<IBaseData[]>
    {
        var promise = this.httpService.get("/content/json/vehicles.json")
            .map((response: Response) => <IBaseData[]>(response.json().data))
            .do(data => console.log())
            .catch(this.handleError);
        return promise;
    }

    public getVehicle(id: number): Observable<IBaseData>
    {
        var promise = this.getVehicles().map(vehicles => vehicles.find(vehicle => vehicle.id === id));
        return promise;
    }

    private handleError(error: Response): Observable<any>
    {
        console.error(error);
        var obs = Observable.throw("Server Error");
        return obs;
    }
}
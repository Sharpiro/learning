import {Injectable} from "angular2/core"
import {Http} from "angular2/http"
import {IVehicleService} from "../interfaces/IVehicleService"
import 'rxjs/Rx';
import {Observable} from "rxjs/Rx"

@Injectable()
export class VehicleService implements IVehicleService
{
    constructor(private httpService: Http) { }

    public getVehicles(): Observable<IBaseData[]>
    {
        var obs = this.httpService.get("/api/vehicles/getdata")
            .map(responese => <IBaseData[]>(responese.json().data));
        return obs;
    }
}
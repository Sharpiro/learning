import {Injectable} from 'angular2/core';

@Injectable()
export class VehicleService
{
    public getVehicles(): Array<IBaseData>
    {
        const data: Array<IBaseData> = [
            { id: 1, name: "X-Wing Fighter" },
            { id: 2, name: "Tie Fighter" },
            { id: 3, name: "Y-Wing Fighter" }
        ];
        return data;
    }
}
import {Component} from 'angular2/core';

@Component({
    selector: "my-app",
    templateUrl: "/app/templates/homeComponent.html"
})
export class HomeComponent
{
    public data: Array<IData> = [
        { value: 12 }, { value: 13 }, { value: 14 }
    ];
}

interface IData
{
    value: number;
}

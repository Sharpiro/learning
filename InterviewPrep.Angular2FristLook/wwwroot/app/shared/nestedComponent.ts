import {Component, Input} from "angular2/core"

@Component({
    selector: "nested",
    template: `<h3>My vehicle is: {{vehicle.name}}</h3>`
})
export class NestedComponent
{
    @Input() vehicle: IBaseData;
}
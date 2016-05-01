import { Injectable } from "angular2/core";
import { Observable, Subject } from "rxjs/Rx";

export interface ISpinnerState
{
    show: boolean
}

@Injectable()
export class SpinnerService
{
    private spinnerSubject: Subject<ISpinnerState> = new Subject();

    public spinnerState = this.spinnerSubject.asObservable();

    show()
    {
        this.spinnerSubject.next(<ISpinnerState>{ show: true });
    }

    hide()
    {
        this.spinnerSubject.next(<ISpinnerState>{ show: false });
    }
}
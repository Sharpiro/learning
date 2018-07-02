import { Injectable } from '@angular/core';

@Injectable()
export class UtilitiesService {
  constructor() { }

  public delay(ms: number) {
    return new Promise<void>((resolve) => {
      setTimeout(resolve, ms);
    });
  }
}
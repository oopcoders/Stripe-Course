import { Injectable } from '@angular/core';
import { NgProgressRef } from 'ngx-progressbar';

@Injectable({
  providedIn: 'root',
})
export class ProgressBarService {
  progressRef: NgProgressRef;

  constructor() {}

  startLoading() {
    this.progressRef.start();
  }

  completeLoading() {
    this.progressRef.complete();
  }
}

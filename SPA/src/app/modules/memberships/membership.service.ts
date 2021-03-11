import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IMemberShipPlan } from './IMembership';

@Injectable({
  providedIn: 'root',
})
export class MembershipService {
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getMembership(): Observable<IMemberShipPlan> {
    return of({
      id: '',
      priceId: '',
      name: 'Awesome Membership Plan',
      price: '$9.00',
      features: [
        'Up to 5 users',
        'Basic support on Github',
        'Monthly updates',
        'Free cancelation',
      ],
    });
  }

  requestMemberSession(priceId: string): void {
    console.log(priceId);
  }

  redirectToCheckout(sessionId: string) {}
}

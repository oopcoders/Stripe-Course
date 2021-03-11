import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { IMemberShipPlan } from '../IMembership';
import { MembershipService } from '../membership.service';

@Component({
  selector: 'app-membership-checkout',
  templateUrl: './membership-checkout.component.html',
  styleUrls: ['./membership-checkout.component.scss'],
})
export class MembershipCheckoutComponent implements OnInit {
  $membership: Observable<IMemberShipPlan>;
  constructor(private membershipService: MembershipService) {}

  ngOnInit(): void {
    this.$membership = this.membershipService.getMembership();
  }

  onSubmit(f: NgForm) {
    this.membershipService.requestMemberSession(f.value.priceId);
  }
}

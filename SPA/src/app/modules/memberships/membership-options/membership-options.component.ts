import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IMemberShipPlan } from '../IMembership';
import { MembershipService } from '../membership.service';

@Component({
  selector: 'app-membership-options',
  templateUrl: './membership-options.component.html',
  styleUrls: ['./membership-options.component.scss'],
})
export class MembershipOptionsComponent implements OnInit {
  $membership: Observable<IMemberShipPlan>;
  constructor(private membershipService: MembershipService) {}

  ngOnInit(): void {
    this.$membership = this.membershipService.getMembership();
  }
}

import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { MembershipCheckoutComponent } from './membership-checkout/membership-checkout.component';
import { MembershipFailureComponent } from './membership-failure/membership-failure.component';
import { MembershipOptionsComponent } from './membership-options/membership-options.component';
import { MembershipSuccessComponent } from './membership-success/membership-success.component';

const routes: Routes = [
  { path: 'memberships', component: MembershipOptionsComponent },
  { path: 'checkout', component: MembershipCheckoutComponent },
  { path: 'success', component: MembershipSuccessComponent },
  { path: 'failure', component: MembershipFailureComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MembershipsModuleRoutes {}

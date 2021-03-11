import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MembershipCheckoutComponent } from './membership-checkout/membership-checkout.component';
import { MembershipOptionsComponent } from './membership-options/membership-options.component';
import { MembershipsModuleRoutes } from './memberships-routing.module';
import { MembershipFailureComponent } from './membership-failure/membership-failure.component';
import { MembershipSuccessComponent } from './membership-success/membership-success.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MembershipCheckoutComponent,
    MembershipOptionsComponent,
    MembershipFailureComponent,
    MembershipSuccessComponent,
  ],
  imports: [
    CommonModule,
    MembershipsModuleRoutes,
    HttpClientModule,
    FormsModule,
  ],
  exports: [
    MembershipOptionsComponent,
    MembershipFailureComponent,
    MembershipSuccessComponent,
  ],
})
export class MembershipsModule {}

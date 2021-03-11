import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { HeaderComponent } from './core/header/header.component';
import { CourseListComponent } from './pages/course-list/course-list.component';
import { FooterComponent } from './core/footer/footer.component';
import { MembershipsModule } from './modules/memberships/memberships.module';
import { PrivacyPolicyComponent } from './pages/privacy-policy/privacy-policy.component';
import { TermsComponent } from './pages/terms/terms.component';
import { AuthModule } from './modules/auth/auth.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertModule } from 'ngx-alerts';
import { NgProgressModule } from 'ngx-progressbar';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    CourseListComponent,
    FooterComponent,
    PrivacyPolicyComponent,
    TermsComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    MembershipsModule,
    AppRoutingModule,
    RouterModule,
    AuthModule,
    AlertModule.forRoot({ maxMessages: 5, timeout: 5000 }),
    NgProgressModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}

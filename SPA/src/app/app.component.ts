import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './modules/auth/auth.service';
import { IUser } from './modules/auth/IAuth';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  helper = new JwtHelperService();
  title = 'Stripe Course';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    const token = localStorage.getItem('token');

    const isTokenexpired = this.helper.isTokenExpired(token);

    if (token && !isTokenexpired) {
      const decodedToken = this.helper.decodeToken(token);
      this.authService.currentUser.username = decodedToken.given_name;
      this.authService.currentUser.email = decodedToken.email;
      if (decodedToken.isSubscriber == 'True') {
        this.authService.currentUser.isSubscriber = true;
      }
    } else {
      localStorage.removeItem('token');
      this.authService.currentUser.username = null;
      this.authService.currentUser.email = null;
      this.authService.currentUser.isSubscriber = false;
    }
  }
}

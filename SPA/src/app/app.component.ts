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

    if (token) {
      const decodedToken = this.helper.decodeToken(token);
      this.authService.currentUser.username = decodedToken.given_name;
      this.authService.currentUser.email = decodedToken.email;
    }
  }
}

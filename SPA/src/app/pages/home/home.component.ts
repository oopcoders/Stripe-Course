import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/modules/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  isLoggedIn(): boolean {
    return this.authService.currentUser.username != null ? true : false;
  }
}

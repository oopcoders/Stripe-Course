import { Component, OnInit } from '@angular/core';
import { NgProgress } from 'ngx-progressbar';
import { AuthService } from 'src/app/modules/auth/auth.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  constructor(
    private progress: NgProgress,
    public progressBar: ProgressBarService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.progressBar.progressRef = this.progress.ref('progressBar');
  }

  isLoggedIn(): boolean {
    return this.authService.currentUser.username != null ? true : false;
  }
}

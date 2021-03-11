import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AlertService } from 'ngx-alerts';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    public progressBar: ProgressBarService,
    private authService: AuthService,
    private alertService: AlertService
  ) {}

  ngOnInit(): void {}

  onSubmit(f: NgForm) {
    this.alertService.info('Check login information');
    this.progressBar.startLoading();

    const loginObserver = {
      next: (x) => {
        this.alertService.success('Welcome back ' + x.username);
        this.progressBar.completeLoading();
        setTimeout(() => {
          this.alertService.info(x.email);
        }, 4000);
      },
      error: (err) => {
        console.log(err);
        this.alertService.danger('Unable to Login');
        this.progressBar.completeLoading();
      },
    };

    this.authService.login(f.value).subscribe(loginObserver);
  }
}

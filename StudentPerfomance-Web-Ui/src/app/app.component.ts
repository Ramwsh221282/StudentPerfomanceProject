import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { setTheme } from 'ngx-bootstrap/utils';
import { SideBarComponent } from './shared/components/side-bar/side-bar.component';
import { SuccessResultNotificationComponent } from './shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from './shared/components/failure-result-notification/failure-result-notification.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    SideBarComponent,
    RouterLink,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  constructor() {
    setTheme('bs5');
  }
}

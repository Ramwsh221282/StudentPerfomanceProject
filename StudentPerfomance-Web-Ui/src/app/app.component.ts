import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { setTheme } from 'ngx-bootstrap/utils';
import { SideBarComponent } from './shared/components/side-bar/side-bar.component';
import { ModalDialogComponent } from './shared/components/modal-dialog/modal-dialog.component';
import { SuccessResultNotificationComponent } from './shared/components/success-result-notification/success-result-notification.component';
import { FailureNotificationFormComponent } from './shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { FailureResultNotificationComponent } from './shared/components/failure-result-notification/failure-result-notification.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    SideBarComponent,
    RouterLink,
    ModalDialogComponent,
    SuccessResultNotificationComponent,
    FailureNotificationFormComponent,
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

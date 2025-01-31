import { Component } from '@angular/core';
import { UserCardComponent } from './user-card/user-card.component';
import { AuthService } from './services/auth.service';
import { UserOperationNotificationService } from '../../shared/services/user-notifications/user-operation-notification-service.service';
import { AdminUserShortInfoComponent } from './admin-user-short-info/admin-user-short-info.component';
import { NgIf } from '@angular/common';
import { AdminControlWeekStatusComponent } from './admin-user-short-info/admin-control-week-status/admin-control-week-status.component';
import { TeacherUserShortInfoComponent } from './teacher-user-short-info/teacher-user-short-info.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [
    UserCardComponent,
    AdminUserShortInfoComponent,
    NgIf,
    AdminControlWeekStatusComponent,
    TeacherUserShortInfoComponent,
  ],
  templateUrl: './user-page.component.html',
  styleUrl: './user-page.component.scss',
  providers: [UserOperationNotificationService],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class UserPageComponent {
  public constructor(protected readonly authService: AuthService) {}
}

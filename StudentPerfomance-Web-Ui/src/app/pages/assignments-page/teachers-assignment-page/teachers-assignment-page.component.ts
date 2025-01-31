import { Component, OnInit } from '@angular/core';
import { TeacherAssignmentsDataService } from '../../user-page/teacher-user-short-info/teacher-assignments-data.service';
import { AvailableJournalsComponent } from './available-journals/available-journals.component';
import { TeacherAssignmentPageViewmodel } from './teacher-assignment-page.viewmodel';
import { NotificationService } from '../../../building-blocks/notifications/notification.service';
import { AvailableDisciplinesComponent } from './available-disciplines/available-disciplines.component';
import { NgIf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';
import { AssignmentJournalComponent } from './assignment-journal/assignment-journal.component';
import { SuccessNotificationComponent } from '../../../building-blocks/notifications/success-notification/success-notification.component';
import { FailureNotificationComponent } from '../../../building-blocks/notifications/failure-notification/failure-notification.component';
import { CompletedAssignmentsNotificationComponent } from './assignment-journal/completed-assignments-notification/completed-assignments-notification.component';
import { AuthService } from '../../user-page/services/auth.service';
import { AdminAssignmentsAccessResolverDialogComponent } from './admin-assignments-access-resolver-dialog/admin-assignments-access-resolver-dialog.component';
import { Router } from '@angular/router';
import { AdminAccessResponse } from './admin-assignments-access-resolver-dialog/admin-assignments-access.service';
import { AdminAssignmentsNotCompletedPopupComponent } from './admin-assignments-not-completed-popup/admin-assignments-not-completed-popup.component';

@Component({
  selector: 'app-teachers-assignment-page',
  templateUrl: './teachers-assignment-page.component.html',
  styleUrl: './teachers-assignment-page.component.scss',
  standalone: true,
  imports: [
    AvailableJournalsComponent,
    AvailableDisciplinesComponent,
    NgIf,
    AssignmentJournalComponent,
    SuccessNotificationComponent,
    FailureNotificationComponent,
    CompletedAssignmentsNotificationComponent,
    AdminAssignmentsAccessResolverDialogComponent,
    AdminAssignmentsNotCompletedPopupComponent,
  ],
  providers: [TeacherAssignmentPageViewmodel, NotificationService],
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
export class TeachersAssignmentPageComponent implements OnInit {
  public isFinished: boolean = false;
  public isNotFullyCompleted: boolean = false;
  public adminAccessRequest: boolean = false;

  public constructor(
    private readonly _service: TeacherAssignmentsDataService,
    protected readonly _viewModel: TeacherAssignmentPageViewmodel,
    protected readonly notifications: NotificationService,
    protected readonly authService: AuthService,
    private readonly _router: Router,
  ) {}

  public ngOnInit() {
    this._service.getTeacherAssignmentsInfo().subscribe((response) => {
      this._viewModel.initialize(response.journals);
    });
    this.adminAccessRequest = this.authService.userData.role == 'Администратор';
  }

  public handleAdminAccessClose(): void {
    this.adminAccessRequest = false;
  }

  public handleAdminAccessGiven(access: AdminAccessResponse): void {
    this._service.getTeacherAssignmentsInfo(access).subscribe((response) => {
      this._viewModel.initialize(response.journals);
    });
  }

  public handleFinish(): void {
    this._viewModel.handleFinish();
  }
}

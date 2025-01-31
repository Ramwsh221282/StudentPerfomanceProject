import { Component } from '@angular/core';
import { FailureNotificationComponent } from '../../building-blocks/notifications/failure-notification/failure-notification.component';
import { NgIf } from '@angular/common';
import { SuccessNotificationComponent } from '../../building-blocks/notifications/success-notification/success-notification.component';
import { NotificationService } from '../../building-blocks/notifications/notification.service';
import { StudentGroupsListComponent } from './student-groups-list/student-groups-list.component';
import { StudentsListComponent } from './students-list/students-list.component';
import { StudentGroupPlanInfoComponent } from './student-group-plan-info/student-group-plan-info.component';
import { StudentPageViewModel } from './student-page-viewmodel.service';
import { StudentGroup } from '../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { AttachPlanGroupDialogComponent } from './student-groups-list/attach-plan-group-dialog/attach-plan-group-dialog.component';
import { DetachPlanGroupDialogComponent } from './student-groups-list/detach-plan-group-dialog/detach-plan-group-dialog.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-students-page',
  imports: [
    FailureNotificationComponent,
    NgIf,
    SuccessNotificationComponent,
    StudentGroupsListComponent,
    StudentsListComponent,
    StudentGroupPlanInfoComponent,
    AttachPlanGroupDialogComponent,
    DetachPlanGroupDialogComponent,
  ],
  templateUrl: './students-page.component.html',
  styleUrl: './students-page.component.scss',
  standalone: true,
  providers: [NotificationService, StudentPageViewModel],
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
export class StudentsPageComponent {
  public attachGroupPlanRequest: StudentGroup | null = null;
  public detachGroupPlanRequest: StudentGroup | null = null;

  public constructor(
    public readonly notifications: NotificationService,
    public readonly viewModel: StudentPageViewModel,
  ) {}
}

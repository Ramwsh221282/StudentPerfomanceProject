import { Component } from '@angular/core';
import { DisciplinesListComponent } from '../education-objects-page/disciplines-list/disciplines-list.component';
import { EducationDirectionsInlineListComponent } from '../education-objects-page/education-directions-inline-list/education-directions-inline-list.component';
import { EducationPlanDataListComponent } from '../education-objects-page/education-plan-data-list/education-plan-data-list.component';
import { EducationPlanSemesterListComponent } from '../education-objects-page/education-plan-semester-list/education-plan-semester-list.component';
import { FailureNotificationComponent } from '../../building-blocks/notifications/failure-notification/failure-notification.component';
import { NgIf } from '@angular/common';
import { SuccessNotificationComponent } from '../../building-blocks/notifications/success-notification/success-notification.component';
import { NotificationService } from '../../building-blocks/notifications/notification.service';
import { StudentGroupsListComponent } from './student-groups-list/student-groups-list.component';
import { StudentsListComponent } from './students-list/students-list.component';
import { StudentGroupPlanInfoComponent } from './student-group-plan-info/student-group-plan-info.component';
import { StudentPageViewModel } from './student-page-viewmodel.service';

@Component({
  selector: 'app-students-page',
  imports: [
    DisciplinesListComponent,
    EducationDirectionsInlineListComponent,
    EducationPlanDataListComponent,
    EducationPlanSemesterListComponent,
    FailureNotificationComponent,
    NgIf,
    SuccessNotificationComponent,
    StudentGroupsListComponent,
    StudentsListComponent,
    StudentGroupPlanInfoComponent,
  ],
  templateUrl: './students-page.component.html',
  styleUrl: './students-page.component.scss',
  standalone: true,
  providers: [NotificationService, StudentPageViewModel],
})
export class StudentsPageComponent {
  public constructor(
    public readonly notifications: NotificationService,
    public readonly viewModel: StudentPageViewModel,
  ) {}

  protected readonly visualViewport = visualViewport;
}

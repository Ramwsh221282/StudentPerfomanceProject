import { Component } from '@angular/core';
import { EducationDirection } from '../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NgIf } from '@angular/common';
import { EducationDirectionsInlineListComponent } from './education-directions-inline-list/education-directions-inline-list.component';
import { EducationPlanDataListComponent } from './education-plan-data-list/education-plan-data-list.component';
import {
  EducationPlan,
  EducationPlanSemester,
} from '../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationPlanSemesterListComponent } from './education-plan-semester-list/education-plan-semester-list.component';
import { DisciplinesListComponent } from './disciplines-list/disciplines-list.component';
import { NotificationService } from '../../building-blocks/notifications/notification.service';
import { SuccessNotificationComponent } from '../../building-blocks/notifications/success-notification/success-notification.component';
import { FailureNotificationComponent } from '../../building-blocks/notifications/failure-notification/failure-notification.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-education-objects-page',
  templateUrl: './education-objects-page.component.html',
  styleUrl: './education-objects-page.component.scss',
  standalone: true,
  imports: [
    NgIf,
    EducationDirectionsInlineListComponent,
    EducationPlanDataListComponent,
    EducationPlanSemesterListComponent,
    DisciplinesListComponent,
    SuccessNotificationComponent,
    FailureNotificationComponent,
  ],
  providers: [NotificationService],
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
export class EducationObjectsPageComponent {
  public currentEducationDirection: EducationDirection | null;
  public currentEducationPlan: EducationPlan | null;
  public currentSemester: EducationPlanSemester | null;

  constructor(public notifications: NotificationService) {}

  public handleDirectionRemoved(): void {
    if (this.currentEducationPlan) this.currentEducationPlan = null;
    this.handlePlanRemoved();
  }

  public handlePlanRemoved(): void {
    if (this.currentSemester) this.currentSemester = null;
  }
}

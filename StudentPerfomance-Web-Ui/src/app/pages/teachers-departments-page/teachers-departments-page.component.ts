import { Component } from '@angular/core';
import { NotificationService } from '../../building-blocks/notifications/notification.service';
import { FailureNotificationComponent } from '../../building-blocks/notifications/failure-notification/failure-notification.component';
import { NgIf } from '@angular/common';
import { SuccessNotificationComponent } from '../../building-blocks/notifications/success-notification/success-notification.component';
import { DepartmentsListComponent } from './departments-list/departments-list.component';
import { TeachersListComponent } from './teachers-list/teachers-list.component';
import { Department } from '../../modules/administration/submodules/departments/models/departments.interface';

@Component({
  selector: 'app-teachers-departments-page',
  imports: [
    FailureNotificationComponent,
    NgIf,
    SuccessNotificationComponent,
    DepartmentsListComponent,
    TeachersListComponent,
  ],
  templateUrl: './teachers-departments-page.component.html',
  styleUrl: './teachers-departments-page.component.scss',
  standalone: true,
  providers: [NotificationService],
})
export class TeachersDepartmentsPageComponent {
  public currentDepartment: Department | null;

  public constructor(public readonly notifications: NotificationService) {}
}

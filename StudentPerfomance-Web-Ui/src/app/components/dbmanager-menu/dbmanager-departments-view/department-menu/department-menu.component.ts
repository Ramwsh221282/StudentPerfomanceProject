import { Component, OnInit } from '@angular/core';
import { FetchTeacherService } from './services/fetch-teacher.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ManageTeacherFormComponent } from './manage-teacher-form/manage-teacher-form.component';
import { TeachersListComponent } from './teachers-list/teachers-list.component';
import { SearchTeacherFormComponent } from './search-teacher-form/search-teacher-form.component';
import { AddTeacherFormComponent } from './add-teacher-form/add-teacher-form.component';
import { FacadeTeacherService } from './services/facade-teacher.service';
import { UserOperationNotificationService } from '../../../shared-services/user-notifications/user-operation-notification-service.service';
import { Department } from '../services/departments.interface';

@Component({
  selector: 'app-department-menu',
  standalone: true,
  imports: [
    RouterLink,
    ManageTeacherFormComponent,
    TeachersListComponent,
    SearchTeacherFormComponent,
    AddTeacherFormComponent,
  ],
  templateUrl: './department-menu.component.html',
  styleUrl: './department-menu.component.scss',
  providers: [FacadeTeacherService, UserOperationNotificationService],
})
export class DepartmentMenuComponent implements OnInit {
  protected readonly fetchService: FetchTeacherService;

  public constructor(
    protected readonly facadeTeacherService: FacadeTeacherService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      const departmentName = params['departmentName'];
      const department = { name: departmentName } as Department;
      this.facadeTeacherService.setDepartment(department);
    });
  }
}

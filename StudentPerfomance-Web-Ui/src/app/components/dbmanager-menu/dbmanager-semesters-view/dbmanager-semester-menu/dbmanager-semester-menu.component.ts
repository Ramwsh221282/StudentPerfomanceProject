import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SemesterPlanListComponent } from './semester-plan-list/semester-plan-list.component';
import { SemesterPlanCreateFormComponent } from './semester-plan-create-form/semester-plan-create-form.component';
import { SemesterPlanManageFormComponent } from './semester-plan-manage-form/semester-plan-manage-form.component';
import { SemesterPlanSearchFormComponent } from './semester-plan-search-form/semester-plan-search-form.component';
import { Semester } from '../models/semester.interface';
import { UserOperationNotificationService } from '../../../shared-services/user-notifications/user-operation-notification-service.service';
import { SemesterPlanFacadeService } from './services/semester-plan-facade.service';

@Component({
  selector: 'app-dbmanager-semester-menu',
  standalone: true,
  imports: [
    RouterLink,
    SemesterPlanListComponent,
    SemesterPlanCreateFormComponent,
    SemesterPlanManageFormComponent,
    SemesterPlanSearchFormComponent,
  ],
  templateUrl: './dbmanager-semester-menu.component.html',
  styleUrl: './dbmanager-semester-menu.component.scss',
  providers: [SemesterPlanFacadeService, UserOperationNotificationService],
})
export class DbmanagerSemesterMenuComponent implements OnInit {
  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.params.subscribe((parameters) => {
      const semester = {
        groupName: parameters['groupName'],
        number: parameters['number'],
        contractsCount: parameters['contractsCount'],
      } as Semester;
      this.facadeService.initialize(semester);
    });
  }
}

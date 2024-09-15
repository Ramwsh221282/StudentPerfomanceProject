import { Component } from '@angular/core';
import { DbmanagerDepartmentsControlPanelComponent } from './dbmanager-departments-control-panel/dbmanager-departments-control-panel.component';
import { DbmanagerDepartmentsPaginationComponent } from './dbmanager-departments-table/dbmanager-departments-pagination/dbmanager-departments-pagination.component';
import { DbmanagerDepartmentsTableComponent } from './dbmanager-departments-table/dbmanager-departments-table.component';
import { DepartmentsCreateService } from './services/departments-create.service';
import { DepartmentsDeleteService } from './services/departments-delete.service';
import { DepartmentsFetchService } from './services/departments-fetch.service';
import { DepartmentsPaginationService } from './services/departments-pagination.service';
import { DepartmentsUpdateService } from './services/departments-update.service';

@Component({
  selector: 'app-dbmanager-departments-view',
  standalone: true,
  imports: [
    DbmanagerDepartmentsControlPanelComponent,
    DbmanagerDepartmentsPaginationComponent,
    DbmanagerDepartmentsTableComponent,
  ],
  templateUrl: './dbmanager-departments-view.component.html',
  styleUrl: './dbmanager-departments-view.component.scss',
  providers: [
    DepartmentsCreateService,
    DepartmentsDeleteService,
    DepartmentsFetchService,
    DepartmentsPaginationService,
    DepartmentsUpdateService,
  ],
})
export class DbmanagerDepartmentsViewComponent {
  public constructor() {}
}

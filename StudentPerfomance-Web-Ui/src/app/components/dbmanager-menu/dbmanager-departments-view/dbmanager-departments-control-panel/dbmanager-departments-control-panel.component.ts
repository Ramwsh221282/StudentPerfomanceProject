import { Component } from '@angular/core';
import { DbmanagerDepartmentsCreateFormComponent } from './dbmanager-departments-create-form/dbmanager-departments-create-form.component';
import { DbmanagerDepartmentsEditFormComponent } from './dbmanager-departments-edit-form/dbmanager-departments-edit-form.component';
import { DbmanagerDepartmentsFilterFormComponent } from './dbmanager-departments-filter-form/dbmanager-departments-filter-form.component';

@Component({
  selector: 'app-dbmanager-departments-control-panel',
  standalone: true,
  imports: [
    DbmanagerDepartmentsCreateFormComponent,
    DbmanagerDepartmentsEditFormComponent,
    DbmanagerDepartmentsFilterFormComponent,
  ],
  templateUrl: './dbmanager-departments-control-panel.component.html',
  styleUrl: './dbmanager-departments-control-panel.component.scss',
})
export class DbmanagerDepartmentsControlPanelComponent {}

import { Component } from '@angular/core';
import { DbmanagerStudentgroupsCreateGroupFormComponent } from './dbmanager-studentgroups-create-group-form/dbmanager-studentgroups-create-group-form.component';
import { DbmanagerStudentgroupsEditGroupFormComponent } from './dbmanager-studentgroups-edit-group-form/dbmanager-studentgroups-edit-group-form.component';
import { DbmanagerStudentgroupsMergeGroupFormComponent } from './dbmanager-studentgroups-merge-group-form/dbmanager-studentgroups-merge-group-form.component';
import { DbmanagerStudentgroupsFilterGroupFormComponent } from './dbmanager-studentgroups-filter-group-form/dbmanager-studentgroups-filter-group-form.component';

@Component({
  selector: 'app-dbmanager-studentgroups-control-panel',
  standalone: true,
  imports: [
    DbmanagerStudentgroupsCreateGroupFormComponent,
    DbmanagerStudentgroupsEditGroupFormComponent,
    DbmanagerStudentgroupsMergeGroupFormComponent,
    DbmanagerStudentgroupsFilterGroupFormComponent,
  ],
  templateUrl: './dbmanager-studentgroups-control-panel.component.html',
  styleUrl: './dbmanager-studentgroups-control-panel.component.scss',
})
export class DbmanagerStudentgroupsControlPanelComponent {}

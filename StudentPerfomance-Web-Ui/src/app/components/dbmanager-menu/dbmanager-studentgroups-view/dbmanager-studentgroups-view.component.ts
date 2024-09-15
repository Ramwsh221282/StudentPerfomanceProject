import { Component } from '@angular/core';
import { DbmanagerStudentgroupsControlPanelComponent } from './dbmanager-studentgroups-control-panel/dbmanager-studentgroups-control-panel.component';
import { DbmanagerStudentgroupsTableComponent } from './dbmanager-studentgroups-table/dbmanager-studentgroups-table.component';
import { UserOperationNotificationService } from '../../shared-services/user-notifications/user-operation-notification-service.service';
import { StudentGroupsFacadeService } from './services/student-groups-facade.service';

@Component({
  selector: 'app-dbmanager-studentgroups-view',
  standalone: true,
  imports: [
    DbmanagerStudentgroupsControlPanelComponent,
    DbmanagerStudentgroupsTableComponent,
  ],
  templateUrl: './dbmanager-studentgroups-view.component.html',
  styleUrl: './dbmanager-studentgroups-view.component.scss',
  providers: [StudentGroupsFacadeService, UserOperationNotificationService],
})
export class DbmanagerStudentgroupsViewComponent {}

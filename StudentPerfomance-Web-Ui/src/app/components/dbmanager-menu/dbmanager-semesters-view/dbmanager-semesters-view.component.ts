import { Component } from '@angular/core';
import { DbmanagerSemestersTableComponent } from './dbmanager-semesters-table/dbmanager-semesters-table.component';
import { DbmanagerSemestersControlPanelComponent } from './dbmanager-semesters-control-panel/dbmanager-semesters-control-panel.component';
import { SemesterFacadeService } from './services/semester-facade.service';
import { UserOperationNotificationService } from '../../shared-services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-dbmanager-semesters-view',
  standalone: true,
  imports: [
    DbmanagerSemestersTableComponent,
    DbmanagerSemestersControlPanelComponent,
  ],
  templateUrl: './dbmanager-semesters-view.component.html',
  styleUrl: './dbmanager-semesters-view.component.scss',
  providers: [SemesterFacadeService, UserOperationNotificationService],
})
export class DbmanagerSemestersViewComponent {}

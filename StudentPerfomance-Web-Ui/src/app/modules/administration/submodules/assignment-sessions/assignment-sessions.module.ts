import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssignmentSessionsPageComponent } from './components/assignment-sessions-page/assignment-sessions-page.component';
import { AssignmentSessionsRoutingModule } from './assignment-sessions-routing.module';
import { AssignmentSessionTableComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-session-table.component';
import { AssignmentSessionsPaginationComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-pagination/assignment-sessions-pagination.component';
import { AssignmentSessionsItemComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-sessions-item.component';
import { AssignmentSessionsCreateModalComponent } from './components/assignment-sessions-page/assignment-sessions-create-modal/assignment-sessions-create-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { AssignmentSessionItemInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-item-info.component';
import { AssignmentSessionItemAssignmentsComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-assignments/assignment-session-item-assignments.component';

@NgModule({
  declarations: [
    AssignmentSessionsPageComponent,
    AssignmentSessionTableComponent,
    AssignmentSessionsPaginationComponent,
    AssignmentSessionsItemComponent,
    AssignmentSessionsCreateModalComponent,
    AssignmentSessionItemInfoComponent,
    AssignmentSessionItemAssignmentsComponent,
  ],
  imports: [
    CommonModule,
    AssignmentSessionsRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  exports: [AssignmentSessionsPageComponent],
})
export class AssignmentSessionsModule {}

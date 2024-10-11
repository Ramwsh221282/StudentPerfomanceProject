import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StudentGroupsRoutingModule } from './student-groups-routing.module';
import { StudentGroupsPageComponent } from './components/student-groups-page/student-groups-page.component';
import { StudentGroupsTableComponent } from './components/student-groups-table/student-groups-table.component';
import { TableRowComponent } from './components/student-groups-table/table-row/table-row.component';
import { TablePaginationComponent } from './components/student-groups-table/table-pagination/table-pagination.component';
import { StudentGroupCreateModalComponent } from './components/student-group-create-modal/student-group-create-modal.component';
import { StudentGroupFilterModalComponent } from './components/student-group-filter-modal/student-group-filter-modal.component';
import { StudentGroupRemoveModalComponent } from './components/student-group-remove-modal/student-group-remove-modal.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { StudentGroupEditModalComponent } from './components/student-group-edit-modal/student-group-edit-modal.component';
import { NameChangeModalComponent } from './components/student-group-edit-modal/name-change-modal/name-change-modal.component';
import { PlanAttachmentModalComponent } from './components/student-group-edit-modal/plan-attachment-modal/plan-attachment-modal.component';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';

@NgModule({
  declarations: [
    StudentGroupsPageComponent,
    StudentGroupsTableComponent,
    TableRowComponent,
    TablePaginationComponent,
    StudentGroupCreateModalComponent,
    StudentGroupFilterModalComponent,
    StudentGroupRemoveModalComponent,
    StudentGroupEditModalComponent,
    NameChangeModalComponent,
    PlanAttachmentModalComponent,
  ],
  imports: [
    CommonModule,
    StudentGroupsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FailureResultNotificationComponent,
    SuccessResultNotificationComponent,
    FailureNotificationFormComponent,
  ],
  exports: [StudentGroupsPageComponent],
})
export class StudentGroupsModule {}

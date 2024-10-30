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
import { MergeGroupModalComponent } from './components/student-group-edit-modal/merge-group-modal/merge-group-modal.component';
import { StudentsMenuModalComponent } from './components/students-menu-modal/students-menu-modal.component';
import { StudentEditModalComponent } from './components/students-menu-modal/student-edit-modal/student-edit-modal.component';
import { StudentDeletionModalComponent } from './components/students-menu-modal/student-deletion-modal/student-deletion-modal.component';
import { StudentCreationModalComponent } from './components/students-menu-modal/student-creation-modal/student-creation-modal.component';
import { StudentFilterModalComponent } from './components/students-menu-modal/student-filter-modal/student-filter-modal.component';
import { StudentSwitchGroupModalComponent } from './components/students-menu-modal/student-edit-modal/student-switch-group-modal/student-switch-group-modal.component';

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
    MergeGroupModalComponent,
    StudentsMenuModalComponent,
    StudentEditModalComponent,
    StudentDeletionModalComponent,
    StudentCreationModalComponent,
    StudentFilterModalComponent,
    StudentSwitchGroupModalComponent,
  ],
  imports: [
    CommonModule,
    StudentGroupsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FailureResultNotificationComponent,
    SuccessResultNotificationComponent,
  ],
  exports: [StudentGroupsPageComponent],
})
export class StudentGroupsModule {}

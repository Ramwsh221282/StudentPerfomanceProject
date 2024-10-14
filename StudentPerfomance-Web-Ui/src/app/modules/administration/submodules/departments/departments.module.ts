import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentPageComponent } from './components/department-page/department-page.component';
import { DepartmentTableComponent } from './components/department-table/department-table.component';
import { DepartmentPaginationComponent } from './components/department-table/department-pagination/department-pagination.component';
import { DepartmentTableRowComponent } from './components/department-table/department-table-row/department-table-row.component';
import { DepartmentCreationModalComponent } from './components/department-table/department-creation-modal/department-creation-modal.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { DepartmentDeletionModalComponent } from './components/department-table/department-deletion-modal/department-deletion-modal.component';
import { DepartmentEditModalComponent } from './components/department-table/department-edit-modal/department-edit-modal.component';
import { DepartmentFilterModalComponent } from './components/department-table/department-filter-modal/department-filter-modal.component';

@NgModule({
  declarations: [
    DepartmentPageComponent,
    DepartmentTableComponent,
    DepartmentPaginationComponent,
    DepartmentTableRowComponent,
    DepartmentCreationModalComponent,
    DepartmentDeletionModalComponent,
    DepartmentEditModalComponent,
    DepartmentFilterModalComponent,
  ],
  imports: [
    CommonModule,
    DepartmentsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  exports: [DepartmentPageComponent],
})
export class DepartmentsModule {}

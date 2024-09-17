import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateDepartmentComponent } from './components/create-department/create-department.component';
import { FilterDepartmentComponent } from './components/filter-department/filter-department.component';
import { ManageDepartmentComponent } from './components/manage-department/manage-department.component';
import { PageDepartmentComponent } from './components/page-department/page-department.component';
import { TableDepartmentComponent } from './components/table-department/table-department.component';
import { PaginationDepartmentComponent } from './components/table-department/pagination-department/pagination-department.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentsFacadeService } from './services/departments-facade.service';

@NgModule({
  declarations: [
    CreateDepartmentComponent,
    FilterDepartmentComponent,
    ManageDepartmentComponent,
    PageDepartmentComponent,
    TableDepartmentComponent,
    PaginationDepartmentComponent,
  ],
  imports: [
    CommonModule,
    DepartmentsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [PageDepartmentComponent],
  providers: [DepartmentsFacadeService],
})
export class DepartmentsModule {}

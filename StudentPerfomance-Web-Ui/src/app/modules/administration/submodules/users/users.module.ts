import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersPageComponent } from './components/users-page/users-page.component';
import { UsersRoutingModule } from './users-routing.module';
import { UsersCreateModalComponent } from './components/users-create-modal/users-create-modal.component';
import { UsersEditModalComponent } from './components/users-edit-modal/users-edit-modal.component';
import { UsersFilterModalComponent } from './components/users-filter-modal/users-filter-modal.component';
import { UsersRemoveModalComponent } from './components/users-remove-modal/users-remove-modal.component';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { UsersTableRowComponent } from './components/users-table/users-table-row/users-table-row.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { UsersTablePaginationComponent } from './components/users-table/users-table-pagination/users-table-pagination.component';

@NgModule({
  declarations: [
    UsersPageComponent,
    UsersCreateModalComponent,
    UsersEditModalComponent,
    UsersFilterModalComponent,
    UsersRemoveModalComponent,
    UsersTableComponent,
    UsersTableRowComponent,
    UsersTablePaginationComponent,
  ],
  imports: [
    UsersRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  exports: [UsersPageComponent],
})
export class UsersModule {}

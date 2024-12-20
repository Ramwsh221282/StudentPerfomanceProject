import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersPageComponent } from './components/users-page/users-page.component';
import { UsersRoutingModule } from './users-routing.module';
import { UsersRemoveModalComponent } from './components/users-remove-modal/users-remove-modal.component';
import { UsersTableComponent } from './components/users-table/users-table.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { UsersTablePaginationComponent } from './components/users-table/users-table-pagination/users-table-pagination.component';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { UsersItemComponent } from './components/users-table/users-item/users-item.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { CreateAdminDropdownComponent } from './components/users-table/create-admin-dropdown/create-admin-dropdown.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { CreateTeacherUserDropdownComponent } from './components/users-table/create-teacher-user-dropdown/create-teacher-user-dropdown.component';
import { DropdownListComponent } from '../../../../building-blocks/dropdown-list/dropdown-list.component';
import { FilterUserDropdownComponent } from './components/users-table/filter-user-dropdown/filter-user-dropdown.component';

@NgModule({
  declarations: [
    UsersPageComponent,
    UsersRemoveModalComponent,
    UsersTableComponent,
    UsersTablePaginationComponent,
    UsersItemComponent,
    CreateAdminDropdownComponent,
    CreateTeacherUserDropdownComponent,
    FilterUserDropdownComponent,
  ],
  imports: [
    UsersRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    BlueButtonComponent,
    BlueOutlineButtonComponent,
    RedButtonComponent,
    YellowButtonComponent,
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    DropdownListComponent,
  ],
  exports: [UsersPageComponent],
})
export class UsersModule {}

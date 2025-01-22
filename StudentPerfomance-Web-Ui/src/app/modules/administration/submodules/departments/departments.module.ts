import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DepartmentsRoutingModule } from './departments-routing.module';
import { DepartmentPageComponent } from './components/department-page/department-page.component';
import { DepartmentTableComponent } from './components/department-table/department-table.component';
import { DepartmentPaginationComponent } from './components/department-table/department-pagination/department-pagination.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { DepartmentDeletionModalComponent } from './components/department-table/department-deletion-modal/department-deletion-modal.component';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { DepartmentItemComponent } from './components/department-table/department-item/department-item.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { TeacherCreateDropdownComponent } from './components/department-table/teachers-menu/teacher-create-dropdown/teacher-create-dropdown.component';
import { TeacherEditDropdownComponent } from './components/department-table/teachers-menu/teacher-edit-dropdown/teacher-edit-dropdown.component';
import { TeacherItemComponent } from './components/department-table/teachers-menu/teacher-item/teacher-item.component';
import { TeacherRemovePopupComponent } from './components/department-table/teachers-menu/teacher-remove-popup/teacher-remove-popup.component';
import { TeachersMenuComponent } from './components/department-table/teachers-menu/teachers-menu.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { DropdownListComponent } from '../../../../building-blocks/dropdown-list/dropdown-list.component';
import { DepartmentCreateDropdownComponent } from './components/department-table/department-create-dropdown/department-create-dropdown.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';
import { DepartmentEditDropdownComponent } from './components/department-table/department-edit-dropdown/department-edit-dropdown.component';
import { DepartmentFilterDropdownComponent } from './components/department-table/department-filter-dropdown/department-filter-dropdown.component';

@NgModule({
  declarations: [
    DepartmentPageComponent,
    DepartmentTableComponent,
    DepartmentPaginationComponent,
    DepartmentDeletionModalComponent,
    DepartmentItemComponent,
    TeacherCreateDropdownComponent,
    TeacherEditDropdownComponent,
    TeacherItemComponent,
    TeacherRemovePopupComponent,
    TeachersMenuComponent,
    DepartmentCreateDropdownComponent,
    DepartmentEditDropdownComponent,
    DepartmentFilterDropdownComponent,
  ],
  imports: [
    CommonModule,
    DepartmentsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
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
    YellowOutlineButtonComponent,
  ],
  exports: [DepartmentPageComponent, DepartmentItemComponent],
})
export class DepartmentsModule {}

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StudentGroupsRoutingModule } from './student-groups-routing.module';
import { StudentGroupsPageComponent } from './components/student-groups-page/student-groups-page.component';
import { StudentGroupsTableComponent } from './components/student-groups-table/student-groups-table.component';
import { TablePaginationComponent } from './components/student-groups-table/table-pagination/table-pagination.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { StudentGroupItemComponent } from './components/student-group-item/student-group-item.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { StudentGroupMenuComponent } from './components/student-group-menu/student-group-menu.component';
import { StudentsListComponent } from './components/students-list/students-list.component';
import { GroupEducationPlanMenuComponent } from './components/group-education-plan-menu/group-education-plan-menu.component';
import { StudentItemComponent } from './components/students-list/student-item/student-item.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { CreateStudentDropdownComponent } from './components/students-list/create-student-dropdown/create-student-dropdown.component';
import { EducationDirectionsSelectComponent } from '../education-plans/components/create-education-plan-dropdown/education-directions-select/education-directions-select.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { StudentStatusDropdownComponent } from './components/students-list/create-student-dropdown/student-status-dropdown/student-status-dropdown.component';
import { EditStudentDropdownComponent } from './components/students-list/edit-student-dropdown/edit-student-dropdown.component';
import { SwitchStudentGroupDropdownComponent } from './components/students-list/switch-student-group-dropdown/switch-student-group-dropdown.component';
import { DropdownListComponent } from '../../../../building-blocks/dropdown-list/dropdown-list.component';
import { StudentRemovePopupComponent } from './components/students-list/student-remove-popup/student-remove-popup.component';
import { GroupEducationPlanInfoComponent } from './components/student-group-menu/group-education-plan-info/group-education-plan-info.component';
import { ChangeGroupEducationPlanPopupComponent } from './components/student-group-menu/group-education-plan-info/change-group-education-plan-popup/change-group-education-plan-popup.component';
import { DeattachEducationPlanPopupComponent } from './components/student-group-menu/group-education-plan-info/deattach-education-plan-popup/deattach-education-plan-popup.component';
import { CreateGroupDropdownComponent } from './components/create-group-dropdown/create-group-dropdown.component';
import { FilterGroupDropdownComponent } from './components/filter-group-dropdown/filter-group-dropdown.component';
import { RemoveGroupPopupComponent } from './components/remove-group-popup/remove-group-popup.component';
import { MergeGroupDropdownComponent } from './components/merge-group-dropdown/merge-group-dropdown.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';

@NgModule({
  declarations: [
    StudentGroupsPageComponent,
    StudentGroupsTableComponent,
    TablePaginationComponent,
    StudentGroupItemComponent,
    StudentGroupMenuComponent,
    StudentsListComponent,
    GroupEducationPlanMenuComponent,
    StudentItemComponent,
    CreateStudentDropdownComponent,
    EditStudentDropdownComponent,
    SwitchStudentGroupDropdownComponent,
    StudentRemovePopupComponent,
    GroupEducationPlanInfoComponent,
    DeattachEducationPlanPopupComponent,
    CreateGroupDropdownComponent,
    FilterGroupDropdownComponent,
    RemoveGroupPopupComponent,
    MergeGroupDropdownComponent,
  ],
  imports: [
    CommonModule,
    StudentGroupsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FailureResultNotificationComponent,
    SuccessResultNotificationComponent,
    BlueOutlineButtonComponent,
    BlueButtonComponent,
    RedButtonComponent,
    YellowButtonComponent,
    EducationDirectionsSelectComponent,
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    StudentStatusDropdownComponent,
    DropdownListComponent,
    ChangeGroupEducationPlanPopupComponent,
    YellowOutlineButtonComponent,
  ],
  exports: [StudentGroupsPageComponent],
})
export class StudentGroupsModule {}

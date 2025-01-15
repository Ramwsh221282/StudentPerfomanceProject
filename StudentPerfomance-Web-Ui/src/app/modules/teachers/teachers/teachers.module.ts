import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeachersAssignmentPageComponent } from '../components/teachers-assignment-page/teachers-assignment-page.component';
import { TeachersRoutingModule } from './teachers-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuccessResultNotificationComponent } from '../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../shared/components/failure-result-notification/failure-result-notification.component';
import { TeacherAssignmentSessionInfoComponent } from '../components/teachers-assignment-page/teacher-assignment-session-info/teacher-assignment-session-info.component';
import { TeacherAssignmentsTableComponent } from '../components/teachers-assignment-page/teacher-assignments-table/teacher-assignments-table.component';
import { TeacherAssignmentsComponent } from '../components/teachers-assignment-page/teacher-assignments-table/teacher-assignments/teacher-assignments.component';
import { MarksModalComponent } from '../components/teachers-assignment-page/teacher-assignments-table/teacher-assignments/marks-modal/marks-modal.component';
import { TeacherAssignmentsGroupTabsComponent } from '../components/teachers-assignment-page/teacher-assignments-table/teacher-assignments-group-tabs/teacher-assignments-group-tabs.component';
import { TeacherAssignmentGroupMenuComponent } from '../components/teachers-assignment-page/teacher-assignments-table/teacher-assignments-group-tabs/teacher-assignment-group-menu/teacher-assignment-group-menu.component';
import { DropdownListComponent } from '../../../building-blocks/dropdown-list/dropdown-list.component';
import { RedOutlineButtonComponent } from '../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { AdminAssignmentsAccessResolverDialogComponent } from '../components/teachers-assignment-page/admin-assignments-access-resolver-dialog/admin-assignments-access-resolver-dialog.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { YellowButtonComponent } from '../../../building-blocks/buttons/yellow-button/yellow-button.component';

@NgModule({
  declarations: [
    TeachersAssignmentPageComponent,
    TeacherAssignmentSessionInfoComponent,
    TeacherAssignmentsTableComponent,
    TeacherAssignmentsComponent,
    MarksModalComponent,
    AdminAssignmentsAccessResolverDialogComponent,
  ],
  imports: [
    CommonModule,
    TeachersRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    TeacherAssignmentsGroupTabsComponent,
    TeacherAssignmentGroupMenuComponent,
    DropdownListComponent,
    RedOutlineButtonComponent,
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    YellowButtonComponent,
  ],
  exports: [TeachersAssignmentPageComponent, TeacherAssignmentsComponent],
})
export class TeachersModule {}

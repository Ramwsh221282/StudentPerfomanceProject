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

@NgModule({
  declarations: [
    TeachersAssignmentPageComponent,
    TeacherAssignmentSessionInfoComponent,
    TeacherAssignmentsTableComponent,
    TeacherAssignmentsComponent,
  ],
  imports: [
    CommonModule,
    TeachersRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  exports: [TeachersAssignmentPageComponent],
})
export class TeachersModule {}

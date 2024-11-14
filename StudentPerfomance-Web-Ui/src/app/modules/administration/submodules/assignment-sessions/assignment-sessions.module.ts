import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssignmentSessionsPageComponent } from './components/assignment-sessions-page/assignment-sessions-page.component';
import { AssignmentSessionsRoutingModule } from './assignment-sessions-routing.module';
import { AssignmentSessionTableComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-session-table.component';
import { AssignmentSessionsPaginationComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-pagination/assignment-sessions-pagination.component';
import { AssignmentSessionsItemComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-sessions-item.component';
import { AssignmentSessionsCreateModalComponent } from './components/assignment-sessions-page/assignment-sessions-create-modal/assignment-sessions-create-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { AssignmentSessionItemInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-item-info.component';
import { AssignmentSessionItemAssignmentsComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-assignments/assignment-session-item-assignments.component';
import { AssignmentSessionCourseInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-course-info/assignment-session-course-info.component';
import { AssignmentSessionDepartmentInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-department-info/assignment-session-department-info.component';
import { AssignmentSessionDirectionInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-direction-info/assignment-session-direction-info.component';
import { AssignmentSessionUniversityInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-university-info/assignment-session-university-info.component';
import { AssignmentSessionDirectionCodeInfoComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-direction-code-info/assignment-session-direction-code-info.component';
import { AssignmentSessionCloseItemModalComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-close-item-modal/assignment-session-close-item-modal.component';
import { AssignmentSessionReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/assignment-session-report.component';
import { CourseReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/course-report/course-report.component';
import { DirectionCodeReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/direction-code-report/direction-code-report.component';
import { DepartmentReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/department-report/department-report.component';
import { TeachersReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/department-report/teachers-report/teachers-report.component';
import { DirectionTypeReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/direction-type-report/direction-type-report.component';
import { GroupReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/group-report/group-report.component';
import { StudentsReportComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-sessions-item/assignment-session-item-info/assignment-session-report/students-report/students-report.component';

@NgModule({
  declarations: [
    AssignmentSessionsPageComponent,
    AssignmentSessionTableComponent,
    AssignmentSessionsPaginationComponent,
    AssignmentSessionsItemComponent,
    AssignmentSessionsCreateModalComponent,
    AssignmentSessionItemInfoComponent,
    AssignmentSessionItemAssignmentsComponent,
    AssignmentSessionCourseInfoComponent,
    AssignmentSessionDepartmentInfoComponent,
    AssignmentSessionDirectionInfoComponent,
    AssignmentSessionUniversityInfoComponent,
    AssignmentSessionDirectionCodeInfoComponent,
    AssignmentSessionCloseItemModalComponent,
    AssignmentSessionReportComponent,
    CourseReportComponent,
    DirectionCodeReportComponent,
    DirectionTypeReportComponent,
    DepartmentReportComponent,
    TeachersReportComponent,
    GroupReportComponent,
    StudentsReportComponent,
  ],
  imports: [
    CommonModule,
    AssignmentSessionsRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  exports: [AssignmentSessionsPageComponent],
})
export class AssignmentSessionsModule {}

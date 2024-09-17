import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { StudentCreateComponent } from './components/student-create/student-create.component';
import { StudentFilterComponent } from './components/student-filter/student-filter.component';
import { StudentManageComponent } from './components/student-manage/student-manage.component';
import { StudentPageComponent } from './components/student-page/student-page.component';
import { StudentPaginationComponent } from './components/student-table/student-pagination/student-pagination.component';
import { StudentTableComponent } from './components/student-table/student-table.component';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { StudentsRoutingModule } from './students-routing.module';
import { FacadeStudentService } from './services/facade-student.service';

@NgModule({
  declarations: [
    StudentCreateComponent,
    StudentFilterComponent,
    StudentPageComponent,
    StudentPaginationComponent,
    StudentTableComponent,
    StudentManageComponent,
  ],
  imports: [
    CommonModule,
    StudentsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  providers: [FacadeStudentService],
  exports: [StudentPageComponent],
})
export class StudentsModule {}

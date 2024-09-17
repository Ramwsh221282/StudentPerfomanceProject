import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TeacherCreateComponent } from './components/teacher-create/teacher-create.component';
import { TeacherManageComponent } from './components/teacher-manage/teacher-manage.component';
import { TeacherFilterComponent } from './components/teacher-filter/teacher-filter.component';
import { TeacherTableComponent } from './components/teacher-table/teacher-table.component';
import { TeacherPaginationComponent } from './components/teacher-table/teacher-pagination/teacher-pagination.component';
import { TeacherPageComponent } from './components/teacher-page/teacher-page.component';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { TeachersRoutingModule } from './teachers-routing.module';
import { FacadeTeacherService } from './services/facade-teacher.service';

@NgModule({
  declarations: [
    TeacherCreateComponent,
    TeacherManageComponent,
    TeacherFilterComponent,
    TeacherTableComponent,
    TeacherPaginationComponent,
    TeacherPageComponent,
  ],
  imports: [
    CommonModule,
    TeachersRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [TeacherPageComponent],
  providers: [FacadeTeacherService],
})
export class TeachersModule {}

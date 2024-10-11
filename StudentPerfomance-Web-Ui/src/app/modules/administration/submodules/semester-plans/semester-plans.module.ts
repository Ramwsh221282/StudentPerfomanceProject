import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { SemesterPlansRoutingModule } from './semester-plans-routing.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SemesterPlansRoutingModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [],
})
export class SemesterPlansModule {}

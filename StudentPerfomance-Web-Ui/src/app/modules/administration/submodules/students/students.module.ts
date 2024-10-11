import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { StudentsRoutingModule } from './students-routing.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StudentsRoutingModule,
    ReactiveFormsModule,
    RouterModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [],
})
export class StudentsModule {}

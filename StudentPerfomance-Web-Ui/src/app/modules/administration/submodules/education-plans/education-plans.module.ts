import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EducationPlansCreateModalComponent } from './components/education-plans-create-modal/education-plans-create-modal.component';
import { EducationPlansFilterModalComponent } from './components/education-plans-filter-modal/education-plans-filter-modal.component';
import { EducationPlansPageComponent } from './components/education-plans-page/education-plans-page.component';
import { EducationPlansTableComponent } from './components/education-plans-table/education-plans-table.component';
import { EducationPlansPaginationComponent } from './components/education-plans-table/education-plans-pagination/education-plans-pagination.component';
import { EducationPlansCardComponent } from './components/education-plans-table/education-plans-card/education-plans-card.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { ActionConfirmationModalComponent } from '../../../../shared/components/action-confirmation-modal/action-confirmation-modal.component';
import { EducationPlansRoutingModule } from './education-plans-routing.module';

@NgModule({
  declarations: [
    EducationPlansCreateModalComponent,
    EducationPlansFilterModalComponent,
    EducationPlansPageComponent,
    EducationPlansTableComponent,
    EducationPlansPaginationComponent,
    EducationPlansCardComponent,
  ],
  imports: [
    CommonModule,
    EducationPlansRoutingModule,
    ReactiveFormsModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
    ActionConfirmationModalComponent,
  ],
  exports: [EducationPlansPageComponent],
})
export class EducationPlansModule {}

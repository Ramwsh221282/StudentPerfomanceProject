import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EducationPlansCreateModalComponent } from './components/education-plans-create-modal/education-plans-create-modal.component';
import { EducationPlansPageComponent } from './components/education-plans-page/education-plans-page.component';
import { EducationPlansTableComponent } from './components/education-plans-table/education-plans-table.component';
import { EducationPlansPaginationComponent } from './components/education-plans-table/education-plans-pagination/education-plans-pagination.component';
import { EducationPlansCardComponent } from './components/education-plans-table/education-plans-card/education-plans-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { EducationPlansRoutingModule } from './education-plans-routing.module';
import { EducationPlansFilterByDirectionModalComponent } from './components/education-plans-filter-by-direction-modal/education-plans-filter-by-direction-modal.component';
import { EducationPlansFilterByPlanYearModalComponent } from './components/education-plans-filter-by-plan-year-modal/education-plans-filter-by-plan-year-modal.component';
import { EducationPlanDeletionConfirmationComponent } from './components/education-plan-deletion-confirmation/education-plan-deletion-confirmation.component';

@NgModule({
  declarations: [
    EducationPlansCreateModalComponent,
    EducationPlansPageComponent,
    EducationPlansTableComponent,
    EducationPlansPaginationComponent,
    EducationPlansCardComponent,
    EducationPlansFilterByDirectionModalComponent,
    EducationPlansFilterByPlanYearModalComponent,
    EducationPlanDeletionConfirmationComponent,
  ],
  imports: [
    CommonModule,
    EducationPlansRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [EducationPlansPageComponent],
})
export class EducationPlansModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EducationDirectionsCreateModalComponent } from './components/education-directions-create-modal/education-directions-create-modal.component';
import { EducationDirectionsEditModalComponent } from './components/education-directions-edit-modal/education-directions-edit-modal.component';
import { EducationDirectionsFilterModalComponent } from './components/education-directions-filter-modal/education-directions-filter-modal.component';
import { EducationDirectionsPageComponent } from './components/education-directions-page/education-directions-page.component';
import { EducationDirectionsTableComponent } from './components/education-directions-table/education-directions-table.component';
import { EducationDirectionsCardComponent } from './components/education-directions-table/education-directions-card/education-directions-card.component';
import { EducationDirectionsRoutingModule } from './education-directions-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { EducationDirectionsPaginationComponent } from './components/education-directions-table/education-directions-pagination/education-directions-pagination.component';
import { EducationDirectionDeletionConfirmationComponent } from './components/education-direction-deletion-confirmation/education-direction-deletion-confirmation.component';
import { EducationDirectionFilterFormComponent } from './components/education-direction-filter-form/education-direction-filter-form.component';
import { EducationDirectionStatisticsCardComponent } from './components/education-direction-statistics-card/education-direction-statistics-card.component';

@NgModule({
  declarations: [
    EducationDirectionsCreateModalComponent,
    EducationDirectionsEditModalComponent,
    EducationDirectionsFilterModalComponent,
    EducationDirectionsPageComponent,
    EducationDirectionsTableComponent,
    EducationDirectionsCardComponent,
    EducationDirectionsPaginationComponent,
    EducationDirectionDeletionConfirmationComponent,
    EducationDirectionFilterFormComponent,
    EducationDirectionStatisticsCardComponent,
  ],
  imports: [
    CommonModule,
    EducationDirectionsRoutingModule,
    ReactiveFormsModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [EducationDirectionsPageComponent],
})
export class EducationDirectionsModule {}

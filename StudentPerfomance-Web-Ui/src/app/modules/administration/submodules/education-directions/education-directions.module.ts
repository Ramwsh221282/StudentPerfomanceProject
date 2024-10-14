import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EducationDirectionsCreateModalComponent } from './components/education-directions-create-modal/education-directions-create-modal.component';
import { EducationDirectionsEditModalComponent } from './components/education-directions-edit-modal/education-directions-edit-modal.component';
import { EducationDirectionsFilterModalComponent } from './components/education-directions-filter-modal/education-directions-filter-modal.component';
import { EducationDirectionsPageComponent } from './components/education-directions-page/education-directions-page.component';
import { EducationDirectionsTableComponent } from './components/education-directions-table/education-directions-table.component';
import { EducationDirectionsRoutingModule } from './education-directions-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EducationDirectionsPaginationComponent } from './components/education-directions-table/education-directions-pagination/education-directions-pagination.component';
import { EducationDirectionsTableRowComponent } from './components/education-directions-table/education-directions-table-row/education-directions-table-row.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { EducationDirectionDeleteModalComponent } from './components/education-direction-delete-modal/education-direction-delete-modal.component';

@NgModule({
  declarations: [
    EducationDirectionsCreateModalComponent,
    EducationDirectionsEditModalComponent,
    EducationDirectionsFilterModalComponent,
    EducationDirectionsPageComponent,
    EducationDirectionsTableComponent,
    EducationDirectionsTableRowComponent,
    EducationDirectionsPaginationComponent,
    EducationDirectionDeleteModalComponent,
  ],
  imports: [
    CommonModule,
    EducationDirectionsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FailureResultNotificationComponent,
    SuccessResultNotificationComponent,
  ],
  exports: [EducationDirectionsPageComponent],
})
export class EducationDirectionsModule {}

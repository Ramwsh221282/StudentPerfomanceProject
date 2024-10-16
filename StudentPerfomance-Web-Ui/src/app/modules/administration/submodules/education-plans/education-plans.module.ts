import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EducationPlansRoutingModule } from './education-plans-routing.module';
import { EducationPlansPageComponent } from './components/education-plans-page/education-plans-page.component';
import { EducationPlansTableComponent } from './components/education-plans-table/education-plans-table.component';
import { EducationPlansTableRowComponent } from './components/education-plans-table/education-plans-table-row/education-plans-table-row.component';
import { EducationPlansPaginationComponent } from './components/education-plans-table/education-plans-pagination/education-plans-pagination.component';
import { EducationPlanCreationModalComponent } from './components/education-plan-creation-modal/education-plan-creation-modal.component';
import { EducationPlanDeletionModalComponent } from './components/education-plan-deletion-modal/education-plan-deletion-modal.component';
import { EducationPlanDisciplinesModalComponent } from './components/education-plan-disciplines-modal/education-plan-disciplines-modal.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { EducationPlanFilterModalComponent } from './components/education-plan-filter-modal/education-plan-filter-modal.component';
import { EducationPlansSemestersComponent } from './components/education-plans-table/education-plans-semesters/education-plans-semesters.component';
import { SemesterDisciplinesModalComponent } from './components/education-plans-table/education-plans-semesters/semester-disciplines-modal/semester-disciplines-modal.component';
import { SemesterDisciplinesCreationModalComponent } from './components/education-plans-table/education-plans-semesters/semester-disciplines-modal/semester-disciplines-creation-modal/semester-disciplines-creation-modal.component';
import { SemesterDisciplinesDeletionModalComponent } from './components/education-plans-table/education-plans-semesters/semester-disciplines-modal/semester-disciplines-deletion-modal/semester-disciplines-deletion-modal.component';
import { SemesterDisciplinesEditModalComponent } from './components/education-plans-table/education-plans-semesters/semester-disciplines-modal/semester-disciplines-edit-modal/semester-disciplines-edit-modal.component';

@NgModule({
  declarations: [
    EducationPlansPageComponent,
    EducationPlansTableComponent,
    EducationPlansTableRowComponent,
    EducationPlansPageComponent,
    EducationPlansPaginationComponent,
    EducationPlanCreationModalComponent,
    EducationPlanDeletionModalComponent,
    EducationPlanDisciplinesModalComponent,
    EducationPlanFilterModalComponent,
    EducationPlansSemestersComponent,
    SemesterDisciplinesModalComponent,
    SemesterDisciplinesCreationModalComponent,
    SemesterDisciplinesDeletionModalComponent,
    SemesterDisciplinesEditModalComponent,
  ],
  imports: [
    CommonModule,
    EducationPlansRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  exports: [EducationPlansPageComponent],
})
export class EducationPlansModule {}

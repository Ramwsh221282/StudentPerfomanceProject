import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlanCreateComponent } from './components/plan-create/plan-create.component';
import { PlanFilterComponent } from './components/plan-filter/plan-filter.component';
import { PlanManageComponent } from './components/plan-manage/plan-manage.component';
import { PlanPageComponent } from './components/plan-page/plan-page.component';
import { PlanPaginationComponent } from './components/plan-table/plan-pagination/plan-pagination.component';
import { PlanCardComponent } from './components/plan-table/plan-card/plan-card.component';
import { PlanTableComponent } from './components/plan-table/plan-table.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PlanCardInfoComponent } from './components/plan-table/plan-card/plan-card-info/plan-card-info.component';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { SemesterPlansRoutingModule } from './semester-plans-routing.module';

@NgModule({
  declarations: [
    PlanCreateComponent,
    PlanManageComponent,
    PlanFilterComponent,
    PlanPageComponent,
    PlanPaginationComponent,
    PlanCardComponent,
    PlanTableComponent,
    PlanCardInfoComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SemesterPlansRoutingModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [PlanPageComponent],
})
export class SemesterPlansModule {}

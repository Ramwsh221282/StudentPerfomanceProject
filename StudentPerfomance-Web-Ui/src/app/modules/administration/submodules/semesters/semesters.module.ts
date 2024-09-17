import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SemesterCreateComponent } from './components/semester-create/semester-create.component';
import { SemesterManageComponent } from './components/semester-manage/semester-manage.component';
import { SemesterFilterComponent } from './components/semester-filter/semester-filter.component';
import { SemesterTableComponent } from './components/semester-table/semester-table.component';
import { SemesterPaginationComponent } from './components/semester-table/semester-pagination/semester-pagination.component';
import { SemesterPageComponent } from './components/semester-page/semester-page.component';
import { SemesterNumberSelectionModalComponent } from './components/semester-number-selection-modal/semester-number-selection-modal.component';
import { SearchGroupComponent } from '../student-groups/components/search-group/search-group.component';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { SemestersRoutingModule } from './semesters-routing.module';
import { SemesterFacadeService } from './services/semester-facade.service';

@NgModule({
  declarations: [
    SemesterCreateComponent,
    SemesterManageComponent,
    SemesterFilterComponent,
    SemesterTableComponent,
    SemesterPaginationComponent,
    SemesterPageComponent,
    SemesterNumberSelectionModalComponent,
  ],
  imports: [
    CommonModule,
    SemestersRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SearchGroupComponent,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
  ],
  exports: [SemesterPageComponent],
  providers: [SemesterFacadeService],
})
export class SemestersModule {}

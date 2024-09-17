import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FailureNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { SuccessNotificationFormComponent } from '../../../../shared/components/notification-modal-forms/success-notification-form/success-notification-form.component';
import { PageGroupComponent } from './components/page-group/page-group.component';
import { CreateGroupComponent } from './components/create-group/create-group.component';
import { MergeGroupComponent } from './components/merge-group/merge-group.component';
import { ManageGroupComponent } from './components/manage-group/manage-group.component';
import { PaginationGroupComponent } from './components/table-group/pagination-group/pagination-group.component';
import { FilterGroupComponent } from './components/filter-group/filter-group.component';
import { TableGroupComponent } from './components/table-group/table-group.component';
import { SearchGroupComponent } from './components/search-group/search-group.component';
import { StudentGroupsFacadeService } from './services/student-groups-facade.service';
import { StudentGroupsRoutingModule } from './student-groups-routing.module';

@NgModule({
  declarations: [
    PageGroupComponent,
    CreateGroupComponent,
    MergeGroupComponent,
    ManageGroupComponent,
    PaginationGroupComponent,
    FilterGroupComponent,
    TableGroupComponent,
  ],
  imports: [
    CommonModule,
    StudentGroupsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FailureNotificationFormComponent,
    SuccessNotificationFormComponent,
    SearchGroupComponent,
  ],
  exports: [PageGroupComponent],
  providers: [StudentGroupsFacadeService],
})
export class StudentGroupsModule {}

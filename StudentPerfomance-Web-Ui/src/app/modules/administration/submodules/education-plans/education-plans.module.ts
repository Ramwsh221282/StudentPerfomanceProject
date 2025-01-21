import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EducationPlansRoutingModule } from './education-plans-routing.module';
import { EducationPlansPageComponent } from './components/education-plans-page/education-plans-page.component';
import { EducationPlansTableComponent } from './components/education-plans-table/education-plans-table.component';
import { EducationPlansPaginationComponent } from './components/education-plans-table/education-plans-pagination/education-plans-pagination.component';
import { EducationPlanDeletionModalComponent } from './components/education-plan-deletion-modal/education-plan-deletion-modal.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { EducationPlanItemComponent } from './components/education-plans-table/education-plan-item/education-plan-item.component';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { EducationPlanItemWorkspaceComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-item-workspace.component';
import { EducationPlanSemesterButtonComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-semester-button/education-plan-semester-button.component';
import { EducationPlanDisciplinesComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/education-plan-disciplines.component';
import { DisciplineItemComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/discipline-item.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { CreateEducationPlanDropdownComponent } from './components/create-education-plan-dropdown/create-education-plan-dropdown.component';
import { DirectionTypeSelectComponent } from '../education-directions/components/education-direction-create-dropdown/direction-type-select/direction-type-select.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { EducationDirectionsSelectComponent } from './components/create-education-plan-dropdown/education-directions-select/education-directions-select.component';
import { FilterEducationPlanDropdownComponent } from './components/filter-education-plan-dropdown/filter-education-plan-dropdown.component';
import { AttachTeacherPopupComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/attach-teacher-popup/attach-teacher-popup.component';
import { DeattachTeacherPopupComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/deattach-teacher-popup/deattach-teacher-popup.component';
import { ChangeDisciplineNamePopupComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/change-discipline-name-popup/change-discipline-name-popup.component';
import { DeleteDisciplinePopupComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/delete-discipline-popup/delete-discipline-popup.component';
import { DropdownListComponent } from '../../../../building-blocks/dropdown-list/dropdown-list.component';
import { AttachTeacherSelectComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/attach-teacher-popup/attach-teacher-select/attach-teacher-select.component';
import { AttachTeacherSelectTeachersComponent } from './components/education-plans-table/education-plan-item-workspace/education-plan-disciplines/discipline-item/attach-teacher-popup/attach-teacher-select-teachers/attach-teacher-select-teachers.component';

@NgModule({
  declarations: [
    EducationPlansPageComponent,
    EducationPlansTableComponent,
    EducationPlansPageComponent,
    EducationPlansPaginationComponent,
    EducationPlanDeletionModalComponent,
    EducationPlanItemComponent,
    EducationPlanItemWorkspaceComponent,
    EducationPlanSemesterButtonComponent,
    EducationPlanDisciplinesComponent,
    DisciplineItemComponent,
    CreateEducationPlanDropdownComponent,
    FilterEducationPlanDropdownComponent,
    AttachTeacherPopupComponent,
    DeattachTeacherPopupComponent,
    ChangeDisciplineNamePopupComponent,
    DeleteDisciplinePopupComponent,
  ],
  imports: [
    CommonModule,
    EducationPlansRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    BlueButtonComponent,
    YellowButtonComponent,
    RedButtonComponent,
    BlueOutlineButtonComponent,
    FloatingLabelInputComponent,
    DirectionTypeSelectComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    EducationDirectionsSelectComponent,
    DropdownListComponent,
    AttachTeacherSelectComponent,
    AttachTeacherSelectTeachersComponent,
  ],
  exports: [EducationPlansPageComponent, DisciplineItemComponent],
})
export class EducationPlansModule {}

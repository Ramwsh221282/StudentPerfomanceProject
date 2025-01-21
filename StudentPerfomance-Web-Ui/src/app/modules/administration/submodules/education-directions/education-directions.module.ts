import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EducationDirectionsPageComponent } from './components/education-directions-page/education-directions-page.component';
import { EducationDirectionsTableComponent } from './components/education-directions-table/education-directions-table.component';
import { EducationDirectionsRoutingModule } from './education-directions-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EducationDirectionsPaginationComponent } from './components/education-directions-table/education-directions-pagination/education-directions-pagination.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { EducationDirectionDeleteModalComponent } from './components/education-direction-delete-modal/education-direction-delete-modal.component';
import { EducationDirectionItemComponent } from './components/education-directions-table/education-direction-item/education-direction-item.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { EducationDirectionCreateDropdownComponent } from './components/education-direction-create-dropdown/education-direction-create-dropdown.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { DirectionTypeSelectComponent } from './components/education-direction-create-dropdown/direction-type-select/direction-type-select.component';
import { EducationDirectionFilterDropdownComponent } from './components/education-direction-filter-dropdown/education-direction-filter-dropdown.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { EducationDirectionEditDropdownComponent } from './components/education-direction-edit-dropdown/education-direction-edit-dropdown.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';

@NgModule({
  declarations: [
    EducationDirectionsPageComponent,
    EducationDirectionsTableComponent,
    EducationDirectionsPaginationComponent,
    EducationDirectionDeleteModalComponent,
    EducationDirectionItemComponent,
    EducationDirectionCreateDropdownComponent,
    EducationDirectionFilterDropdownComponent,
    EducationDirectionEditDropdownComponent,
  ],
  imports: [
    CommonModule,
    EducationDirectionsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    FailureResultNotificationComponent,
    SuccessResultNotificationComponent,
    GreenOutlineButtonComponent,
    BlueButtonComponent,
    RedButtonComponent,
    YellowButtonComponent,
    FloatingLabelInputComponent,
    RedOutlineButtonComponent,
    DirectionTypeSelectComponent,
    BlueOutlineButtonComponent,
    YellowOutlineButtonComponent,
  ],
  exports: [EducationDirectionsPageComponent, EducationDirectionItemComponent],
})
export class EducationDirectionsModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssignmentSessionsPageComponent } from './components/assignment-sessions-page/assignment-sessions-page.component';
import { AssignmentSessionsRoutingModule } from './assignment-sessions-routing.module';
import { AssignmentSessionTableComponent } from './components/assignment-sessions-page/assignment-session-table/assignment-session-table.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuccessResultNotificationComponent } from '../../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../../shared/components/failure-result-notification/failure-result-notification.component';
import { AssignmentSessionCloseItemModalComponent } from './components/assignment-session-close-item-modal/assignment-session-close-item-modal.component';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { SessionItemComponent } from './components/assignment-sessions-page/session-item/session-item.component';
import { SessionItemAttributesComponent } from './components/assignment-sessions-page/session-item/session-item-attributes/session-item-attributes.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { SessionItemGroupAssignmentsComponent } from './components/assignment-sessions-page/session-item/session-item-group-assignments/session-item-group-assignments.component';
import { SessionItemGroupAssignmentComponent } from './components/assignment-sessions-page/session-item/session-item-group-assignments/session-item-group-assignment/session-item-group-assignment.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { CreateSessionItemDropdownComponent } from './components/create-session-item-dropdown/create-session-item-dropdown.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { DropdownListComponent } from '../../../../building-blocks/dropdown-list/dropdown-list.component';

@NgModule({
  declarations: [
    AssignmentSessionsPageComponent,
    AssignmentSessionTableComponent,
    AssignmentSessionCloseItemModalComponent,
    SessionItemComponent,
    SessionItemAttributesComponent,
    SessionItemGroupAssignmentsComponent,
    SessionItemGroupAssignmentComponent,
    CreateSessionItemDropdownComponent,
  ],
  imports: [
    CommonModule,
    AssignmentSessionsRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    BlueButtonComponent,
    BlueOutlineButtonComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    FloatingLabelInputComponent,
    DropdownListComponent,
  ],
  exports: [AssignmentSessionsPageComponent, SessionItemComponent],
})
export class AssignmentSessionsModule {}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../../models/assignment-session-interface';
import { AssignmentSessionWeek } from '../../../../../models/assignment-session-week';

@Component({
  selector: 'app-assignment-session-item-info',
  templateUrl: './assignment-session-item-info.component.html',
  styleUrl: './assignment-session-item-info.component.scss',
})
export class AssignmentSessionItemInfoComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  protected isAssignmentsVisible: boolean = false;
  protected selectedAssignmentWeek: AssignmentSessionWeek;
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../models/assignment-session-interface';

@Component({
  selector: 'app-session-item-attributes',
  templateUrl: './session-item-attributes.component.html',
  styleUrl: './session-item-attributes.component.scss',
})
export class SessionItemAttributesComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() sessionCloseRequested: EventEmitter<AssignmentSession> =
    new EventEmitter();
}

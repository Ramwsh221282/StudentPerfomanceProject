import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSessionDirectionCodePerfomance } from '../../../../../../models/assignment-session-direction-code-perfomance';

@Component({
  selector: 'app-assignment-session-direction-code-info',
  templateUrl: './assignment-session-direction-code-info.component.html',
  styleUrl: './assignment-session-direction-code-info.component.scss',
})
export class AssignmentSessionDirectionCodeInfoComponent {
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Input({ required: true })
  perfomances: AssignmentSessionDirectionCodePerfomance[];
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSessionDirectionPerfomance } from '../../../../../../models/assignment-session-direction-perfomance';

@Component({
  selector: 'app-assignment-session-direction-info',
  templateUrl: './assignment-session-direction-info.component.html',
  styleUrl: './assignment-session-direction-info.component.scss',
})
export class AssignmentSessionDirectionInfoComponent {
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Input({ required: true })
  directionPerfomances: AssignmentSessionDirectionPerfomance[];
}

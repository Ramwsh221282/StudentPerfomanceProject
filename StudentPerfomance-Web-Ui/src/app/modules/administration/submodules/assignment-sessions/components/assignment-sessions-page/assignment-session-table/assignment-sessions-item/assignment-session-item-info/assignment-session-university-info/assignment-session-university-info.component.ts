import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSessionUniversityPerfomance } from '../../../../../../models/assignment-session-university-perfomance';

@Component({
  selector: 'app-assignment-session-university-info',
  templateUrl: './assignment-session-university-info.component.html',
  styleUrl: './assignment-session-university-info.component.scss',
})
export class AssignmentSessionUniversityInfoComponent {
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Input({ required: true })
  perfomance: AssignmentSessionUniversityPerfomance;
}

import { Component, Input } from '@angular/core';
import { AssignmentSession } from '../../../../models/assignment-session-interface';

@Component({
  selector: 'app-assignment-sessions-item',
  templateUrl: './assignment-sessions-item.component.html',
  styleUrl: './assignment-sessions-item.component.scss',
})
export class AssignmentSessionsItemComponent {
  @Input({ required: true }) session: AssignmentSession;
  protected isInfoVisible: boolean = false;
}

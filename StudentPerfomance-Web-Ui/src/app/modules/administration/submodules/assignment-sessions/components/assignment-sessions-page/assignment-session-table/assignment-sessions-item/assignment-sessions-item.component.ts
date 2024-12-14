import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../models/assignment-session-interface';

@Component({
  selector: 'app-assignment-sessions-item',
  templateUrl: './assignment-sessions-item.component.html',
  styleUrl: './assignment-sessions-item.component.scss',
})
export class AssignmentSessionsItemComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() refresh: EventEmitter<void> = new EventEmitter();
  @Output() success: EventEmitter<void> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();
  protected isInfoVisible: boolean = false;
  protected isClosingModalVisible: boolean = false;

  public constructor() {}
}

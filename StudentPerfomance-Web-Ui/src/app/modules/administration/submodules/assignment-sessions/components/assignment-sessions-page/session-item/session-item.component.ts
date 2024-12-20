import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../models/assignment-session-interface';

@Component({
  selector: 'app-session-item',
  templateUrl: './session-item.component.html',
  styleUrl: './session-item.component.scss',
})
export class SessionItemComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() sessionCloseRequested: EventEmitter<AssignmentSession> =
    new EventEmitter();
  protected readonly tabs: any = [
    { id: 1, label: 'Информация' },
    { id: 2, label: 'Проставления' },
  ];

  protected activeTab: number = 1;
}

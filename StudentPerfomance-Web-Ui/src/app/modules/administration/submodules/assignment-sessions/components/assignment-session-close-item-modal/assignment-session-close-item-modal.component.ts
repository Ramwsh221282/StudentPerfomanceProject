import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../models/assignment-session-interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';

@Component({
  selector: 'app-assignment-session-close-item-modal',
  templateUrl: './assignment-session-close-item-modal.component.html',
  styleUrl: './assignment-session-close-item-modal.component.scss',
})
export class AssignmentSessionCloseItemModalComponent implements ISubbmittable {
  @Input({ required: true }) session: AssignmentSession;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange = new EventEmitter();
  @Output() closeCommited: EventEmitter<AssignmentSession> = new EventEmitter();

  public constructor() {}

  public submit(): void {
    this.closeCommited.emit(this.session);
    this.close();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }
}

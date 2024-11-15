import { Component, EventEmitter, Output } from '@angular/core';
import { UserCreationService } from '../../services/user-create.service';

@Component({
  selector: 'app-users-create-modal',
  templateUrl: './users-create-modal.component.html',
  styleUrl: './users-create-modal.component.scss',
  providers: [UserCreationService],
})
export class UsersCreateModalComponent {
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter<void>();

  protected _isCreatingAdmin: boolean;
  protected _isCreatingTeacher: boolean;

  public constructor() {}
}

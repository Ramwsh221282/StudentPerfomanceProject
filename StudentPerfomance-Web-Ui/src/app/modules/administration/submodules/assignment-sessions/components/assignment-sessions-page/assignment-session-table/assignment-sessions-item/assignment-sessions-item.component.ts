import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../models/assignment-session-interface';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-assignment-sessions-item',
  templateUrl: './assignment-sessions-item.component.html',
  styleUrl: './assignment-sessions-item.component.scss',
})
export class AssignmentSessionsItemComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() refresh: EventEmitter<void> = new EventEmitter();
  protected isInfoVisible: boolean = false;
  protected isClosingModalVisible: boolean = false;
  protected isSuccess: boolean = false;
  protected isFailure: boolean = false;

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
  ) {}
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-table-row',
  templateUrl: './table-row.component.html',
  styleUrl: './table-row.component.scss',
  providers: [UserOperationNotificationService],
})
export class TableRowComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected deletionModalState: boolean;
  protected editModalState: boolean;
  protected studentsModalState: boolean;

  public constructor(
    protected notificationService: UserOperationNotificationService
  ) {
    this.deletionModalState = false;
    this.editModalState = false;
    this.studentsModalState = false;
  }

  public openDeletionModal(): void {
    this.deletionModalState = true;
  }

  public closeDeletionModal(value: boolean): void {
    this.deletionModalState = value;
  }

  public openEditModal(): void {
    this.editModalState = true;
  }

  public closeEditModal(value: boolean): void {
    this.editModalState = value;
  }

  public openStudentsModal(): void {
    this.studentsModalState = true;
  }

  public closeStudentsModal(value: boolean): void {
    this.studentsModalState = value;
  }
}

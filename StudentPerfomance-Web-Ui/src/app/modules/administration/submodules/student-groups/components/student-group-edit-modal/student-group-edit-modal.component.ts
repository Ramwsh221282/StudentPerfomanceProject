import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-student-group-edit-modal',
  templateUrl: './student-group-edit-modal.component.html',
  styleUrl: './student-group-edit-modal.component.scss',
})
export class StudentGroupEditModalComponent {
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) copy: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected nameUpdateModalVisibility: boolean;
  protected planAttachmentModalVisibility: boolean;
  protected mergeGroupModalVisibility: boolean;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {}

  protected openNameChangeModal(): void {
    this.nameUpdateModalVisibility = true;
  }

  protected closeNameChangeModal(value: boolean): void {
    this.nameUpdateModalVisibility = value;
  }

  protected openMergeGroupModal(): void {
    this.mergeGroupModalVisibility = true;
  }

  protected closeMergeGroupModal(value: boolean): void {
    this.mergeGroupModalVisibility = value;
  }

  protected openPlanAttachmentModal(): void {
    this.planAttachmentModalVisibility = true;
  }

  protected closePlanAttachmentModal(value: boolean): void {
    this.planAttachmentModalVisibility = value;
  }

  protected refreshGroup(group: StudentGroup): void {
    this.group = group;
    this.copy = group;
  }

  protected close(): void {
    this._facadeService.fetchData();
    this.visibility.emit(false);
  }
}

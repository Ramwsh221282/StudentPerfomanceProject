import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { RemoveGroupService } from './remove-group.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-remove-group-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    RedButtonComponent,
  ],
  templateUrl: './remove-group-dialog.component.html',
  styleUrl: './remove-group-dialog.component.scss',
  standalone: true,
})
export class RemoveGroupDialogComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter<void>();
  @Output() groupRemoved: EventEmitter<StudentGroup> = new EventEmitter();

  public constructor(
    private readonly _service: RemoveGroupService,
    private readonly _notifications: NotificationService,
  ) {}

  public remove(): void {
    this._service
      .remove(this.group)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Студенческая группа удалена');
          this._notifications.success();
          this._notifications.turn();
          this.groupRemoved.emit(this.group);
          this.visibilityChanged.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}

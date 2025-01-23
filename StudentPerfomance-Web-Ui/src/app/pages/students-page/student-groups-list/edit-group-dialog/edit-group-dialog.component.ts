import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { EditGroupService } from './edit-group.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-group-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    RedButtonComponent,
  ],
  templateUrl: './edit-group-dialog.component.html',
  styleUrl: './edit-group-dialog.component.scss',
  standalone: true,
})
export class EditGroupDialogComponent implements OnInit {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public groupCopy: StudentGroup;

  public constructor(
    private readonly _service: EditGroupService,
    private readonly _notifications: NotificationService,
  ) {}

  public ngOnInit() {
    this.groupCopy = { ...this.group };
  }

  public edit(): void {
    this._service
      .edit(this.group, this.groupCopy)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Изменено название группы');
          this._notifications.success();
          this._notifications.turn();
          this.group.name = this.groupCopy.name;
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

  public reset(): void {
    this.ngOnInit();
  }
}

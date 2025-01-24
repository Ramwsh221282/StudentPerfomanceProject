import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { RemoveStudentService } from './remove-student.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-remove-student-dialog',
  imports: [RedButtonComponent, RedOutlineButtonComponent],
  templateUrl: './remove-student-dialog.component.html',
  styleUrl: './remove-student-dialog.component.scss',
  standalone: true,
})
export class RemoveStudentDialogComponent {
  @Input({ required: true }) student: Student;
  @Input({ required: true }) group: StudentGroup;
  @Output() studentRemoved: EventEmitter<Student> = new EventEmitter();
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _service: RemoveStudentService,
  ) {}

  public remove(): void {
    this.student.group = { ...this.group };
    this._service
      .remove(this.student)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Студент был удалён');
          this.studentRemoved.emit(this.student);
          this.visibilityChanged.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}

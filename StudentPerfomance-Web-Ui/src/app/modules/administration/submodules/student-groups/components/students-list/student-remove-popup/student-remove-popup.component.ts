import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentDeletionService } from '../../students-menu-modal/student-deletion-modal/student-deletion.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-student-remove-popup',
  templateUrl: './student-remove-popup.component.html',
  styleUrl: './student-remove-popup.component.scss',
})
export class StudentRemovePopupComponent implements ISubbmittable {
  @Input({ required: true }) student: Student;
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) visibility: boolean = false;
  @Output() studentRemoveCommited: EventEmitter<Student> = new EventEmitter();
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _deletionService: StudentDeletionService,
  ) {}

  public submit(): void {
    this.student.group = { ...this.group };
    this._deletionService
      .remove(this.student)
      .pipe(
        tap((response) => {
          this.studentRemoveCommited.emit(this.student);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable();
        }),
      )
      .subscribe();
  }

  public close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { Student } from '../../../../../modules/administration/submodules/students/models/student.interface';
import { ChangeStudentDataService } from './change-student-data.service';
import { NotificationService } from '../../../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RadioButtonListComponent } from '../../../../../building-blocks/radio-button-list/radio-button-list.component';
import { YellowButtonComponent } from '../../../../../building-blocks/buttons/yellow-button/yellow-button.component';

@Component({
  selector: 'app-change-student-data-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RadioButtonListComponent,
    YellowButtonComponent,
  ],
  templateUrl: './change-student-data-dialog.component.html',
  styleUrl: './change-student-data-dialog.component.scss',
  standalone: true,
})
export class ChangeStudentDataDialogComponent {
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) student: Student;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public studentCopy: Student;

  public newPatronymic: string = '';
  public newRecordbook: string = '';

  public constructor(
    private readonly _service: ChangeStudentDataService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this.newPatronymic = this.student.patronymic ?? '';
    this.newRecordbook = this.student.recordbook.toString();
    this.studentCopy = { ...this.student };
  }

  public edit(): void {
    const newRecordBookNumber = Number(this.newRecordbook);
    if (Number.isNaN(newRecordBookNumber)) {
      this._notifications.bulkFailure(
        'Зачётная книжка студента должна быть числом',
      );
      return;
    }
    this.student.group = { ...this.group };
    this.studentCopy.group = { ...this.group };
    this.studentCopy.recordbook = newRecordBookNumber;
    this._service
      .edit(this.student, this.studentCopy)
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Изменены данные о студенте');
          this.student.name = this.studentCopy.name;
          this.student.surname = this.studentCopy.surname;
          this.student.patronymic = this.studentCopy.patronymic;
          this.student.state = this.studentCopy.state;
          this.student.recordbook = this.studentCopy.recordbook;
          this.student.id = response.id;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public reset(): void {
    this.ngOnInit();
  }
}

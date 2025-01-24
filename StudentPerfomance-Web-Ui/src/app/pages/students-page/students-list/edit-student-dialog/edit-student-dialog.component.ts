import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { EditStudentService } from './edit-student.service';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../../../education-objects-page/education-directions-inline-list/create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { RadioButtonListComponent } from '../../../../building-blocks/radio-button-list/radio-button-list.component';

@Component({
  selector: 'app-edit-student-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
    YellowButtonComponent,
    RadioButtonListComponent,
  ],
  templateUrl: './edit-student-dialog.component.html',
  styleUrl: './edit-student-dialog.component.scss',
  standalone: true,
})
export class EditStudentDialogComponent implements OnInit {
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) student: Student;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public studentCopy: Student;

  public newPatronymic: string = '';
  public newRecordbook: string = '';

  public constructor(
    private readonly _service: EditStudentService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this.newPatronymic =
      this.student.patronymic == null ? '' : this.student.patronymic;
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

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { Student } from '../../../../modules/administration/submodules/students/models/student.interface';
import { CreateStudentService } from './create-student.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { RadioButtonListComponent } from '../../../../building-blocks/radio-button-list/radio-button-list.component';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentPageViewModel } from '../../student-page-viewmodel.service';

@Component({
  selector: 'app-create-student-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    RadioButtonListComponent,
  ],
  templateUrl: './create-student-dialog.component.html',
  styleUrl: './create-student-dialog.component.scss',
  standalone: true,
})
export class CreateStudentDialogComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();

  public name: string = '';
  public surname: string = '';
  public patronymic: string = '';
  public state: string = 'Активен';
  public recordBook: string = '';

  public constructor(
    private readonly _service: CreateStudentService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _viewModel: StudentPageViewModel,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notifications.bulkFailure('Имя студента должно быть указано');
      return;
    }
    if (IsNullOrWhiteSpace(this.surname)) {
      this._notifications.bulkFailure('Фамилия студента должна быть указана');
      return;
    }
    if (IsNullOrWhiteSpace(this.recordBook)) {
      this._notifications.bulkFailure(
        'Зачётная книжка студента должна быть указана',
      );
      return;
    }
    const recordBookNumber = Number(this.recordBook);
    if (Number.isNaN(recordBookNumber)) {
      this._notifications.bulkFailure(
        'Зачётная книжка студента должна быть числом',
      );
      return;
    }
    const student = {} as Student;
    student.group = { ...this.group };
    student.name = this.name;
    student.surname = this.surname;
    student.patronymic = this.patronymic;
    student.state = this.state;
    student.recordbook = recordBookNumber;
    this._service
      .create(student)
      .pipe(
        tap((response) => {
          this._notifications.bulkSuccess('Студент был добавлен в группу');
          this._viewModel.appendStudent(this.group, response);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private cleanInputs(): void {
    this.name = '';
    this.surname = '';
    this.patronymic = '';
    this.state = 'Активен';
    this.recordBook = '';
  }
}

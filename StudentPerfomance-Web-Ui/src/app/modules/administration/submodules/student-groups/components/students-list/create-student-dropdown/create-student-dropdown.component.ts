import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { Student } from '../../../../students/models/student.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-create-student-dropdown',
  templateUrl: './create-student-dropdown.component.html',
  styleUrl: './create-student-dropdown.component.scss',
})
export class CreateStudentDropdownComponent implements ISubbmittable {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() studentAdded: EventEmitter<Student> = new EventEmitter();
  protected name: string = '';
  protected surname: string = '';
  protected patronymic: string = '';
  protected state: string = '';
  protected recordbook: string = '';
  protected isSelectingStatus: boolean = false;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    if (this.isNameEmpty()) return;
    if (this.isSurnameEmpty()) return;
    if (this.isRecordBookEmpty()) return;
    if (this.isRecordBookNotCorrect()) return;
    if (this.isStateEmpty()) return;
    const student = this.createStudent();
    this.studentAdded.emit(student);
    this.closeDropdown();
  }

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private createStudent(): Student {
    const student: Student = {} as Student;
    student.name = this.name;
    student.surname = this.surname;
    student.patronymic = this.patronymic;
    student.state = this.state;
    student.recordbook = Number(this.recordbook);
    return student;
  }

  private isNameEmpty(): boolean {
    if (this.name.length == 0 || this.name.trim().length == 0) {
      this._notificationService.SetMessage = 'Не указано имя студента';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isSurnameEmpty(): boolean {
    if (this.surname.length == 0 || this.surname.trim().length == 0) {
      this._notificationService.SetMessage = 'Не указана фамилия студента';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isStateEmpty(): boolean {
    if (this.state.length == 0 || this.state.trim().length == 0) {
      this._notificationService.SetMessage = 'Не выбран статус студент';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isRecordBookEmpty(): boolean {
    if (this.recordbook.length == 0 || this.recordbook.trim().length == 0) {
      this._notificationService.SetMessage =
        'Не указана зачётная книжка студента';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isRecordBookNotCorrect(): boolean {
    try {
      Number(this.recordbook);
      return false;
    } catch {
      this._notificationService.SetMessage =
        'Зачётная книжка должна состоять только из цифр';
      this._notificationService.failure();
      return true;
    }
  }
}

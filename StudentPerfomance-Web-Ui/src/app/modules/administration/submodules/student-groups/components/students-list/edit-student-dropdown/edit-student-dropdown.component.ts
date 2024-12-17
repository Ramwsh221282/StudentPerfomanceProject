import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentEditService } from '../../students-menu-modal/student-edit-modal/student-edit.service';

@Component({
  selector: 'app-edit-student-dropdown',
  templateUrl: './edit-student-dropdown.component.html',
  styleUrl: './edit-student-dropdown.component.scss',
})
export class EditStudentDropdownComponent implements OnInit, ISubbmittable {
  @Input({ required: true }) student: Student;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  protected newName: string = '';
  protected newSurname: string = '';
  protected newPatronymic: string = '';
  protected newState: string = '';
  protected newRecordbook: string = '';
  protected isSelectingStatus: boolean = false;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _editService: StudentEditService,
  ) {}

  public submit(): void {
    if (this.isNewNameEmpty()) return;
    if (this.isNewSurnameEmpty()) return;
    if (this.isNewRecordbookEmpty()) return;
    if (this.isNewRecordbookNotCorrect()) return;
    if (this.isNewStateEmpty()) return;
    const updatedStudent = this.createUpdatedStudent();
    this.updateCurrentStudent(updatedStudent);
    this.closeDropdown();
  }

  public ngOnInit(): void {
    this.newName = this.student.name;
    this.newSurname = this.student.surname;
    this.newPatronymic = this.student.patronymic;
    this.newState = this.student.state;
    this.newRecordbook = String(this.student.recordbook);
  }

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private updateCurrentStudent(newStudent: Student): void {
    this.student.name = newStudent.name;
    this.student.surname = newStudent.surname;
    this.student.recordbook = newStudent.recordbook;
    this.student.patronymic = newStudent.patronymic;
    this.student.state = newStudent.state;
  }

  private createUpdatedStudent(): Student {
    const student = {} as Student;
    student.group = { ...this.student.group };
    student.name = this.newName;
    student.surname = this.newSurname;
    student.patronymic = this.newPatronymic;
    student.recordbook = Number(this.newRecordbook);
    student.state = this.newState;
    return student;
  }

  private isNewNameEmpty(): boolean {
    if (this.newName.length == 0 || this.newName.trim().length == 0) {
      this._notificationService.SetMessage = 'Новое имя студента было пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isNewSurnameEmpty(): boolean {
    if (this.newSurname.length == 0 || this.newSurname.trim().length == 0) {
      this._notificationService.SetMessage =
        'Новая фамилия студента была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isNewStateEmpty(): boolean {
    if (this.newState.length == 0 || this.newState.trim().length == 0) {
      this._notificationService.SetMessage = 'Новый статус студента был пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isNewRecordbookEmpty(): boolean {
    if (
      this.newRecordbook.length == 0 ||
      this.newRecordbook.trim().length == 0
    ) {
      this._notificationService.SetMessage =
        'Новая зачётная книжка студента была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isNewRecordbookNotCorrect(): boolean {
    try {
      Number(this.newRecordbook);
      return false;
    } catch {
      this._notificationService.SetMessage =
        'Новая зачётная книжка должна содержать только цифры';
      this._notificationService.failure();
      return true;
    }
  }
}

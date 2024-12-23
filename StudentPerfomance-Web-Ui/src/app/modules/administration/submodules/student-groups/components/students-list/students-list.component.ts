import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Student } from '../../../students/models/student.interface';
import { StudentCreationService } from '../../services/student-creation.service';
import { catchError, Observable, tap } from 'rxjs';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrl: './students-list.component.scss',
})
export class StudentsListComponent {
  @Input({ required: true }) group: StudentGroup;
  @Output() studentMovedToOtherGroup: EventEmitter<Student> =
    new EventEmitter();
  protected createVisibility: boolean = false;

  // for student attributes editing
  protected isEditing: boolean = false;
  protected studentToEdit: Student | null;

  // for changing group of student
  protected isMoveToOtherGroup: boolean = false;
  protected studentToMoveInOtherGroup: Student | null;
  protected studentThatMovedInOtherGroup: Student | null;

  // for removing student from his group
  protected isRemovingStudent: boolean = false;
  protected studentToRemove: Student | null;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _creationService: StudentCreationService,
  ) {}

  protected handleStudentCreation(student: Student) {
    student = this.addStudentInGroup(student);
    this._creationService
      .create(student)
      .pipe(
        tap((response) => {
          const surname: string = response.surname;
          const name = response.name[0];
          const patronymic =
            response.patronymic.length == 0 ? ' ' : response.patronymic[0];
          this._notificationService.SetMessage = `Студент ${surname} ${name} ${patronymic == undefined ? ' ' : patronymic[0]} добавлен в группу ${this.group.name}`;
          this._notificationService.success();
          this.sortStudentsBySurname();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.group.students.pop();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected handleStudentGroupSwitch(group: StudentGroup): void {
    if (!this.studentThatMovedInOtherGroup) {
      this._notificationService.SetMessage =
        'Не выбран студент для перевода в другую группу или не выбрана группа';
      this._notificationService.failure();
      return;
    }
    this.studentThatMovedInOtherGroup.group = { ...group };
    this.studentMovedToOtherGroup.emit(this.studentThatMovedInOtherGroup);
    this.studentThatMovedInOtherGroup = null;
    this.studentToMoveInOtherGroup = null;
  }

  protected handleStudentDeletion(student: Student): void {
    this.group.students = this.group.students.filter(
      (s) => s.recordbook != student.recordbook,
    );
    this.sortStudentsBySurname();
    this.isRemovingStudent = false;
    this.studentToRemove = null;
  }

  private addStudentInGroup(student: Student): Student {
    this.group.students.push(student);
    student.group = { ...this.group };
    return student;
  }

  private sortStudentsBySurname(): void {
    this.group.students.sort((a, b) =>
      a.surname > b.surname ? 1 : a.surname < b.surname ? -1 : 0,
    );
  }
}

import { Component, Input } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Student } from '../../../students/models/student.interface';
import { StudentCreationService } from '../students-menu-modal/student-creation-modal/student-creation.service';
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
  protected createVisibility: boolean = false;

  protected isEditing: boolean = false;
  protected studentToEdit: Student | null;

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
          this._notificationService.SetMessage = `Студент ${response.surname} ${response.name[0]} ${response.patronymic == null ? '' : response.patronymic[0]} добавлен в группу ${this.group.name}`;
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

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student } from '../../../../../modules/administration/submodules/students/models/student.interface';
import { StudentGroup } from '../../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { NotificationService } from '../../../../../building-blocks/notifications/notification.service';
import { StudentGroupsDataService } from '../../../student-groups-list/student-groups-data.service';
import { UnauthorizedErrorHandler } from '../../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { MoveStudentService } from './move-student.service';
import { StudentPageViewModel } from '../../../student-page-viewmodel.service';
import { NgForOf, NgIf } from '@angular/common';
import { GreenOutlineButtonComponent } from '../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';

@Component({
  selector: 'app-move-student-dialog',
  imports: [NgForOf, NgIf, GreenOutlineButtonComponent],
  templateUrl: './move-student-dialog.component.html',
  styleUrl: './move-student-dialog.component.scss',
  standalone: true,
})
export class MoveStudentDialogComponent implements OnInit {
  @Input({ required: true }) student: Student;
  @Input({ required: true }) currentGroup: StudentGroup;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public availableGroups: StudentGroup[] = [];
  public selectedGroup: StudentGroup | null = null;

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _groupsDataService: StudentGroupsDataService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _service: MoveStudentService,
    private readonly _viewModel: StudentPageViewModel,
  ) {}

  public move(): void {
    if (!this.selectedGroup) {
      this._notifications.bulkFailure('Необходимо выбрать группу');
      return;
    }
    this._service
      .moveStudent(this.student, this.currentGroup, this.selectedGroup)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Студент переведён в другую группу');
          this._viewModel.removeStudent(this.currentGroup, this.student);
          this._viewModel.appendStudent(this.selectedGroup!, this.student);
          this.visibilityChanged.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.bulkFailure(error.error);
          this.visibilityChanged.emit();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public ngOnInit() {
    this._groupsDataService
      .getGroups()
      .pipe(
        tap((response) => {
          this.availableGroups = response;
          const currentGroup = this.availableGroups.find(
            (group) => group.id == this.currentGroup.id,
          )!;
          const indexOfCurrentGroups =
            this.availableGroups.indexOf(currentGroup);
          if (indexOfCurrentGroups >= 0)
            this.availableGroups.splice(indexOfCurrentGroups, 1);
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public selectGroup(group: StudentGroup, $event: MouseEvent): void {
    $event.stopPropagation();
    this.selectedGroup = { ...group };
  }
}

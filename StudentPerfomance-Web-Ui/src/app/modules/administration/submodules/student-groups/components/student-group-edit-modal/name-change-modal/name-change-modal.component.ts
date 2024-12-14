import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { StudentGroupsFacadeService } from '../../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { GroupNameChangeHandler } from './group-change-name-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { EducationPlan } from '../../../../education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../education-directions/models/education-direction-interface';

@Component({
  selector: 'app-name-change-modal',
  templateUrl: './name-change-modal.component.html',
  styleUrl: './name-change-modal.component.scss',
})
export class NameChangeModalComponent implements ISubbmittable, OnInit {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() returnGroup: EventEmitter<StudentGroup> =
    new EventEmitter<StudentGroup>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  protected editableGroup: StudentGroup;
  protected copy: StudentGroup;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {}

  public ngOnInit(): void {
    this.editableGroup = { ...this.group };
    this.copy = { ...this.group };
  }

  public submit(): void {
    const handler = GroupNameChangeHandler(
      this.notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.refreshEmitter,
      this.copy,
    );

    this.copy.plan = {} as EducationPlan;
    this.copy.plan.direction = {} as EducationDirection;
    this.editableGroup.plan = {} as EducationPlan;
    this.editableGroup.plan.direction = {} as EducationDirection;

    this._facadeService
      .update(this.copy, this.editableGroup)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.group = { ...response };
        }),
        catchError((error: HttpErrorResponse) => handler.handleError(error)),
      )
      .subscribe();
    this.ngOnInit();
  }

  public close(): void {
    this.returnGroup.emit(this.group);
    this.visibility.emit(false);
  }
}

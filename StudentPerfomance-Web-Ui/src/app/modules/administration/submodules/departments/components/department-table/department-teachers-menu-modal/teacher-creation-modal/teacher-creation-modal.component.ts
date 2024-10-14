import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  output,
  Output,
} from '@angular/core';
import { TeacherCreationService } from './teacher-creation.service';
import { Department } from '../../../../models/departments.interface';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { BaseTeacherForm } from '../../../../../teachers/models/base-teacher-form';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { TeacherCreationHandler } from './teacher-creation-handler';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-teacher-creation-modal',
  templateUrl: './teacher-creation-modal.component.html',
  styleUrl: './teacher-creation-modal.component.scss',
  providers: [TeacherCreationService],
})
export class TeacherCreationModalComponent
  extends BaseTeacherForm
  implements ISubbmittable, OnInit
{
  @Input({ required: true }) department: Department;
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter<void>();

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _creationService: TeacherCreationService
  ) {
    super();
  }

  public submit(): void {
    const teacher: Teacher = this.createTeacherFromForm(this.department);
    const handler = TeacherCreationHandler(
      this.notificationService,
      this.successEmitter,
      this.failureEmitter
    );
    console.log(teacher);
    this._creationService
      .create(teacher)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.refreshEmitter.emit();
        }),
        catchError((error) => handler.handleError(error))
      )
      .subscribe();
  }

  public ngOnInit(): void {
    this.initForm();
  }
}

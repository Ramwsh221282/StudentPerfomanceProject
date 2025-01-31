import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { RemoveDisciplineService } from './remove-discipline.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SemesterPlan } from '../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../modules/administration/submodules/semesters/models/semester.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-remove-discipline-dialog',
  imports: [RedButtonComponent, RedOutlineButtonComponent],
  templateUrl: './remove-discipline-dialog.component.html',
  styleUrl: './remove-discipline-dialog.component.scss',
  standalone: true,
})
export class RemoveDisciplineDialogComponent {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;
  @Output() disciplineRemoved: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _service: RemoveDisciplineService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public delete(): void {
    const disciplinePayload: SemesterPlan = {} as SemesterPlan;
    disciplinePayload.discipline = this.discipline.disciplineName;
    const semesterPayload: Semester = {} as Semester;
    semesterPayload.number = this.semester.number;
    this._service
      .remove(
        disciplinePayload,
        semesterPayload,
        this.educationPlan,
        this.direction,
      )
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Дисциплина была удалена');
          this.disciplineRemoved.emit(this.discipline);
          this.visibilityChange.emit();
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.bulkFailure(error.error);
          this.visibilityChange.emit();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}

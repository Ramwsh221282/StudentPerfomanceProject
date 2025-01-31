import { Component, Input, OnInit } from '@angular/core';
import { GreenOutlineButtonComponent } from '../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { YellowButtonComponent } from '../../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { FloatingLabelInputComponent } from '../../../../../building-blocks/floating-label-input/floating-label-input.component';
import { ChangeDisciplineNameService } from './change-discipline-name.service';
import { NotificationService } from '../../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../../modules/administration/submodules/semesters/models/semester.interface';
import { UnauthorizedErrorHandler } from '../../../../../shared/models/common/401-error-handler/401-error-handler.service';

@Component({
  selector: 'app-change-discipline-name-part',
  imports: [
    GreenOutlineButtonComponent,
    YellowButtonComponent,
    FloatingLabelInputComponent,
  ],
  templateUrl: './change-discipline-name-part.component.html',
  styleUrl: './change-discipline-name-part.component.scss',
  standalone: true,
})
export class ChangeDisciplineNamePartComponent implements OnInit {
  @Input({ required: true }) discipline: SemesterDiscipline;
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;
  public disciplineCopy: SemesterDiscipline;

  public constructor(
    private readonly _service: ChangeDisciplineNameService,
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this.disciplineCopy = { ...this.discipline };
  }

  public edit(): void {
    const initialPayload = {} as SemesterPlan;
    initialPayload.discipline = this.discipline.disciplineName;
    const updatedPayload = {} as SemesterPlan;
    updatedPayload.discipline = this.disciplineCopy.disciplineName;
    const semesterPayload = {} as Semester;
    semesterPayload.number = this.semester.number;
    this._service
      .changeName(
        initialPayload,
        updatedPayload,
        semesterPayload,
        this.educationPlan,
        this.direction,
      )
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Изменено название дисциплины');
          this.discipline.disciplineName = this.disciplineCopy.disciplineName;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          this._notifications.setMessage(error.error);
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

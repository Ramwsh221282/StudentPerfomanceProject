import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { CreateDisciplineService } from './create-discipline.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectDirectionTypeDropdownComponent } from '../../education-directions-inline-list/create-education-direction-dialog/select-direction-type-dropdown/select-direction-type-dropdown.component';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { SemesterPlan } from '../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { Semester } from '../../../../modules/administration/submodules/semesters/models/semester.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-discipline-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectDirectionTypeDropdownComponent,
  ],
  templateUrl: './create-discipline-dialog.component.html',
  styleUrl: './create-discipline-dialog.component.scss',
  standalone: true,
})
export class CreateDisciplineDialogComponent {
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) direction: EducationDirection;
  @Output() disciplineCreated: EventEmitter<SemesterDiscipline> =
    new EventEmitter();
  @Output() visibilityChange: EventEmitter<void> = new EventEmitter();

  public discipline: string = '';

  public constructor(
    private readonly _service: CreateDisciplineService,
    private readonly _notifications: NotificationService,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.discipline)) {
      this._notifications.setMessage('Название дисциплины должно быть указано');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    const disciplinePayload: SemesterPlan = {} as SemesterPlan;
    disciplinePayload.discipline = this.discipline;
    const semesterPayload: Semester = {} as Semester;
    semesterPayload.number = this.semester.number;
    this._service
      .create(
        disciplinePayload,
        semesterPayload,
        this.educationPlan,
        this.direction,
      )
      .pipe(
        tap((response) => {
          this._notifications.setMessage('Добавлена новая дисциплина');
          this._notifications.success();
          this._notifications.turn();
          const addedDiscipline: SemesterDiscipline = {} as SemesterDiscipline;
          addedDiscipline.disciplineName = response.discipline;
          addedDiscipline.teacher = response.teacher;
          this.disciplineCreated.emit(addedDiscipline);
          this.updateInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private updateInputs(): void {
    this.discipline = '';
  }
}

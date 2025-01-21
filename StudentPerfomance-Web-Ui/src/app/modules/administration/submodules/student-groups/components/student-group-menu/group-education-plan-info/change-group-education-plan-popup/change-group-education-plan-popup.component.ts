import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirection } from '../../../../../education-directions/models/education-direction-interface';
import { GreenOutlineButtonComponent } from '../../../../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { FloatingLabelInputComponent } from '../../../../../../../../building-blocks/floating-label-input/floating-label-input.component';
import { DropdownListComponent } from '../../../../../../../../building-blocks/dropdown-list/dropdown-list.component';
import { NgIf } from '@angular/common';
import { EducationDirectionsSelectComponent } from '../../../../../education-plans/components/create-education-plan-dropdown/education-directions-select/education-directions-select.component';
import {
  EducationPlan,
  EducationPlanSemester,
} from '../../../../../education-plans/models/education-plan-interface';
import { EducationPlanAttachmentService } from '../../../../services/education-plan-attachment.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-change-group-education-plan-popup',
    imports: [
        GreenOutlineButtonComponent,
        RedOutlineButtonComponent,
        FloatingLabelInputComponent,
        DropdownListComponent,
        NgIf,
        EducationDirectionsSelectComponent,
    ],
    templateUrl: './change-group-education-plan-popup.component.html',
    styleUrl: './change-group-education-plan-popup.component.scss'
})
export class ChangeGroupEducationPlanPopupComponent implements ISubbmittable {
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();

  protected isSelectingDirection: boolean = false;
  protected selectDirectionLabel: string = 'Выберите направление подготовки';
  protected selectedDirection: EducationDirection | null = null;

  protected isSelectingEducationPlan: boolean = false;
  protected selectEducationPlanLabel: string = 'Выберите год учебного плана';
  protected selectedEducationPlan: EducationPlan | null = null;
  protected educationPlans: EducationPlan[] = [];

  protected selectSemesterLabel: string = 'Выберите текущий семестр группы';
  protected isSelectingActiveSemester: boolean = false;
  protected selectedSemester: EducationPlanSemester | null = null;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _planAttachmentService: EducationPlanAttachmentService,
  ) {}

  public submit(): void {
    if (this.isDirectionWasNotSelected()) return;
    if (this.isEducationPlanNotSelected()) return;
    if (this.isSemesterNotSelected()) return;
    this.selectedEducationPlan!.direction = { ...this.selectedDirection! };
    this.group.plan = { ...this.selectedEducationPlan! };
    this.group.activeSemesterNumber = this.selectedSemester!.number;
    this._planAttachmentService
      .attachPlan(
        this.group,
        this.selectedEducationPlan!,
        this.selectedSemester!.number,
      )
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Группе ${this.group.name} задан учебный план ${this.selectedDirection!.name} ${this.selectedDirection!.code} ${this.selectedDirection!.type} ${this.selectedEducationPlan!.year} с семестром ${this.selectedSemester!.number}`;
          this._notificationService.success();
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected handleDirectionSelect(direction: EducationDirection): void {
    this.selectDirectionLabel = `${direction.name} ${direction.code} ${direction.type}`;
    this.selectedDirection = direction;
    this.educationPlans = direction.plans;
  }

  protected handlePlanSelect(planYear: string): void {
    this.selectEducationPlanLabel = planYear;
    this.selectedEducationPlan = this.educationPlans.find(
      (plan) => String(plan.year) == planYear,
    )!;
  }

  protected handleSemesterSelect(semesterNumber: string): void {
    this.selectSemesterLabel = semesterNumber;
    this.selectedSemester = this.selectedEducationPlan!.semesters.find(
      (semester) => String(semester.number) == semesterNumber,
    )!;
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChanged.emit(this.visibility);
  }

  protected getPlanYearsAsStringArray(): string[] {
    return this.educationPlans.map((plan) => String(plan.year));
  }

  protected getSemesterNumbersAsStringArray(): string[] {
    return this.selectedEducationPlan!.semesters.map((semester) =>
      String(semester.number),
    );
  }

  private isDirectionWasNotSelected(): boolean {
    if (this.selectedDirection == null) {
      this._notificationService.SetMessage =
        'Направление подготовки не выбрано';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isEducationPlanNotSelected(): boolean {
    if (this.selectedEducationPlan == null) {
      this._notificationService.SetMessage = 'Учебный план не выбран';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isSemesterNotSelected(): boolean {
    if (this.selectedSemester == null) {
      this._notificationService.SetMessage = 'Семестр не был выбран';
      this._notificationService.failure();
      return true;
    }
    return false;
  }
}

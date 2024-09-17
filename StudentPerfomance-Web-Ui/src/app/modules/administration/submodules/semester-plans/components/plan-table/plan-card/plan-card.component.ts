import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlan } from '../../../models/semester-plan.interface';
import { SemesterPlanFacadeService } from '../../../services/semester-plan-facade.service';
import { Teacher } from '../../../../teachers/models/teacher.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-plan-card',
  templateUrl: './plan-card.component.html',
  styleUrl: './plan-card.component.scss',
})
export class PlanCardComponent implements OnInit {
  @Input({ required: true }) public semesterPlan: SemesterPlan;
  protected isTeacherAttached: boolean;
  protected teacherState: string;
  protected isModalEnabled: boolean;

  public constructor(
    protected readonly facadeService: SemesterPlanFacadeService
  ) {
    this.isTeacherAttached = false;
  }

  public ngOnInit(): void {
    this.isTeacherAssigned();
    this.isModalEnabled = false;
  }

  protected openCardInfoModal(): void {
    this.isModalEnabled = true;
  }

  protected checkModalState(state: boolean): void {
    this.isModalEnabled = state;
  }

  protected getTeacherInfoFromModal(teacher: Teacher): void {
    this.facadeService
      .assignTeacher(teacher, this.semesterPlan)
      .pipe(
        tap((response) => {
          this.semesterPlan = response;
          this.isTeacherAssigned();
        }),
        catchError((error: HttpErrorResponse) => new Observable())
      )
      .subscribe();
  }

  private isTeacherAssigned(): void {
    if (
      [
        this.semesterPlan.attachedTeacherName,
        this.semesterPlan.attachedTeacherSurname,
      ].some((item) => item == null || item == undefined || item.length == 0)
    ) {
      this.isTeacherAttached = false;
      this.teacherState = 'Преподаватель не закреплён';
    } else {
      this.teacherState = `Преподаватель: ${this.semesterPlan.attachedTeacherSurname} ${this.semesterPlan.attachedTeacherName[0]}. ${this.semesterPlan.attachedTeacherThirdname}.`;
      this.isTeacherAttached = true;
    }
  }
}

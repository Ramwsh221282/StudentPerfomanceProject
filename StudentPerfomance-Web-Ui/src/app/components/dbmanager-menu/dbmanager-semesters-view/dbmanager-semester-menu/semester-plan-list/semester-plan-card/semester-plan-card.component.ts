import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlan } from '../../models/semester-plan.interface';
import { NgClass, NgIf } from '@angular/common';
import { SemesterPlanCardInfoComponent } from './semester-plan-card-info/semester-plan-card-info.component';
import { Teacher } from '../../../../dbmanager-departments-view/department-menu/models/teacher.interface';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-semester-plan-card',
  standalone: true,
  imports: [NgClass, SemesterPlanCardInfoComponent, NgIf],
  templateUrl: './semester-plan-card.component.html',
  styleUrl: './semester-plan-card.component.scss',
})
export class SemesterPlanCardComponent implements OnInit {
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

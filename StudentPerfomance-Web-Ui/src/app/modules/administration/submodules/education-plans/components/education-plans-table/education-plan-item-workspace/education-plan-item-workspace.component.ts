import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../models/education-plan-interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-education-plan-item-workspace',
  templateUrl: './education-plan-item-workspace.component.html',
  styleUrl: './education-plan-item-workspace.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EducationPlanItemWorkspaceComponent {
  @Input() selectedEducationPlan: EducationPlan;
  @Input() selectedSemester: EducationPlanSemester | null;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  protected handleDisciplineTeacherAttachment(
    discipline: SemesterDiscipline,
  ): void {
    if (!this.selectedSemester) return;
    for (let semesterDiscipline of this.selectedSemester.disciplines) {
      if (semesterDiscipline.disciplineName == discipline.disciplineName) {
        semesterDiscipline = discipline;
        this._notificationService.SetMessage = `Дисциплине ${semesterDiscipline.disciplineName} закреплён преподаватель ${semesterDiscipline.teacher.surname} ${semesterDiscipline.teacher.name[0]}. ${semesterDiscipline.teacher.patronymic == null ? '' : semesterDiscipline.teacher.patronymic[0]}`;
        this._notificationService.success();
      }
    }
  }

  protected handleDisciplineTeacherDeattached(
    disciplineWithoutTeacher: SemesterDiscipline,
  ): void {
    if (!this.selectedSemester) return;
    for (let semesterDiscipline of this.selectedSemester.disciplines) {
      if (
        semesterDiscipline.disciplineName ==
        disciplineWithoutTeacher.disciplineName
      ) {
        semesterDiscipline.teacher = null!;
        this._notificationService.SetMessage = `У дисциплины ${semesterDiscipline.disciplineName} откреплён преподаватель`;
        this._notificationService.success();
      }
    }
  }

  protected handleDisciplineNameUpdate(
    disciplines: SemesterDiscipline[],
  ): void {
    if (!this.selectedSemester) return;
    const initialDiscipline = disciplines[1];
    const oldName = initialDiscipline.disciplineName;
    const updatedDiscipline = disciplines[0];
    for (let semesterDiscipline of this.selectedSemester.disciplines) {
      if (
        semesterDiscipline.disciplineName == initialDiscipline.disciplineName
      ) {
        semesterDiscipline.disciplineName = updatedDiscipline.disciplineName;
        this._notificationService.SetMessage = `Дисциплина ${oldName} переименована в ${updatedDiscipline.disciplineName}`;
        this._notificationService.success();
      }
    }
  }

  protected handleDisciplineDeletion(
    deletedDiscipline: SemesterDiscipline,
  ): void {
    if (!this.selectedSemester) return;
    const name = deletedDiscipline.disciplineName;
    for (const discipline of this.selectedSemester.disciplines) {
      if (discipline.disciplineName == deletedDiscipline.disciplineName) {
        const index = this.selectedSemester.disciplines.indexOf(discipline);
        if (index > -1) {
          this.selectedSemester.disciplines.splice(index, 1);
          this._notificationService.SetMessage = `Дисциплина ${name} удалена из ${this.selectedSemester.number} семестра учебного плана ${this.selectedEducationPlan.year} года направления ${this.selectedEducationPlan.direction.name}`;
          this._notificationService.success();
        }
      }
    }
  }
}

import { Component, Input } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import {
  EducationPlan,
  EducationPlanSemester,
  SemesterDiscipline,
} from '../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { DisciplineItemComponent } from './discipline-item/discipline-item.component';
import { NgForOf, NgIf } from '@angular/common';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { CreateDisciplineDialogComponent } from './create-discipline-dialog/create-discipline-dialog.component';
import { RemoveDisciplineDialogComponent } from './remove-discipline-dialog/remove-discipline-dialog.component';
import { EditDisciplineDialogComponent } from './edit-discipline-dialog/edit-discipline-dialog.component';

@Component({
  selector: 'app-disciplines-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    DisciplineItemComponent,
    NgForOf,
    CreateDisciplineDialogComponent,
    NgIf,
    RemoveDisciplineDialogComponent,
    EditDisciplineDialogComponent,
  ],
  templateUrl: './disciplines-list.component.html',
  styleUrl: './disciplines-list.component.scss',
  standalone: true,
})
export class DisciplinesListComponent {
  @Input({ required: true }) semester: EducationPlanSemester;
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) direction: EducationDirection;

  public isCreatingDiscipline: boolean = false;
  public removeDisciplineRequest: SemesterDiscipline | null = null;
  public editDisciplineRequest: SemesterDiscipline | null = null;

  public handleDisciplineCreated(discipline: SemesterDiscipline): void {
    this.semester.disciplines.push(discipline);
  }

  public handleDisciplineRemoved(discipline: SemesterDiscipline): void {
    this.semester.disciplines.splice(
      this.semester.disciplines.indexOf(discipline),
      1,
    );
  }
}

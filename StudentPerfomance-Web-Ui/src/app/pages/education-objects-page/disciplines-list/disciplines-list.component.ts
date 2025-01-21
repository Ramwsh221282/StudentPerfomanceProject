import { Component, Input } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { EducationPlanSemester } from '../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { DisciplineItemComponent } from './discipline-item/discipline-item.component';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-disciplines-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    DisciplineItemComponent,
    NgForOf,
  ],
  templateUrl: './disciplines-list.component.html',
  styleUrl: './disciplines-list.component.scss',
  standalone: true,
})
export class DisciplinesListComponent {
  @Input({ required: true }) semester: EducationPlanSemester;
}

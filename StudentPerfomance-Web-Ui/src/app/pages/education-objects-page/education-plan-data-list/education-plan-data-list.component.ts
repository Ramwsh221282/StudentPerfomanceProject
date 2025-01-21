import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { EducationPlanDataItemComponent } from './education-plan-data-item/education-plan-data-item.component';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NgForOf } from '@angular/common';
import { EducationPlan } from '../../../modules/administration/submodules/education-plans/models/education-plan-interface';

@Component({
  selector: 'app-education-plan-data-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    EducationPlanDataItemComponent,
    NgForOf,
  ],
  templateUrl: './education-plan-data-list.component.html',
  styleUrl: './education-plan-data-list.component.scss',
  standalone: true,
})
export class EducationPlanDataListComponent {
  @Input({ required: true }) direction: EducationDirection;
  @Output() selectedEducationPlan: EventEmitter<EducationPlan> =
    new EventEmitter();
  public currentEducationPlan: EducationPlan | null = null;
}

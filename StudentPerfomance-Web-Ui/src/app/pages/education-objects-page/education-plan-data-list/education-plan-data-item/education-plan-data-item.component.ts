import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationPlan } from '../../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';
import { NgClass, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-education-plan-data-item',
  templateUrl: './education-plan-data-item.component.html',
  styleUrl: './education-plan-data-item.component.scss',
  standalone: true,
  imports: [
    EditIconButtonComponent,
    RemoveIconButtonComponent,
    NgClass,
    NgOptimizedImage,
  ],
})
export class EducationPlanDataItemComponent {
  @Input({ required: true }) educationPlan: EducationPlan;
  @Input({ required: true }) isCurrentPlan: boolean = false;
  @Output() selectedPlan: EventEmitter<EducationPlan> = new EventEmitter();
}

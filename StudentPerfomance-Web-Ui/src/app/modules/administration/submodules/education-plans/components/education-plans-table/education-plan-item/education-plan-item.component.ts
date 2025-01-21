import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationPlan } from '../../../models/education-plan-interface';

@Component({
    selector: 'app-education-plan-item',
    templateUrl: './education-plan-item.component.html',
    styleUrl: './education-plan-item.component.scss',
    standalone: false
})
export class EducationPlanItemComponent {
  @Input() currentSelected: EducationPlan | null = null;
  @Input({ required: true }) plan: EducationPlan;
  @Output() itemSelectionChanged = new EventEmitter<EducationPlan>();
  @Output() itemDeletionRequested = new EventEmitter<EducationPlan>();
}

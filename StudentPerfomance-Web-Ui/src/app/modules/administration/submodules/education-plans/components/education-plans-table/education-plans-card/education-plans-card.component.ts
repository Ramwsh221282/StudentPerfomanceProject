import { Component, Input } from '@angular/core';
import { EducationPlan } from '../../../models/education-plan-interface';

@Component({
  selector: 'app-education-plans-card',
  templateUrl: './education-plans-card.component.html',
  styleUrl: './education-plans-card.component.scss',
})
export class EducationPlansCardComponent {
  @Input({ required: true }) public plan: EducationPlan;
}

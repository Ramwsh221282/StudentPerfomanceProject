import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationPlan } from '../../../models/education-plan-interface';

@Component({
  selector: 'app-education-plans-table-row',
  templateUrl: './education-plans-table-row.component.html',
  styleUrl: './education-plans-table-row.component.scss',
})
export class EducationPlansTableRowComponent {
  @Input({ required: true }) plan: EducationPlan;
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  protected deletionModalVisibility: boolean;
  protected semestersInfoVisibility: boolean;

  public constructor() {
    this.deletionModalVisibility = false;
  }
}

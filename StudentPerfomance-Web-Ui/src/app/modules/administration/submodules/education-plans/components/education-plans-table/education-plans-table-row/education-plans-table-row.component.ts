import { Component, Input } from '@angular/core';
import { EducationPlan } from '../../../models/education-plan-interface';

@Component({
  selector: 'app-education-plans-table-row',
  templateUrl: './education-plans-table-row.component.html',
  styleUrl: './education-plans-table-row.component.scss',
})
export class EducationPlansTableRowComponent {
  @Input({ required: true }) plan: EducationPlan;
  protected deletionModalVisibility: boolean;
  protected semestersInfoVisibility: boolean;

  public constructor() {
    this.deletionModalVisibility = false;
  }

  protected startDeletion(): void {
    this.deletionModalVisibility = true;
  }

  protected manageDeletion(value: boolean): void {
    this.deletionModalVisibility = value;
  }

  protected openSemestersInfo(): void {
    this.semestersInfoVisibility = true;
  }

  protected closeSemestersInfo(value: boolean): void {
    this.semestersInfoVisibility = value;
  }
}

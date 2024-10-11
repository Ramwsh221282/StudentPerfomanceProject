import { Component, Input } from '@angular/core';
import { EducationDirection } from '../../../models/education-direction-interface';

@Component({
  selector: 'app-education-directions-table-row',
  templateUrl: './education-directions-table-row.component.html',
  styleUrl: './education-directions-table-row.component.scss',
})
export class EducationDirectionsTableRowComponent {
  @Input({ required: true }) public Direction: EducationDirection;

  protected deletionModalVisibility: boolean;
  protected updateModalVisibility: boolean;

  public constructor() {
    this.deletionModalVisibility = false;
    this.updateModalVisibility = false;
  }

  protected startDeletion(): void {
    this.deletionModalVisibility = true;
  }

  protected stopDeletion(value: boolean): void {
    this.deletionModalVisibility = value;
  }

  protected startUpdate(): void {
    this.updateModalVisibility = true;
  }

  protected stopUpdate(value: boolean): void {
    this.updateModalVisibility = value;
  }
}

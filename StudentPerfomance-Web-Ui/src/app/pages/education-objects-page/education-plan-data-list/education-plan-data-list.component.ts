import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { EducationPlanDataItemComponent } from './education-plan-data-item/education-plan-data-item.component';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NgForOf, NgIf } from '@angular/common';
import { EducationPlan } from '../../../modules/administration/submodules/education-plans/models/education-plan-interface';
import { CreateEducationPlanDialogComponent } from './create-education-plan-dialog/create-education-plan-dialog.component';
import { RemoveEducationPlanDialogComponent } from './remove-education-plan-dialog/remove-education-plan-dialog.component';
import { EditEducationPlanDialogComponent } from './edit-education-plan-dialog/edit-education-plan-dialog.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-education-plan-data-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    EducationPlanDataItemComponent,
    NgForOf,
    CreateEducationPlanDialogComponent,
    NgIf,
    RemoveEducationPlanDialogComponent,
    EditEducationPlanDialogComponent,
  ],
  templateUrl: './education-plan-data-list.component.html',
  styleUrl: './education-plan-data-list.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class EducationPlanDataListComponent {
  @Input({ required: true }) direction: EducationDirection;
  @Output() selectedEducationPlan: EventEmitter<EducationPlan> =
    new EventEmitter();
  @Output() educationPlanRemoved: EventEmitter<void> = new EventEmitter();
  public currentEducationPlan: EducationPlan | null = null;
  public removeEducationPlanRequest: EducationPlan | null;
  public editEducationPlanRequest: EducationPlan | null;
  public isCreatingNewPlan: boolean = false;

  public handleEducationPlanCreation(plan: EducationPlan): void {
    this.direction.plans.push(plan);
  }

  public handleEducationPlanRemoved(plan: EducationPlan): void {
    this.direction.plans.splice(this.direction.plans.indexOf(plan), 1);
    if (this.currentEducationPlan && this.currentEducationPlan.id == plan.id) {
      this.currentEducationPlan = null;
      this.educationPlanRemoved.emit();
      this.selectedEducationPlan.emit(null!);
    }
  }
}

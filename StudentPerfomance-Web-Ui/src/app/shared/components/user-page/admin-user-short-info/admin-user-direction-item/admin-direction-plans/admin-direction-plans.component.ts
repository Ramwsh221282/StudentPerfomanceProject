import { Component, Input } from '@angular/core';
import { IAdminEducationPlanInfoResponse } from '../../../models/admin-education-plan-info.response';
import { BlueOutlineButtonComponent } from '../../../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';

@Component({
  selector: 'app-admin-direction-plans',
  standalone: true,
  imports: [BlueOutlineButtonComponent],
  templateUrl: './admin-direction-plans.component.html',
  styleUrl: './admin-direction-plans.component.scss',
})
export class AdminDirectionPlansComponent {
  @Input({ required: true }) plan: IAdminEducationPlanInfoResponse;

  public test(): void {}
}

import { Component, Input } from '@angular/core';
import { IAdminEducationDirectionInfo } from '../../models/admin-education-direction-info.response';
import { BlueOutlineButtonComponent } from '../../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { AdminDirectionPlansComponent } from './admin-direction-plans/admin-direction-plans.component';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { RedOutlineButtonComponent } from '../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-admin-user-direction-item',
  standalone: true,
  imports: [
    BlueOutlineButtonComponent,
    AdminDirectionPlansComponent,
    NgIf,
    NgForOf,
    RedOutlineButtonComponent,
    NgClass,
  ],
  templateUrl: './admin-user-direction-item.component.html',
  styleUrl: './admin-user-direction-item.component.scss',
})
export class AdminUserDirectionItemComponent {
  @Input({ required: true }) direction: IAdminEducationDirectionInfo;
  public isPlansShown: boolean = false;

  public showPlans(): void {
    this.isPlansShown = !this.isPlansShown;
  }
}

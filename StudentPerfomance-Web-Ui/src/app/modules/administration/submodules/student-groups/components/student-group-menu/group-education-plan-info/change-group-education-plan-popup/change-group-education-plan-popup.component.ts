import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentGroup } from '../../../../services/studentsGroup.interface';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { SearchDirectionsService } from '../../../../../education-directions/services/search-directions.service';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirection } from '../../../../../education-directions/models/education-direction-interface';
import { GreenOutlineButtonComponent } from '../../../../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { FloatingLabelInputComponent } from '../../../../../../../../building-blocks/floating-label-input/floating-label-input.component';
import { DropdownListComponent } from '../../../../../../../../building-blocks/dropdown-list/dropdown-list.component';
import { NgIf } from '@angular/common';
import { EducationDirectionsSelectComponent } from '../../../../../education-plans/components/create-education-plan-dropdown/education-directions-select/education-directions-select.component';

@Component({
  selector: 'app-change-group-education-plan-popup',
  standalone: true,
  imports: [
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    FloatingLabelInputComponent,
    DropdownListComponent,
    NgIf,
    EducationDirectionsSelectComponent,
  ],
  templateUrl: './change-group-education-plan-popup.component.html',
  styleUrl: './change-group-education-plan-popup.component.scss',
})
export class ChangeGroupEducationPlanPopupComponent
  implements OnInit, ISubbmittable
{
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();

  protected isSelectingDirection: boolean = false;
  protected selectDirectionLabel: string = 'Выберите направление подготовки';
  protected selectedDirection: EducationDirection | null;
  protected directions: EducationDirection[] = [];

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _directionService: SearchDirectionsService,
  ) {}

  public ngOnInit(): void {
    this._directionService.getAll().subscribe((response) => {
      this.directions = response;
    });
  }

  public submit(): void {
    throw new Error('Method not implemented.');
  }

  protected handleDirectionSelect(direction: EducationDirection): void {
    this.selectDirectionLabel = `${direction.name} ${direction.code} ${direction.type}`;
    this.selectedDirection = { ...direction };
    console.log(this.selectedDirection);
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChanged.emit(this.visibility);
  }
}

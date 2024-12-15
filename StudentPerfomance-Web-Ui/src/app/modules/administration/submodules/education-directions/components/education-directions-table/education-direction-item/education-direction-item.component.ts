import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../models/education-direction-interface';

@Component({
  selector: 'app-education-direction-item',
  templateUrl: './education-direction-item.component.html',
  styleUrl: './education-direction-item.component.scss',
})
export class EducationDirectionItemComponent {
  @Input({ required: true }) public direction: EducationDirection;
  @Output() directionRemoveRequested: EventEmitter<EducationDirection> =
    new EventEmitter();
  protected isEditVisible: boolean = false;
}

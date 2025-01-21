import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';

@Component({
  selector: 'app-education-direction-item-block',
  imports: [
    NgOptimizedImage,
    EditIconButtonComponent,
    RemoveIconButtonComponent,
    NgClass,
  ],
  templateUrl: './education-direction-item-block.component.html',
  styleUrl: './education-direction-item-block.component.scss',
  standalone: true,
})
export class EducationDirectionItemBlockComponent {
  @Input({ required: true }) direction: EducationDirection;
  @Input({ required: true }) isSelected: boolean = false;
  @Output() selectedDirection: EventEmitter<EducationDirection> =
    new EventEmitter();
}

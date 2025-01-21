import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DropdownListComponent } from '../../../../../../../building-blocks/dropdown-list/dropdown-list.component';

@Component({
    selector: 'app-direction-type-select',
    templateUrl: './direction-type-select.component.html',
    styleUrl: './direction-type-select.component.scss',
    imports: [DropdownListComponent]
})
export class DirectionTypeSelectComponent {
  @Input({ required: true }) visibility: boolean;
  @Output() visibilityChange: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() itemSelected: EventEmitter<string> = new EventEmitter();
}

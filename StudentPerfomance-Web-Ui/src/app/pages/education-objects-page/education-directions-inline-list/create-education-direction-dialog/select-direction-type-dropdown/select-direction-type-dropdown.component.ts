import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';

@Component({
  selector: 'app-select-direction-type-dropdown',
  imports: [NgForOf, NgIf],
  templateUrl: './select-direction-type-dropdown.component.html',
  styleUrl: './select-direction-type-dropdown.component.scss',
  standalone: true,
})
export class SelectDirectionTypeDropdownComponent {
  @Output() directionTypeSelected: EventEmitter<string> = new EventEmitter();
  public readonly directions: string[] = ['Бакалавриат', 'Магистратура'];

  public onDirectionTypeSelect(type: string, event: MouseEvent): void {
    event.stopPropagation();
    this.directionTypeSelected.emit(type);
  }
}

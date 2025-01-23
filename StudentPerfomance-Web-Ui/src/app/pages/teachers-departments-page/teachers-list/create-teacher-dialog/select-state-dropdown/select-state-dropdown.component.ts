import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-select-state-dropdown',
  imports: [NgForOf],
  templateUrl: './select-state-dropdown.component.html',
  styleUrl: './select-state-dropdown.component.scss',
  standalone: true,
})
export class SelectStateDropdownComponent {
  @Output() workingStateSelected: EventEmitter<string> = new EventEmitter();
  public readonly workingStates = ['Штатный', 'Внешний совместитель'];

  public handleStateSelection(state: string, $event: MouseEvent) {
    $event.stopPropagation();
    this.workingStateSelected.emit(state);
  }
}

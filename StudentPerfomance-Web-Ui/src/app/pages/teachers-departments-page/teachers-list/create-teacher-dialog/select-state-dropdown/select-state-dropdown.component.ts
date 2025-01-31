import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-select-state-dropdown',
  imports: [NgForOf],
  templateUrl: './select-state-dropdown.component.html',
  styleUrl: './select-state-dropdown.component.scss',
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
export class SelectStateDropdownComponent {
  @Output() workingStateSelected: EventEmitter<string> = new EventEmitter();
  public readonly workingStates = ['Штатный', 'Внешний совместитель'];

  public handleStateSelection(state: string, $event: MouseEvent) {
    $event.stopPropagation();
    this.workingStateSelected.emit(state);
  }
}

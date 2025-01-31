import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-control-week-number-select',
  imports: [NgForOf],
  templateUrl: './control-week-number-select.component.html',
  styleUrl: './control-week-number-select.component.scss',
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
export class ControlWeekNumberSelectComponent {
  @Output() numberSelected: EventEmitter<string> = new EventEmitter();
  public readonly sessionNumbers: string[] = ['1', '2', '3', '4'];

  public select(number: string, $event: MouseEvent): void {
    $event.stopPropagation();
    this.numberSelected.emit(number);
  }
}

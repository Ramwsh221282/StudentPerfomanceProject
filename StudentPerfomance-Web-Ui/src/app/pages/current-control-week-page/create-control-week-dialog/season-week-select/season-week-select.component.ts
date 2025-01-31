import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-season-week-select',
  imports: [NgForOf],
  templateUrl: './season-week-select.component.html',
  styleUrl: './season-week-select.component.scss',
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
export class SeasonWeekSelectComponent {
  @Output() seasonSelected: EventEmitter<string> = new EventEmitter();
  public readonly sessionSeasons: string[] = ['Весна', 'Осень'];

  public selectSeason(season: string, $event: MouseEvent): void {
    $event.stopPropagation();
    this.seasonSelected.emit(season);
  }
}

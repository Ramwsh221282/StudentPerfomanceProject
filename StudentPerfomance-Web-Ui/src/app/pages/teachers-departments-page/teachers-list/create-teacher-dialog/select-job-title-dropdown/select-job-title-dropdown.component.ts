import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-select-job-title-dropdown',
  imports: [NgForOf],
  templateUrl: './select-job-title-dropdown.component.html',
  styleUrl: './select-job-title-dropdown.component.scss',
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
export class SelectJobTitleDropdownComponent {
  @Output() jobTitleSelected: EventEmitter<string> = new EventEmitter();

  public readonly jobTitles = [
    'Профессор',
    'Ассистент',
    'Старший преподаватель',
    'Доцент',
  ];

  public handleSelection(jobTitle: string, $event: MouseEvent) {
    $event.stopPropagation();
    this.jobTitleSelected.emit(jobTitle);
  }
}

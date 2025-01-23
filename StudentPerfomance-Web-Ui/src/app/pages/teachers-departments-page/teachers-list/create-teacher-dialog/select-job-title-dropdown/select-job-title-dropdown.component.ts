import { Component, EventEmitter, Output } from '@angular/core';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-select-job-title-dropdown',
  imports: [NgForOf],
  templateUrl: './select-job-title-dropdown.component.html',
  styleUrl: './select-job-title-dropdown.component.scss',
  standalone: true,
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

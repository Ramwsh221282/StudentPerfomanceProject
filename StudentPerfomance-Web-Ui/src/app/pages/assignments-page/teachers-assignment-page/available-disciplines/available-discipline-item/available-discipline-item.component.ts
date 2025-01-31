import { Component, Input } from '@angular/core';
import { TeacherJournalDiscipline } from '../../../models/teacher-journal-disciplines';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TeacherAssignmentPageViewmodel } from '../../teacher-assignment-page.viewmodel';

@Component({
  selector: 'app-available-discipline-item',
  imports: [NgOptimizedImage, NgClass],
  templateUrl: './available-discipline-item.component.html',
  styleUrl: './available-discipline-item.component.scss',
})
export class AvailableDisciplineItemComponent {
  @Input({ required: true }) discipline: TeacherJournalDiscipline;

  public constructor(
    protected readonly viewModel: TeacherAssignmentPageViewmodel,
  ) {}

  public selectDiscipline($event: MouseEvent): void {
    $event.stopPropagation();
    this.viewModel.selectDiscipline(this.discipline);
  }
}

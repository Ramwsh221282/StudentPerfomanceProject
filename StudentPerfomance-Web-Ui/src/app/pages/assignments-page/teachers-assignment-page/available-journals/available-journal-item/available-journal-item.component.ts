import { Component, Input } from '@angular/core';
import { TeacherJournal } from '../../../models/teacher-journal';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TeacherAssignmentPageViewmodel } from '../../teacher-assignment-page.viewmodel';

@Component({
  selector: 'app-available-journal-item',
  imports: [NgOptimizedImage, NgClass],
  templateUrl: './available-journal-item.component.html',
  styleUrl: './available-journal-item.component.scss',
  standalone: true,
})
export class AvailableJournalItemComponent {
  @Input({ required: true }) journal: TeacherJournal;

  public constructor(
    protected readonly viewModel: TeacherAssignmentPageViewmodel,
  ) {}

  public selectJournal($event: MouseEvent): void {
    $event.stopPropagation();
    this.viewModel.selectJournal(this.journal);
  }
}

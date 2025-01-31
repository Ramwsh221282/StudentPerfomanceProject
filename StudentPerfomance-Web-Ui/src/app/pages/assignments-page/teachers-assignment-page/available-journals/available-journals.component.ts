import { Component, Input } from '@angular/core';
import { TeacherJournal } from '../../models/teacher-journal';
import { NgForOf, NgIf } from '@angular/common';
import { AvailableJournalItemComponent } from './available-journal-item/available-journal-item.component';

@Component({
  selector: 'app-available-journals',
  imports: [NgIf, AvailableJournalItemComponent, NgForOf],
  templateUrl: './available-journals.component.html',
  styleUrl: './available-journals.component.scss',
  standalone: true,
})
export class AvailableJournalsComponent {
  @Input({ required: true }) journals: TeacherJournal[] = [];
}

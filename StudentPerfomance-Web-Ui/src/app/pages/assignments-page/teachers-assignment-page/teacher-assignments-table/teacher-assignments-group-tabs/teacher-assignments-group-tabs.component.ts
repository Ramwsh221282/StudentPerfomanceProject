import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgClass, NgForOf } from '@angular/common';
import { TeacherJournal } from '../../../models/teacher-journal';

@Component({
  selector: 'app-teacher-assignments-group-tabs',
  imports: [NgForOf, NgClass],
  templateUrl: './teacher-assignments-group-tabs.component.html',
  styleUrl: './teacher-assignments-group-tabs.component.scss',
})
export class TeacherAssignmentsGroupTabsComponent {
  @Input({ required: true }) teacherJournals: TeacherJournal[];
  @Output() journalSelected: EventEmitter<TeacherJournal> = new EventEmitter();
  protected activeGroupName: string = '';

  protected handleGroupSelect(journal: TeacherJournal): void {
    this.journalSelected.emit(journal);
    this.activeGroupName = journal.groupName.name;
  }
}

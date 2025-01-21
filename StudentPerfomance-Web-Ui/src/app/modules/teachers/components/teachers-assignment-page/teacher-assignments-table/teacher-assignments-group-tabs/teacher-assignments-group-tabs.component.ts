import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TeacherJournal } from '../../../../models/teacher-journal';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { TeacherAssignmentGroupMenuComponent } from './teacher-assignment-group-menu/teacher-assignment-group-menu.component';

@Component({
    selector: 'app-teacher-assignments-group-tabs',
    imports: [NgForOf, NgClass, NgIf, TeacherAssignmentGroupMenuComponent],
    templateUrl: './teacher-assignments-group-tabs.component.html',
    styleUrl: './teacher-assignments-group-tabs.component.scss'
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

import { Component, Input } from '@angular/core';
import { TeacherJournalStudent } from '../../../../models/teacher-journal-students';

@Component({
  selector: 'app-teacher-assignments',
  templateUrl: './teacher-assignments.component.html',
  styleUrl: './teacher-assignments.component.scss',
})
export class TeacherAssignmentsComponent {
  @Input({ required: true }) student: TeacherJournalStudent;
}

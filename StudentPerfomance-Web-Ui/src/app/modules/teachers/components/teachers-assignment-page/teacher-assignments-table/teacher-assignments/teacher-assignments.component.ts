import { Component, Input } from '@angular/core';
import { TeacherJournalStudent } from '../../../../models/teacher-journal-students';
import { TeacherJournalDiscipline } from '../../../../models/teacher-journal-disciplines';
import { TeacherAssignmentsService } from './teacher-assignments.service';

@Component({
    selector: 'app-teacher-assignments',
    templateUrl: './teacher-assignments.component.html',
    styleUrl: './teacher-assignments.component.scss',
    providers: [TeacherAssignmentsService],
    standalone: false
})
export class TeacherAssignmentsComponent {
  @Input({ required: true }) student: TeacherJournalStudent;
  @Input({ required: true }) discipline: TeacherJournalDiscipline;
  protected isSelectingMark: boolean = false;

  public constructor(private readonly _service: TeacherAssignmentsService) {}

  protected teacherMarkSelectionUpdate(student: TeacherJournalStudent): void {
    this.student = { ...student };
    this._service
      .makeAssignment(this.student, this.discipline)
      .subscribe((response) => {
        this.student.assignment.value = response['value'];
      });
  }
}

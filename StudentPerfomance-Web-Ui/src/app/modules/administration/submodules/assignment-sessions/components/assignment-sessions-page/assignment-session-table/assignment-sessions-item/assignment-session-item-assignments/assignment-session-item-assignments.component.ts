import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AssignmentSessionWeek } from '../../../../../models/assignment-session-week';
import { StudentAssignments } from '../../../../../models/assignment-session-student-assignments';
import { AssignmentSessionAssignment } from '../../../../../models/assignment-session-assignment';

@Component({
  selector: 'app-assignment-session-item-assignments',
  templateUrl: './assignment-session-item-assignments.component.html',
  styleUrl: './assignment-session-item-assignments.component.scss',
})
export class AssignmentSessionItemAssignmentsComponent implements OnInit {
  @Input({ required: true }) week: AssignmentSessionWeek;
  @Output() visibility: EventEmitter<void> = new EventEmitter();

  public constructor() {}

  public ngOnInit(): void {}

  protected getStudents(): StudentAssignments[] {
    const students: StudentAssignments[] = [];
    const assignment = this.week.disciplines[0];
    for (let student of assignment.students) {
      students.push(student);
    }
    return students;
  }

  protected getStudentGrade(discipline: string, recordBook: number): string {
    const assignment: AssignmentSessionAssignment = this.week.disciplines.find(
      (a) => a.discipline.name == discipline,
    )!;

    const studentAssignment = assignment.students.find(
      (s) => s.recordbook.recordbook == recordBook,
    )!;

    if (studentAssignment.value == 'Не проставлена') return 'НП';

    if (studentAssignment.value == 'Нет аттестации') return 'НА';

    return studentAssignment.value;
  }
}

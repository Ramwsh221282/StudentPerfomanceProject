import { Component, Input } from '@angular/core';
import { AssignmentSessionWeek } from '../../../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-week';
import { StudentAssignments } from '../../../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-student-assignments';
import { AssignmentSessionAssignment } from '../../../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-assignment';
import { DecimalPipe, NgClass } from '@angular/common';

@Component({
  selector: 'app-control-week-group-item',
  imports: [DecimalPipe, NgClass],
  templateUrl: './control-week-group-item.component.html',
  styleUrl: './control-week-group-item.component.scss',
  standalone: true,
})
export class ControlWeekGroupItemComponent {
  @Input({ required: true }) week: AssignmentSessionWeek;

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

  protected isBadMark(grade: string): boolean {
    return grade === '2' || grade === 'НП' || grade === 'НА';
  }
}

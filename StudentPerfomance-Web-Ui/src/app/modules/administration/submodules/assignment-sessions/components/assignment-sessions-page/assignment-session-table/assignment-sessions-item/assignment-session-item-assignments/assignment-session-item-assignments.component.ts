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

    const assignment = this.week.assignments[0];

    for (let student of assignment.studentAssignments) {
      students.push(student);
    }

    return students;
  }

  protected getStudentGrade(
    discipline: string,
    student: StudentAssignments
  ): string {
    const assignment: AssignmentSessionAssignment = this.week.assignments.find(
      (a) => a.discipline == discipline
    )!;

    const studentAssignment = assignment.studentAssignments.find(
      (s) => s.studentRecordbook == student.studentRecordbook
    )!;

    return studentAssignment.value;
  }

  private getStudentMarks(student: StudentAssignments): string[] {
    const marks: string[] = [];
    for (let assignment of this.week.assignments) {
      for (let studentAssignment of assignment.studentAssignments) {
        if (studentAssignment.studentRecordbook == student.studentRecordbook)
          marks.push(studentAssignment.value);
      }
    }
    return marks;
  }

  protected getStudentAverage(student: StudentAssignments): string {
    const marks = this.getStudentMarks(student);
    let sum = 0;
    let count = 0;
    for (let mark of marks) {
      if (mark == 'Нет проставления') continue;
      if (mark == 'Неаттестация') {
        sum += 2;
      } else if (!isNaN(parseFloat(mark))) {
        sum += parseFloat(mark);
      }
      count++;
    }

    if (count === 0) {
      return '0.00';
    }

    return (sum / count).toFixed(2);
  }

  protected getStudentInvividualPerfomance(student: StudentAssignments) {
    const marks = this.getStudentMarks(student);
    let positive = 0;
    for (let mark of marks) {
      if (mark == 'Неаттестация') continue;
      if (mark == 'Нет проставления') continue;
      positive++;
    }

    if (marks.length == 0) {
      return '0.00';
    }

    const percentage = (positive / marks.length) * 100;
    return percentage.toFixed(2);
  }

  protected getAverageByDiscipline(
    discipline: AssignmentSessionAssignment
  ): string {
    let sum = 0;
    let count = 0;
    for (let assignment of discipline.studentAssignments) {
      if (assignment.value == 'Нет проставления') continue;
      if (assignment.value == 'Неаттестация') {
        sum += 2;
      } else if (!isNaN(parseFloat(assignment.value))) {
        sum += parseFloat(assignment.value);
      }
      count++;
    }

    if (count === 0) {
      return '0.00';
    }

    return (sum / count).toFixed(2);
  }

  protected getPerfomanceByDiscipline(
    discipline: AssignmentSessionAssignment
  ): string {
    const marks = discipline.studentAssignments.length;
    let positive = 0;
    for (let assignment of discipline.studentAssignments) {
      if (assignment.value == 'Неаттестация') continue;
      if (assignment.value == 'Нет проставления') continue;
      positive++;
    }

    if (marks == 0) {
      return '0.00';
    }

    const percentage = (positive / marks) * 100;
    return percentage.toFixed(2);
  }

  protected getGroupTotalAverage(): string {
    const students = this.getStudents();
    let averages = 0;
    for (let student of students) {
      const studentAverage = this.getStudentAverage(student);
      averages += +studentAverage;
    }

    if (students.length == 0) return '0.00';

    return (averages / students.length).toFixed(2);
  }

  protected getGroupTotalPerfomance(): string {
    const studentsCount = this.getStudents().length;
    const totalMarks = this.week.assignments.length * studentsCount;
    let positive = 0;
    for (let assignment of this.week.assignments) {
      for (let mark of assignment.studentAssignments) {
        if (mark.value == 'Неаттестация') continue;
        if (mark.value == 'Нет проставления') continue;
        positive++;
      }
    }

    if (totalMarks == 0) return '0.00';

    const percentage = (positive / totalMarks) * 100;
    return percentage.toFixed(2);
  }
}

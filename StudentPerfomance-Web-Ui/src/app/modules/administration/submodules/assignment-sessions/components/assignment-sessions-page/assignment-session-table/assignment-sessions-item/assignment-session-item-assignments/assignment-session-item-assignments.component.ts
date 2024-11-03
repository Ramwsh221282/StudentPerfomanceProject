import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AssignmentSessionWeek } from '../../../../../models/assignment-session-week';

@Component({
  selector: 'app-assignment-session-item-assignments',
  templateUrl: './assignment-session-item-assignments.component.html',
  styleUrl: './assignment-session-item-assignments.component.scss',
})
export class AssignmentSessionItemAssignmentsComponent implements OnInit {
  @Input({ required: true }) week: AssignmentSessionWeek;
  @Output() visibility: EventEmitter<void> = new EventEmitter();

  protected studentGrades: StudentGrades[];
  protected disciplines: string[];

  public constructor() {
    this.studentGrades = [];
    this.disciplines = [];
  }

  public ngOnInit(): void {
    this.disciplines = Array.from(
      new Set(this.week.assignments.map((a) => a.discipline))
    );
    this.week.assignments.forEach((assignment) => {
      const studentName = `${assignment.assignedToSurname} ${
        assignment.assignedToName[0]
      } ${
        assignment.assignedToPatronymic == null
          ? ''
          : assignment.assignedToPatronymic[0]
      }`;
      const discipline = assignment.discipline;
      const assignmentValue = assignment.assignmentValue;
      const existingRow = this.studentGrades.find(
        (row) => row.studentName == studentName
      );
      if (existingRow) {
        existingRow[discipline] = assignmentValue!;
      } else {
        const newRow: StudentGrades = {
          studentName,
          [discipline]: assignmentValue!,
        };
        this.studentGrades.push(newRow);
      }
    });

    this.disciplines.forEach((discipline) => {
      this.studentGrades.forEach((row) => {
        if (!row[discipline]) {
          row[discipline] = 'Нет проставления';
        }
      });
    });
  }
}

interface StudentGrades {
  studentName: string;
  [discipline: string]: string;
}

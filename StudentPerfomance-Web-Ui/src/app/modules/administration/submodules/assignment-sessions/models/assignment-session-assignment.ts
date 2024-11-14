import { StudentAssignments } from './assignment-session-student-assignments';

export interface AssignmentSessionAssignment {
  id: string;
  discipline: string;
  studentAssignments: StudentAssignments[];
  assignmentAverage: number;
  assignmentPerfomance: number;
  departmentName: string;
  teacherName: string;
  teacherSurname: string;
  teacherPatronymic: string;
}

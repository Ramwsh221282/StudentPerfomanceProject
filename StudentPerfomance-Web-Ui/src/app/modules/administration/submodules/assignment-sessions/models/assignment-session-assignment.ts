import { StudentAssignments } from './assignment-session-student-assignments';

export interface AssignmentSessionAssignment {
  discipline: string;
  studentAssignments: StudentAssignments[];
}

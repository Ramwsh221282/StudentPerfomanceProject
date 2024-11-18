import { StudentAssignments } from './assignment-session-student-assignments';

export interface AssignmentSessionAssignment {
  id: string;
  average: number;
  perfomance: number;
  discipline: AssignmentSessionAssignmentDiscipline;
  teacherName: AssignmentSessionAssignmentTeacherName;
  students: StudentAssignments[];
}

export interface AssignmentSessionAssignmentTeacherName {
  name: string;
  surname: string;
  patronymic: string | null;
}

export interface AssignmentSessionAssignmentDiscipline {
  name: string;
}

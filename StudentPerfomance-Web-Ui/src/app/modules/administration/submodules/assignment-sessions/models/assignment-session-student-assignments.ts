export interface StudentAssignments {
  id: string;
  value: string;
  name: AssignmentStudentAssignmentName;
  recordbook: AssignmentStudentAssignmentRecordbook;
  average: number;
  perfomance: number;
}

export interface AssignmentStudentAssignmentName {
  name: string;
  surname: string;
  patronymic: string | null;
}

export interface AssignmentStudentAssignmentRecordbook {
  recordbook: number;
}

export interface AssignmentSessionAssignment {
  assignerName: string;
  assignerSurname: string;
  assignerPatronymic: string | null;
  assignerDepartment: string;
  assignedToName: string;
  assignedToSurname: string;
  assignedToPatronymic: string | null;
  assignedToRecordbook: number;
  assignmentValue: string | null;
  assignmentDate: string | null;
  discipline: string;
}

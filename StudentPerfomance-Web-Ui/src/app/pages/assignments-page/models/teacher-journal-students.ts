export interface TeacherJournalStudent {
  assignment: {
    value: number;
  };
  name: string;
  surname: string;
  patronymic: string | null;
  belongsToGroup: string;
  recordbook: number;
  id: string;
  requiresAssignment: boolean;
}

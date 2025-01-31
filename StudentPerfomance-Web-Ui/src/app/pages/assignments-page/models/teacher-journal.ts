import { TeacherJournalDiscipline } from './teacher-journal-disciplines';

export interface TeacherJournal {
  disciplines: TeacherJournalDiscipline[];
  groupName: {
    name: string;
  };
  requiresAssignment: boolean;
}

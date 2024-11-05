import { TeacherJournalStudent } from './teacher-journal-students';

export interface TeacherJournalDiscipline {
  name: {
    name: string;
  };
  students: TeacherJournalStudent[];
}

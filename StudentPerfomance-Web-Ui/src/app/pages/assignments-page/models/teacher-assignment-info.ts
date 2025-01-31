import { TeacherJournal } from './teacher-journal';

export interface TeacherAssignmentInfo {
  journals: TeacherJournal[];
  teacher: {
    name: string;
    patronymic: string | null;
    surname: string;
  };
}

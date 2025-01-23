import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';

export interface Student {
  name: string;
  surname: string;
  patronymic: string | null;
  recordbook: number;
  state: string;
  group: StudentGroup;
  id: string;
}

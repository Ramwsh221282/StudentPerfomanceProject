import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';

export interface Student {
  name: string;
  surname: string;
  patronymic: string;
  recordbook: number;
  state: string;
  group: StudentGroup;
}

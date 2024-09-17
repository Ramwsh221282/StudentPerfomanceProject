import { Student } from '../../students/models/student.interface';

export interface StudentGroup {
  groupName: string;
  studentsCount: number;
  students: Student[];
}

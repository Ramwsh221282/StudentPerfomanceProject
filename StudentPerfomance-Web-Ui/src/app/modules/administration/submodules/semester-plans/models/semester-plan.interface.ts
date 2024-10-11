import { Teacher } from '../../teachers/models/teacher.interface';

export interface SemesterPlan {
  discipline: string;
  teacher: Teacher;
}

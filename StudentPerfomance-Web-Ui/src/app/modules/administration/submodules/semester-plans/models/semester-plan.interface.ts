import { Semester } from '../../semesters/models/semester.interface';
import { Teacher } from '../../teachers/models/teacher.interface';

export interface SemesterPlan {
  discipline: string;
  semester: Semester;
  teacher: Teacher | null;
}

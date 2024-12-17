import { EducationPlan } from '../../education-plans/models/education-plan-interface';
import { Student } from '../../students/models/student.interface';

export interface StudentGroup {
  entityNumber: number;
  name: string;
  plan: EducationPlan;
  activeSemesterNumber: number;
  students: Student[];
}

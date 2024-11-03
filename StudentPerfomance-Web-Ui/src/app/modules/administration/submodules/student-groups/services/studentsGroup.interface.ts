import { EducationPlan } from '../../education-plans/models/education-plan-interface';

export interface StudentGroup {
  entityNumber: number;
  name: string;
  plan: EducationPlan;
  activeSemesterNumber: number;
}

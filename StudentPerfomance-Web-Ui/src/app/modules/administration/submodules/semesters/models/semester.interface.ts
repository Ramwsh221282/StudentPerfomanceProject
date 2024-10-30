import { EducationPlan } from '../../education-plans/models/education-plan-interface';

export interface Semester {
  number: number;
  educationPlan: EducationPlan;
  contractsCount: number;
}

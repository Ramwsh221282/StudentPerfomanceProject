import { EducationPlan } from '../../education-plans/models/education-plan-interface';

export interface EducationDirection {
  id: string;
  entityNumber: number;
  code: string;
  name: string;
  type: string;
  educationPlansCount: number;
  plans: EducationPlan[];
}

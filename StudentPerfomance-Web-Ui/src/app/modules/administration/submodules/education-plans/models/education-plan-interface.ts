import { EducationDirection } from '../../education-directions/models/education-direction-interface';

export interface EducationPlan {
  year: number;
  direction: EducationDirection;
  entityNumber: number;
}

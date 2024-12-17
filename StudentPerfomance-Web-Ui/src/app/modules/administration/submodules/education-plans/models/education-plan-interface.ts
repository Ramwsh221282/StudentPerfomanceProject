import { EducationDirection } from '../../education-directions/models/education-direction-interface';
import { Teacher } from '../../teachers/models/teacher.interface';

export interface EducationPlan {
  id: string;
  year: number;
  direction: EducationDirection;
  entityNumber: number;
  semesters: EducationPlanSemester[];
}

export interface EducationPlanSemester {
  id: string;
  number: number;
  disciplines: SemesterDiscipline[];
}

export interface SemesterDiscipline {
  id: string;
  disciplineName: string;
  teacher: Teacher;
}

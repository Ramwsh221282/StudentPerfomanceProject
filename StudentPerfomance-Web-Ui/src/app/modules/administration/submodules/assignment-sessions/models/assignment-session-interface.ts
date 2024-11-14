import { AssignmentSessionCoursePerfomance } from './assignment-session-course-perfomance';
import { AssignmentSessionDepartmentPerfomance } from './assignment-session-department-perfomance';
import { AssignmentSessionDirectionCodePerfomance } from './assignment-session-direction-code-perfomance';
import { AssignmentSessionDirectionPerfomance } from './assignment-session-direction-perfomance';
import { AssignmentSessionUniversityPerfomance } from './assignment-session-university-perfomance';
import { AssignmentSessionWeek } from './assignment-session-week';

export interface AssignmentSession {
  id: string;
  number: number;
  startDate: string;
  endDate: string;
  state: string;
  weeks: AssignmentSessionWeek[];
  coursePerfomances: AssignmentSessionCoursePerfomance[];
  directionTypePerfomances: AssignmentSessionDirectionPerfomance[];
  directionCodePerfomances: AssignmentSessionDirectionCodePerfomance[];
  departmentPerfomances: AssignmentSessionDepartmentPerfomance[];
  universityPerfomance: AssignmentSessionUniversityPerfomance;
}

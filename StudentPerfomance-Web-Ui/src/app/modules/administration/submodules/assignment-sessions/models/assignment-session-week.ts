import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';
import { AssignmentSessionAssignment } from './assignment-session-assignment';

export interface AssignmentSessionWeek {
  id: string;
  group: StudentGroup;
  assignments: AssignmentSessionAssignment[];
  averageMarks: number;
  averagePerfomancePercent: number;
  directionType: string;
  course: number;
}

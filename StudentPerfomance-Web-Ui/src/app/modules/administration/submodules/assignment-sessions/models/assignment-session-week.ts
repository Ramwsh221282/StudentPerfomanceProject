import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';
import { AssignmentSessionAssignment } from './assignment-session-assignment';

export interface AssignmentSessionWeek {
  group: StudentGroup;
  assignments: AssignmentSessionAssignment[];
}

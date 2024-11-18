import { AssignmentSessionAssignment } from './assignment-session-assignment';

export interface AssignmentSessionWeek {
  id: string;
  average: number;
  perfomance: number;
  groupName: AssignmentSessionWeekGroupName;
  code: AssignmentSessionWeekDirectionCode;
  type: AssignmentSessionWeekDirectionType;
  course: AssignmentSessionWeekDirectionCourse;
  disciplines: AssignmentSessionAssignment[];
}

export interface AssignmentSessionWeekGroupName {
  name: string;
}

export interface AssignmentSessionWeekDirectionCode {
  code: string;
}

export interface AssignmentSessionWeekDirectionType {
  type: string;
}

export interface AssignmentSessionWeekDirectionCourse {
  value: number;
}

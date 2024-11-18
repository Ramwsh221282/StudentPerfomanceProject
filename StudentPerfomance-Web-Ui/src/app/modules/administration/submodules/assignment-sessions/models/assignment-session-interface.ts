import { AssignmentSessionWeek } from './assignment-session-week';

export interface AssignmentSession {
  id: string;
  number: number;
  startDate: string;
  endDate: string;
  state: string;
  weeks: AssignmentSessionWeek[];
}

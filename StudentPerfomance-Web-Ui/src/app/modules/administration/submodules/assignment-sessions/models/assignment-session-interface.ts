import { AssignmentSessionWeek } from './assignment-session-week';

export interface AssignmentSession {
  number: number;
  startDate: string;
  endDate: string | null;
  state: string;
  weeks: AssignmentSessionWeek[];
}

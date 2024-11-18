import { DisciplineReportInterface } from './discipline-report-interface';

export interface GroupReportInterface {
  id: string;
  rootId: string;
  directionCode: string;
  directionType: string;
  course: number;
  average: number;
  perfomance: number;
  groupName: string;
  Parts: DisciplineReportInterface[];
}

import { StudentStatisticsPartEntity } from './student-statistics-report';

export interface GroupStatisticsReportEntity {
  average: number;
  perfomance: number;
  groupName: string;
  atSemester: number;
  directionCode: string;
  directionType: string;
  parts: StudentStatisticsPartEntity[];
}

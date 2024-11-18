import { GroupReportInterface } from './group-report-interface';
import { DirectionTypeReportInterface } from './direction-type-report-interface';
import { CourseReportInterface } from './course-report-interface';
import { DirectionCodeReportInterface } from './direction-code-report-interface';

export interface ControlWeekReportInterface {
  rowNumber: number;
  id: string;
  creationDate: string;
  completionDate: string;
  groupParts: GroupReportInterface[];
  courseParts: CourseReportInterface[];
  directionTypeReport: DirectionTypeReportInterface[];
  directionCodeReport: DirectionCodeReportInterface[];
}

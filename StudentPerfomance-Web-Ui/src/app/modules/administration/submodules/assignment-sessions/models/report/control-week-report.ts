import { GroupStatisticsReportEntity } from './group-statistics-report';
import { CourseReportEntity } from './course-report-entity';
import { DirectionTypeReportEntity } from './direction-type-report-entity';
import { DirectionCodeReportEntity } from './direction-code-report-entity';
import { DepartmentStatisticsReportEntity } from './department-statistics-report-entity';

export interface ControlWeekReportEntity {
  creationDate: string;
  completionDate: string;
  isFinished: boolean;
  groupParts: GroupStatisticsReportEntity[];
  courseReport: CourseReportEntity;
  directionTypeReport: DirectionTypeReportEntity;
  directionCodeReport: DirectionCodeReportEntity;
  departmentReport: DepartmentStatisticsReportEntity;
}

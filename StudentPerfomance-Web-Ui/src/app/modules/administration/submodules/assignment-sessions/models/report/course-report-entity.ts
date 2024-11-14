import { CourseReportEntityPart } from './course-report-entity-part';

export interface CourseReportEntity {
  average: number;
  perfomance: number;
  parts: CourseReportEntityPart[];
}

import { TeacherStatisticsReportPartEntity } from './teacher-statistics-report-part-entity';

export interface DepartmentStatisticsReportPartEntity {
  departmentName: string;
  average: number;
  perfomance: number;
  parts: TeacherStatisticsReportPartEntity[];
}

import { DepartmentStatisticsReportPartEntity } from './department-statistics-report-part-entity';

export interface DepartmentStatisticsReportEntity {
  average: number;
  perfomance: number;
  parts: DepartmentStatisticsReportPartEntity[];
}

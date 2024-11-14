import { DirectionCodeReportPartEntity } from './direction-code-report-part-entity';

export interface DirectionCodeReportEntity {
  average: number;
  perfomance: number;
  parts: DirectionCodeReportPartEntity[];
}

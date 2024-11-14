import { DirectionTypeReportEntityPart } from './direction-type-report-entity-part';

export interface DirectionTypeReportEntity {
  average: number;
  perfomance: number;
  parts: DirectionTypeReportEntityPart[];
}

import { StudentReportInterface } from './student-report-interface';

export interface DisciplineReportInterface {
  id: string;
  rootId: string;
  disciplineName: string;
  teacherName: string;
  teacherSurname: string;
  teacherPatronymic: string | null;
  average: number;
  perfomance: number;
  Parts: StudentReportInterface[];
}

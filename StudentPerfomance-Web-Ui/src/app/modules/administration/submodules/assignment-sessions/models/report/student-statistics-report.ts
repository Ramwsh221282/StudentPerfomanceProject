import { StudentStatisticsOnDisciplinePartEntity } from './student-statistics-on-discipline-report';

export interface StudentStatisticsPartEntity {
  studentName: string;
  studentSurname: string;
  studentPatronymic: string;
  studentRecordBook: string;
  perfomance: number;
  average: number;
  parts: StudentStatisticsOnDisciplinePartEntity[];
}

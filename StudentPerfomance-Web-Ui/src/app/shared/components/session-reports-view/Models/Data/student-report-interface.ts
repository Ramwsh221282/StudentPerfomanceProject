export interface StudentReportInterface {
  id: string;
  rootId: string;
  studentName: string;
  studentSurname: string;
  studentPatronymic: string | null;
  average: number;
  perfomance: number;
  grade: string;
  recordbook: number;
}

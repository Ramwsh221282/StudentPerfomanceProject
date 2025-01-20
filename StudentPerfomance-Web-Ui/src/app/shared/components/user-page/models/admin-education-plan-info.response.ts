import { IAdminStudentGroupInfo } from './admin-student-group-info.interface';
import { IAdminSemesterInfo } from './admin-semester-info.interface';

export interface IAdminEducationPlanInfoResponse {
  id: string;
  year: number;
  hasGroups: boolean;
  groups: IAdminStudentGroupInfo[];
  semesters: IAdminSemesterInfo[];
}

export interface IAdminStudentGroupInfo {
  id: string;
  name: string;
  semester: IAdminStudentGroupSemester | null;
  hasSemester: boolean;
  students: IAdminStudentGroupStudent[];
  hasStudents: boolean;
}

export interface IAdminStudentGroupSemester {
  semester: number;
}

export interface IAdminStudentGroupStudent {
  name: string;
  surname: string;
  patronymic: string;
  state: string;
  recordBook: string;
}

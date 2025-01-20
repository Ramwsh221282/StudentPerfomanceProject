export interface IAdminSemesterInfo {
  id: string;
  number: number;
  disciplines: ISemesterDisciplineInfo[];
  hasDisciplines: boolean;
  hasDisciplinesWithoutTeacher: boolean;
  disciplinesWithoutTeacher: ISemesterDisciplineInfo[];
}

export interface ISemesterDisciplineInfo {
  id: string;
  discipline: string;
  hasTeacher: boolean;
  teacher: ISemesterDisciplineTeacherInfo | null;
}

export interface ISemesterDisciplineTeacherInfo {
  id: string;
  name: string;
  surname: string;
  patronymic: string | null;
  department: ISemesterDisciplineTeacherDepartmentInfo;
}

export interface ISemesterDisciplineTeacherDepartmentInfo {
  id: string;
  name: string;
}

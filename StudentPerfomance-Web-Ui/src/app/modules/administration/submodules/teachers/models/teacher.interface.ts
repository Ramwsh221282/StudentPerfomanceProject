import { Department } from '../../departments/models/departments.interface';

export interface Teacher {
  name: string;
  surname: string;
  patronymic: string;
  state: string;
  jobTitle: string;
  department: Department;
}

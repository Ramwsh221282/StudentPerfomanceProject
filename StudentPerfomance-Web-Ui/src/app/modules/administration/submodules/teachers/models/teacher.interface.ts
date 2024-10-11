import { Department } from '../../departments/models/departments.interface';

export interface Teacher {
  name: string;
  surname: string;
  thirdname: string;
  condition: string;
  job: string;
  department: Department;
}

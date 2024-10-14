import { Department } from '../../departments/models/departments.interface';

export interface Teacher {
  name: string;
  surname: string;
  thirdname: string;
  workingCondition: string;
  jobTitle: string;
  department: Department;
}

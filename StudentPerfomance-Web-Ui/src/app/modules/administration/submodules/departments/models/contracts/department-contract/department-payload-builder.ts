import { Department } from '../../departments.interface';

export const DepartmentPayloadBuilder = (department: Department): object => {
  return {
    name: department.name,
  };
};

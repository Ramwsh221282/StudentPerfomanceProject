import { Teacher } from '../../teachers/models/teacher.interface';

export interface Department {
  entityNumber: number;
  name: string;
  acronymus: string;
  teachers: Teacher[];
}

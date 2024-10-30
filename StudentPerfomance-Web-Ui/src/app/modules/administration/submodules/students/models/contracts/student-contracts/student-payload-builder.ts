import { Student } from '../../student.interface';

export const StudentPayloadBuilder = (student: Student): object => {
  return {
    name: student.name,
    surname: student.surname,
    patronymic: student.patronymic,
    state: student.state,
    recordbook: student.recordbook,
    groupName: student.group.name,
  };
};

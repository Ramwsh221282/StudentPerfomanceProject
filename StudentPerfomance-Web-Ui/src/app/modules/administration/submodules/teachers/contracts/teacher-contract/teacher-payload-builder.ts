import { Teacher } from '../../models/teacher.interface';

export const TeacherPayloadBuilder = (teacher: Teacher): object => {
  return {
    name: teacher.name,
    surname: teacher.surname,
    patronymic: teacher.patronymic,
    state: teacher.state,
    job: teacher.jobTitle,
  };
};

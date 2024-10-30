import { Semester } from '../../../../semesters/models/semester.interface';

export const SemesterPayloadBuilder = (semester: Semester): object => {
  return {
    number: semester.number,
  };
};

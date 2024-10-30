import { StudentGroup } from '../../../services/studentsGroup.interface';

export const StudentGroupPayloadBuilder = (group: StudentGroup): object => {
  return {
    name: group.name,
  };
};

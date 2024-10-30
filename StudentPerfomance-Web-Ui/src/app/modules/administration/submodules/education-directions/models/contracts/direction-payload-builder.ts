import { EducationDirection } from '../education-direction-interface';

export const DirectionPayloadBuilder = (
  direction: EducationDirection
): object => {
  return {
    name: direction.name,
    code: direction.code,
    type: direction.type,
  };
};

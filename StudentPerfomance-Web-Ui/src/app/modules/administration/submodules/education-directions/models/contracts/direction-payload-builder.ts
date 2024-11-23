import { EducationDirection } from '../education-direction-interface';

export const DirectionPayloadBuilder = (
  direction: EducationDirection,
): object => {
  return {
    name: direction.name,
    type: direction.type,
    code: direction.code,
  };
};

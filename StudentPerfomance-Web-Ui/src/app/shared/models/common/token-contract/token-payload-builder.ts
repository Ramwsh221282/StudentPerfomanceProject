import { User } from '../../../../pages/user-page/services/user-interface';

export const TokenPayloadBuilder = (user: User): object => {
  return {
    token: user.token,
  };
};

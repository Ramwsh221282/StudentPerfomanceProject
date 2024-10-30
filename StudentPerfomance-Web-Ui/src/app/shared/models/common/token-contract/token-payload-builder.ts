import { User } from '../../../../modules/users/services/user-interface';

export const TokenPayloadBuilder = (user: User): object => {
  return {
    token: user.token,
  };
};

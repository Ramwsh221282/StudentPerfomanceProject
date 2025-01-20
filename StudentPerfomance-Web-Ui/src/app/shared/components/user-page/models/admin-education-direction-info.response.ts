import { IAdminEducationPlanInfoResponse } from './admin-education-plan-info.response';

export interface IAdminEducationDirectionInfo {
  id: string;
  name: string;
  code: string;
  type: string;
  hasPlans: boolean;
  plans: IAdminEducationPlanInfoResponse[];
}

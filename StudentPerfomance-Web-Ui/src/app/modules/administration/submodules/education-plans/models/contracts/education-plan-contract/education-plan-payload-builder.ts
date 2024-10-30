import { EducationPlan } from '../../education-plan-interface';

export const EducationPlanPayloadBuilder = (plan: EducationPlan): object => {
  return {
    planYear: plan.year,
  };
};

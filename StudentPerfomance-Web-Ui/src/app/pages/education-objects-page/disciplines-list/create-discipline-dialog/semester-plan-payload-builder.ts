import { SemesterPlan } from '../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';

export const SemesterPlanPayloadBuilder = (plan: SemesterPlan): object => {
  return {
    discipline: plan.discipline,
  };
};

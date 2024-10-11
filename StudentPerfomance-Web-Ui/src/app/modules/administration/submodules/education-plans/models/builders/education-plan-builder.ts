import { NumericValueBuilder } from '../../../../../../shared/models/form-value-builder/numeric-value-builder';
import { EducationDirectionBuilder } from '../../../education-directions/models/builders/education-direction-builder';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';
import { EducationPlan } from '../education-plan-interface';

export class EducationPlanBuilder {
  private readonly _plan: EducationPlan;

  public constructor() {
    this._plan = {} as EducationPlan;
  }

  public buildDefault(): EducationPlan {
    const directionBuilder: EducationDirectionBuilder =
      new EducationDirectionBuilder();
    const numericBuilder: NumericValueBuilder = new NumericValueBuilder();
    this._plan.year = numericBuilder.build(this._plan.year);
    this._plan.direction = directionBuilder.buildDefault();
    return this._plan;
  }

  public buildInitialized(
    plan: EducationPlan,
    direction: EducationDirection
  ): EducationPlan {
    const directionBuilder: EducationDirectionBuilder =
      new EducationDirectionBuilder();
    const numericBuilder: NumericValueBuilder = new NumericValueBuilder();
    plan.year = numericBuilder.build(plan.year);
    direction = directionBuilder.buildInitialized(direction);
    plan.direction = { ...direction };
    return plan;
  }
}

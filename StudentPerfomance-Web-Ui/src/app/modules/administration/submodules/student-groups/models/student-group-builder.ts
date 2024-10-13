import { StringValueBuilder } from '../../../../../shared/models/form-value-builder/string-value-builder';
import { EducationPlanBuilder } from '../../education-plans/models/builders/education-plan-builder';
import { StudentGroup } from '../services/studentsGroup.interface';

export class StudentGroupBuilder {
  private readonly _group: StudentGroup;

  public constructor() {
    this._group = {} as StudentGroup;
  }

  public buildDefault(): StudentGroup {
    const educationPlanBuilder: EducationPlanBuilder =
      new EducationPlanBuilder();
    const stringValueBuilder: StringValueBuilder = new StringValueBuilder();
    const plan = educationPlanBuilder.buildDefault();
    this._group.plan = { ...plan };
    this._group.name = stringValueBuilder.extractStringOrEmpty(
      this._group.name
    );
    return this._group;
  }

  public buildInitialized(group: StudentGroup): StudentGroup {
    const educationPlanBuilder: EducationPlanBuilder =
      new EducationPlanBuilder();
    const stringValueBuilder: StringValueBuilder = new StringValueBuilder();
    const plan = educationPlanBuilder.buildInitialized(
      group.plan,
      group.plan.direction
    );
    group.name = stringValueBuilder.extractStringOrEmpty(group.name);
    group.plan = { ...plan };
    return group;
  }
}

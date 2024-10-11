import { StringValueBuilder } from '../../../../../../shared/models/form-value-builder/string-value-builder';
import { EducationDirection } from '../education-direction-interface';

export class EducationDirectionBuilder {
  private readonly _direction: EducationDirection;

  public constructor() {
    this._direction = {} as EducationDirection;
  }

  public buildDefault(): EducationDirection {
    const builder: StringValueBuilder = new StringValueBuilder();
    this._direction.code = builder.extractStringOrEmpty(this._direction.code);
    this._direction.name = builder.extractStringOrEmpty(this._direction.name);
    this._direction.type = builder.extractStringOrEmpty(this._direction.type);
    return this._direction;
  }

  public buildInitialized(direction: EducationDirection): EducationDirection {
    const builder: StringValueBuilder = new StringValueBuilder();
    direction.code = builder.extractStringOrEmpty(this._direction.code);
    direction.name = builder.extractStringOrEmpty(this._direction.name);
    direction.type = builder.extractStringOrEmpty(this._direction.type);
    return direction;
  }
}

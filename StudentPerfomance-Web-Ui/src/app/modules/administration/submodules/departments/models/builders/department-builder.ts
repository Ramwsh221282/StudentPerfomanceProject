import { StringValueBuilder } from '../../../../../../shared/models/form-value-builder/string-value-builder';
import { Department } from '../departments.interface';
import { DepartmentShortNameBuilder } from './department-short-name.builder';

export class DepartmentBuilder {
  private readonly _department: Department;

  public constructor() {
    this._department = {} as Department;
    const builder: StringValueBuilder = new StringValueBuilder();
    this._department.name = builder.extractStringOrEmpty(this._department.name);
    this._department.shortName = builder.extractStringOrEmpty(
      this._department.shortName
    );
  }

  public buildDefault(): Department {
    return this._department;
  }

  public buildInitialized(department: Department): Department {
    const builder: StringValueBuilder = new StringValueBuilder();
    department.name = builder.extractStringOrEmpty(this._department.name);
    department.shortName = builder.extractStringOrEmpty(
      this._department.shortName
    );
    const shortNameBuilder: DepartmentShortNameBuilder =
      new DepartmentShortNameBuilder(department);
    department.shortName = shortNameBuilder.buildShortName();
    return department;
  }
}

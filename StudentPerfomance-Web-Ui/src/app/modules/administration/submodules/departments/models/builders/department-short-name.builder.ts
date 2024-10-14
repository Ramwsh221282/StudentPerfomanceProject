import { Department } from '../departments.interface';

export class DepartmentShortNameBuilder {
  private readonly _department: Department;

  public constructor(department: Department) {
    this._department = { ...department };
  }

  public buildShortName(): string {
    const parts = this._department.name.split(' ');
    let shortName = '';

    for (const part of parts) {
      const filteredPart = part.match(/[a-zA-Z]/g)?.join('');
      if (filteredPart) {
        shortName += filteredPart.charAt(0).toUpperCase();
      }
    }
    return shortName;
  }
}

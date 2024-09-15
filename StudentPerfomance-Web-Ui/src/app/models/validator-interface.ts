import { ValidationResult } from './validation-result';

export interface IValidator {
  validate(): ValidationResult;
}

export class NumericValueBuilder {
  public build(value: number): number {
    return value == null || value == undefined ? 0 : value;
  }
}

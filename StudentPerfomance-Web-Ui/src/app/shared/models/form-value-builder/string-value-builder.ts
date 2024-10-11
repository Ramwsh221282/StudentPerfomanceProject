export class StringValueBuilder {
  public extractStringOrEmpty(value: string): string {
    return value == null || value == undefined ? '' : value;
  }
}

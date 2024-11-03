export class DateParser {
  private readonly _dateString: string;

  public constructor(dateString: string) {
    this._dateString = dateString;
  }

  public parseYear(): number {
    try {
      const parts: string[] = this._dateString.split('-');
      return +parts[0];
    } catch {
      return 0;
    }
  }

  public parseMonth(): number {
    try {
      const parts: string[] = this._dateString.split('-');
      const monthPart = parts[1];
      if (monthPart[0] == '0') return +monthPart[1];
      return +monthPart;
    } catch {
      return 0;
    }
  }

  public parseDay(): number {
    try {
      const parts: string[] = this._dateString.split('-');
      const dayPart = parts[2];
      if (dayPart[0] == '0') return +dayPart[1];
      return +dayPart;
    } catch {
      return 0;
    }
  }
}

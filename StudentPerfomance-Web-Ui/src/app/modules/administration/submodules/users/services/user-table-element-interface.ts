export interface UserRecord {
  number: number;
  name: string;
  surname: string;
  patronymic: string;
  role: string;
  email: string;
  lastTimeOnline: string | null;
  registeredDate: string | null;
}

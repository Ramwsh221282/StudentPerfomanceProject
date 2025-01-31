export interface UserRecord {
  id: string;
  number: number;
  name: string;
  surname: string;
  patronymic: string;
  role: string;
  email: string;
  lastTimeOnline: string | null;
  registeredDate: string | null;
}

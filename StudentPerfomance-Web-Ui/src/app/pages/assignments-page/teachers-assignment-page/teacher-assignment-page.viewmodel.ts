import { Injectable } from '@angular/core';
import { TeacherJournal } from '../models/teacher-journal';
import { TeacherJournalDiscipline } from '../models/teacher-journal-disciplines';
import { TeacherJournalStudent } from '../models/teacher-journal-students';

@Injectable({
  providedIn: 'any',
})
export class TeacherAssignmentPageViewmodel {
  private _journals: TeacherJournal[] = [];
  private isInited: boolean = false;
  private _currentJournal: TeacherJournal | null = null;
  private _currentDiscipline: TeacherJournalDiscipline | null = null;
  private _currentStudents: TeacherJournalStudent[] = [];
  private _isLocked: boolean = true;
  private _isFinished: boolean = false;
  private _isNotFullyCompleted: boolean = false;

  public get isLocked(): boolean {
    return this._isLocked;
  }

  public get isFinished(): boolean {
    return this._isFinished;
  }

  public get isNotFullyCompleted(): boolean {
    return this._isNotFullyCompleted;
  }

  public setNotFullyCompletedFalse(): void {
    this._isNotFullyCompleted = false;
  }

  public setFinishedFalse(): void {
    this._isFinished = false;
  }

  public handleFinish(): void {
    for (const journal of this._journals) {
      for (const discipline of journal.disciplines) {
        for (const student of discipline.students) {
          if (student.assignment.value === 1) {
            this._isFinished = false;
            this._isNotFullyCompleted = true;
            this._isLocked = false;
            return;
          }
        }
      }
    }
    this._isNotFullyCompleted = false;
    this._isLocked = true;
    this._isFinished = true;
  }

  public unlock(): void {
    this._isLocked = false;
  }

  public initialize(journals: TeacherJournal[]): void {
    if (this.isInited) return;
    this._journals = journals;
    this.isInited = true;
  }

  public selectJournal(journal: TeacherJournal): void {
    if (this._currentJournal != null) {
      this._currentDiscipline = null;
      this._currentStudents = [];
    }
    this._currentJournal = journal;
  }

  public selectDiscipline(discipline: TeacherJournalDiscipline): void {
    if (this._currentDiscipline != null) {
      this._currentDiscipline = null;
      this._currentStudents = [];
    }
    this._currentDiscipline = discipline;
    this._currentStudents = discipline.students;
  }

  public get currentDiscipline(): TeacherJournalDiscipline | null {
    return this._currentDiscipline;
  }

  public get currentJournal(): TeacherJournal | null {
    return this._currentJournal;
  }

  public get currentStudents(): TeacherJournalStudent[] {
    return this._currentStudents;
  }

  public isCurrentJournal(journal: TeacherJournal): boolean {
    if (this._currentJournal == null) return false;
    return journal.groupName == this._currentJournal.groupName;
  }

  public isCurrentDiscipline(discipline: TeacherJournalDiscipline): boolean {
    if (!this._currentDiscipline == null) return false;
    return this._currentDiscipline?.name! == discipline.name;
  }

  public get journals(): TeacherJournal[] {
    return this._journals;
  }
}
